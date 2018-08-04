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
    /// <summary>
    /// PlayList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayListPage : UserControl
    {
        public event EventHandler addMusicImageClick;
        public event EventHandler playListDoubleClick;

        public PlayListPage()
        {
            InitializeComponent();
            PlayListBox.ItemsSource = PlayList.List;
        }

        public void refreshPlayList()
        {
        }

        private void AddMusicImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            addMusicImageClick?.Invoke(this, EventArgs.Empty);
        }

        private void PlayListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try {
                ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
                YoutubeSearchItem currentMusic = item.Content as YoutubeSearchItem;
                DataEventArgs args = new DataEventArgs();
                args.Data1 = currentMusic;
                playListDoubleClick?.Invoke(this, args);
            } catch (Exception)
            {
                
            }
        }

        private void IconMouseEnter(object sender, MouseEventArgs e)
        {
            if(((Image)e.Source).Name == "addMusicImage")
            {
                addMusicImage.Source 
                    = new BitmapImage(new Uri(@"\Image\AddMusicEnter.png", UriKind.Relative));
            }
            else
            {
                searchMusicImage.Source 
                    = new BitmapImage(new Uri(@"\Image\SearchMusicEnter.png", UriKind.Relative));
            }
            Mouse.OverrideCursor = Cursors.Hand;
       }

        private void IconMouseLeave(object sender, MouseEventArgs e)
        {
            if(((Image)e.Source).Name == "addMusicImage")
            {
                addMusicImage.Source 
                    = new BitmapImage(new Uri(@"\Image\AddMusic.png", UriKind.Relative));
            }
            else
            {
                searchMusicImage.Source 
                    = new BitmapImage(new Uri(@"\Image\SearchMusic.png", UriKind.Relative));
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            searchTextBoxBorder.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
        }

        private void SearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            searchTextBoxBorder.BorderBrush = new SolidColorBrush(Colors.DimGray);
        }
    }
}
