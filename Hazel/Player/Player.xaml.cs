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
    
    enum States { Stopped, Running, NotInit, Paused }
    enum LoopStates { NotLoop, LoopAll, LoopOne }
    enum RandomStates { Random, UnRandom }

    public partial class Player : UserControl
    {
        private WaveOutEvent player;
        private States state;
        private LoopStates loopState;
        private RandomStates randomState;
        public static YoutubeSearchItem currentMusic;
        private WaveChannel32 volumeStream;

        public Player()
        {
            InitializeComponent();
            state = States.NotInit;
            LoadState();
            player = new WaveOutEvent();
        }

        private void SaveState()
        {
            JObject json = new JObject();
            json.Add("loopState", (int)loopState);
            json.Add("randomState", (int)randomState);
            json.Add("index", PlayList.List.IndexOf(currentMusic));
            String state = json.ToString();
            Preference.isStateFileExist();
            File.WriteAllText(Preference.stateFilePath, state);
        }
        
        private void LoadState()
        {
            if (Preference.isStateFileExist())
            {
                String Text = File.ReadAllText(Preference.stateFilePath);
                if(Text.Length != 0)
                {
                    try
                    {
                        JObject json = JObject.Parse(Text);
                        LoopState = (LoopStates)int.Parse(json["loopState"].ToString());
                        RandomState = (RandomStates)int.Parse(json["randomState"].ToString());
                        int index = int.Parse(json["index"].ToString());
                        YoutubeSearchItem music = PlayList.List[index];
                        currentMusic = music;
                        PlayerThumbnail.Source = new BitmapImage(new Uri(currentMusic.Thumbnail));
                        state = States.Stopped;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
           }
            else
            {
                loopState = LoopStates.NotLoop;
                randomState = RandomStates.UnRandom;
            }
        }

        private LoopStates LoopState
        {
            set
            {
                loopState = value;
                if(loopState == LoopStates.NotLoop)
                {
                    loopImage.Source
                        = new BitmapImage(new Uri(@"\Image\Loop.png", UriKind.Relative));
                }
                else if(loopState == LoopStates.LoopAll)
                {
                    loopImage.Source
                        = new BitmapImage(new Uri(@"\Image\LoopAll.png", UriKind.Relative));
                }
                else if(loopState == LoopStates.LoopOne)
                {
                    loopImage.Source
                        = new BitmapImage(new Uri(@"\Image\LoopOne.png", UriKind.Relative));
                }
            }
        }

        private RandomStates RandomState
        {
            set
            {
                randomState = value;
                if(randomState == RandomStates.Random)
                {
                    randomImage.Source
                        = new BitmapImage(new Uri(@"\Image\Random.png", UriKind.Relative));
                }
                else if(randomState == RandomStates.UnRandom)
                {
                    randomImage.Source
                        = new BitmapImage(new Uri(@"\Image\UnRandom.png", UriKind.Relative));
                }
            }
        }

        public YoutubeSearchItem CurrentMusic
        {
            get => currentMusic;
            set {
                currentMusic = value;
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
                if (loopState == LoopStates.LoopOne)
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
            if(loopState == LoopStates.NotLoop)
            {
                int index = PlayList.List.IndexOf(currentMusic);
                if(index + 1 == PlayList.List.Count)
                {
                    playOrStopImage.Source 
                        = new BitmapImage(new Uri(@"\image\PlayButton.png", UriKind.Relative));
                    player.Stop();
                    state = States.Stopped;
                }
            }
            else
            {
                CurrentMusic = NextMusic;
            }
        }

        private void SetPlayer()
        {
            if(state != States.NotInit)
            {
                player.PlaybackStopped -= new EventHandler<StoppedEventArgs>(PlaybackStopped);
                player.Stop();
            }
            PlayerThumbnail.Source = new BitmapImage(new Uri(currentMusic.Thumbnail));
            JObject audioFmt = Youtube.getAudioFmt(currentMusic.WatchUrl);
            MediaFoundationReader outputStream 
                = new MediaFoundationReader(audioFmt["url"].ToString());
            volumeStream = new WaveChannel32(outputStream);
            volumeStream.PadWithZeroes = false;
            player.PlaybackStopped += new EventHandler<StoppedEventArgs>(PlaybackStopped);
            player.Init(volumeStream);
            setTrackBarPosition();
            SaveState();
        }
        
        private void Play()
        {
            player.Play();
            state = States.Running;
            playOrStopImage.Source = new BitmapImage(new Uri(@"\image\Stop.png", UriKind.Relative));
        }
        private void Pause()
        {
            player.Pause();
            state = States.Paused;
            playOrStopImage.Source = new BitmapImage(new Uri(@"\image\PlayButton.png", UriKind.Relative));
        }
        private void PlayOrStopImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(state == States.NotInit)
            {
                return;
            }
            else if(state == States.Running)
            {
                Pause();
            }
            else if(state == States.Stopped)
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

            if(state == States.Running)
            {
                TimeSpan playTime = volumeStream.TotalTime;
                double currentTimeSeconds = playTime.TotalSeconds * ratio * 10000000;
                TimeSpan currentTime = new TimeSpan((long)currentTimeSeconds);
                volumeStream.CurrentTime = currentTime;
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
            if(loopState == LoopStates.NotLoop)
            {
                LoopState = LoopStates.LoopAll;
            }
            else if(loopState == LoopStates.LoopAll)
            {
                LoopState = LoopStates.LoopOne;
            }
            else
            {
                LoopState = LoopStates.NotLoop;
            }
            SaveState();
        }

        private void RandomImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(randomState == RandomStates.Random)
            {
                RandomState = RandomStates.UnRandom;
            }
            else if(randomState == RandomStates.UnRandom)
            {
                RandomState = RandomStates.Random;
            }
            SaveState();
        }
    }
}
