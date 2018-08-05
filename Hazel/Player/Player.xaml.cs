using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hazel.Player
{
    /// <summary>
    /// Player.xaml에 대한 상호 작용 논리
    /// </summary>
    
    enum State { Stopped, Running, NotInit, Paused }
    enum LoopState { NotLoop, LoopAll, LoopOne }
    enum RandomState { Random, UnRandom }

    public partial class Player : UserControl
    {
        private WaveOutEvent player;
        private State state;
        private LoopState loopState;
        private RandomState randomState;
        private YoutubeSearchItem currentMusic;
        private WaveChannel32 volumeStream;

        public Player()
        {
            InitializeComponent();
            state = State.NotInit;
            loopState = LoopState.NotLoop;
            player = new WaveOutEvent();
        }

        public YoutubeSearchItem CurrentMusic
        {
            get => this.currentMusic;
            set {
                this.currentMusic = value;
                SetPlayer();
                Play();
                playTimeTrackBar.setPosition(new Point(0, 0));
            }
        }

        public YoutubeSearchItem NextMusic
        {
            get
            {
                int index = PlayList.List.IndexOf(currentMusic);
                int nextIndex;
                if (loopState == LoopState.LoopOne)
                {
                    nextIndex = index;
                }
                else
                {
                    if (index + 1 < PlayList.Count)
                    {
                        nextIndex = index + 1;
                    }
                    else
                    {
                        nextIndex = 0;
                    }
                }
                return PlayList.List[nextIndex];
            }
        }

        public YoutubeSearchItem PreviousMusic
        {
            get
            {
                int index = PlayList.List.IndexOf(currentMusic);
                int previousIndex;
                if (index - 1 >= 0)
                {
                    previousIndex = index - 1;
                }
                else
                {
                    previousIndex = PlayList.Count - 1;
                }
                return PlayList.List[previousIndex];
            }
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if(loopState == LoopState.NotLoop)
            {
                int index = PlayList.List.IndexOf(currentMusic);
                if(index + 1 == PlayList.List.Count)
                {
                    playOrStopImage.Source 
                        = new BitmapImage(new Uri(@"\image\PlayButton.png", UriKind.Relative));
                    player.Stop();
                    state = State.Stopped;
                }
            }
            else
            {
                CurrentMusic = NextMusic;
            }
        }

        private void SetPlayer()
        {
            if(state != State.NotInit)
            {
                player.PlaybackStopped -= new EventHandler<StoppedEventArgs>(PlaybackStopped);
                player.Stop();
            }
            PlayerThumbnail.Source = new BitmapImage(new Uri(this.currentMusic.Thumbnail));
            JObject audioFmt = Youtube.getAudioFmt(this.currentMusic.WatchUrl);
            MediaFoundationReader outputStream 
                = new MediaFoundationReader(audioFmt["url"].ToString());
            volumeStream = new WaveChannel32(outputStream);
            volumeStream.PadWithZeroes = false;
            player.PlaybackStopped += new EventHandler<StoppedEventArgs>(PlaybackStopped);
            player.Init(volumeStream);
            setTrackBarPosition();
        }
        
        private void Play()
        {
            player.Play();
            state = State.Running;
            playOrStopImage.Source = new BitmapImage(new Uri(@"\image\Stop.png", UriKind.Relative));
        }
        private void Pause()
        {
            player.Pause();
            state = State.Paused;
            playOrStopImage.Source = new BitmapImage(new Uri(@"\image\PlayButton.png", UriKind.Relative));
        }
        private void PlayOrStopImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(state == State.NotInit)
            {
                return;
            }
            else if(state == State.Running)
            {
                Pause();
            }
            else if(state == State.Stopped)
            {
                CurrentMusic = currentMusic;
            }
            else
            {
                Play();
            }
        }

        private void PlayNextImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentMusic = NextMusic;
        }

        private void PlayBackImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentMusic = PreviousMusic;
        }

        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void TrackBarPositionChanged(object sender, EventArgs e)
        {
            DataEventArgs args = e as DataEventArgs;
            double max = (double)args.Data2;
            double position = (double)args.Data1;
            double ratio = position / max;

            if(state != State.NotInit)
            {
                TimeSpan playTime = volumeStream.TotalTime;
                double currentTimeSeconds = playTime.TotalSeconds * ratio * 10000000;
                TimeSpan currentTime = new TimeSpan((long)currentTimeSeconds);
                Debug.WriteLine(currentTime);

                volumeStream.CurrentTime =
                    currentTime;
            }
        }
        private void setTrackBarPosition()
        {

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    double currentTime = volumeStream.CurrentTime.TotalSeconds;
                    double totalTime = volumeStream.TotalTime.TotalSeconds;
                    if (currentTime > totalTime)
                        break;
                    double ratio = currentTime / totalTime;
                    double pinPosition = ratio * (playTimeTrackBar.Max - playTimeTrackBar.Min);
                    playTimeTrackBar.Dispatcher.Invoke(new Action(() =>
                    {
                        if(!playTimeTrackBar.pinCliked)
                            playTimeTrackBar.setPosition(new Point(pinPosition, 0));
                        playTimeTrackBar.setTime(volumeStream.CurrentTime
                            , volumeStream.TotalTime);
                    }));
                    Thread.Sleep(100);
                }
            });
            thread.Start();
        }

        private void LoopImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(loopState == LoopState.NotLoop)
            {
                loopState = LoopState.LoopAll;
                loopImage.Source = new BitmapImage(new Uri(@"\Image\LoopAll.png", UriKind.Relative));
            }
            else if(loopState == LoopState.LoopAll)
            {
                loopState = LoopState.LoopOne;
                loopImage.Source = new BitmapImage(new Uri(@"\Image\LoopOne.png", UriKind.Relative));
            }
            else
            {
                loopState = LoopState.NotLoop;
                loopImage.Source = new BitmapImage(new Uri(@"\Image\Loop.png", UriKind.Relative));
            }
        }

        private void RandomImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(randomState == RandomState.Random)
            {
                randomState = RandomState.UnRandom;
                randomImage.Source 
                    = new BitmapImage(new Uri(@"\Image\UnRandom.png", UriKind.Relative));
            }
            else
            {
                randomState = RandomState.Random;
                randomImage.Source
                    = new BitmapImage(new Uri(@"\Image\Random.png", UriKind.Relative));
            }
        }
    }
}
