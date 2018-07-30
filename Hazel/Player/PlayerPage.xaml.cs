using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool isTitleBarCliked = false;
        private Point currentPosition = new Point();
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
            playListPage.refreshPlayList();
        }
        
        private void PlayListDoubleClick(object sender, EventArgs e)
        {
            Player.CurrentMusic = (e as DataEventArgs).Data1 as YoutubeSearchItem;
        }

        private void WindowCloseImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = Application.Current.MainWindow as MainWindow;
            if(window != null)
            {
                window.Close();
            }
        }

        private void WindowCloseImageMouseEnter(object sender, MouseEventArgs e)
        {
            windowCloseImage.Background = new SolidColorBrush(Colors.OrangeRed);
        }

        private void WindowCloseImageMouseLeave(object sender, MouseEventArgs e)
        {
            windowCloseImage.Background = new SolidColorBrush(Colors.White);
        }

        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            isTitleBarCliked = true;
            currentPosition = PointToScreen(e.GetPosition(this));
            MainWindow window = Application.Current.MainWindow as MainWindow;
            window.DragMove();
        }
    }
}
