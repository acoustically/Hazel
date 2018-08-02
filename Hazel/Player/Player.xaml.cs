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
            this.player = new WaveOutEvent();
        }
        public YoutubeSearchItem CurrentMusic
        {
            get => this.currentMusic;
            set {
                this.currentMusic = value;
                PlayerThumbnail.Source = new BitmapImage(new Uri(this.currentMusic.Thumbnail));
                String watchUrl = this.currentMusic.WatchUrl;
                setPlayer(watchUrl);
                Play();
            }
        }

        public void setPlayer(String watchUrl)
        {
            JObject audioFmt = Youtube.getAudioFmt(watchUrl);
            MediaFoundationReader outputStream 
                = new MediaFoundationReader(audioFmt["url"].ToString());
            WaveChannel32 volumeStream = new WaveChannel32(outputStream);
            player.Stop();
            player.Init(volumeStream);
        }
        
        public void Play()
        {
            player.Play();
            state = State.Running;
            playOrStopImage.Source = new BitmapImage(new Uri(@"\image\Stop.png", UriKind.Relative));
        }
        public void Pause()
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
    }
}
