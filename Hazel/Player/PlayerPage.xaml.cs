﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    enum Pages { PlayListPage, SearchPage }
    /// <summary>
    /// PlayerPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        private void AddMusicImageClick(object sender, EventArgs e)
        {
            playListPage.Visibility = Visibility.Collapsed;
            youtubeSearchPage.Visibility = Visibility.Visible;
        }

        private void MusicListImageClick(object sender, EventArgs e)
        {
            youtubeSearchPage.Visibility = Visibility.Collapsed;
            playListPage.Visibility = Visibility.Visible;
        }
        
        private void PlayListDoubleClick(object sender, EventArgs e)
        {
            Player.CurrentMusic = (e as DataEventArgs).Data1 as YoutubeSearchItem;
        }
    }
}
