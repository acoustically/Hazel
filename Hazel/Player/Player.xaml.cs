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
    
    enum State { Stoped, Running, NotInit }

    public partial class Player : UserControl
    {
        private WaveOutEvent player;
        private State state;
        private YoutubeSearchItem currentMusic;

        public Player()
        {
            InitializeComponent();
            state = State.NotInit;
            player = new WaveOutEvent();
        }

        public YoutubeSearchItem CurrentMusic
        {
            get => this.currentMusic;
            set {
                this.currentMusic = value;
                SetPlayer();
                Play();
            }
        }

        public YoutubeSearchItem NextMusic
        {
            get
            {
                int index = PlayList.List.IndexOf(currentMusic);
                int nextIndex;
                if (index + 1 < PlayList.Count)
                {
                    nextIndex = index + 1;
                }
                else
                {
                    nextIndex = 0;
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
            CurrentMusic = NextMusic;
            Debug.WriteLine("test");
            Debug.WriteLine(CurrentMusic);
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
            WaveChannel32 volumeStream = new WaveChannel32(outputStream);
            volumeStream.PadWithZeroes = false;
            player.PlaybackStopped += new EventHandler<StoppedEventArgs>(PlaybackStopped);
            player.Init(volumeStream);
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
            state = State.Stoped;
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
    }
}
