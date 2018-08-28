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
    /// <summary>
    /// PlayList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayListPage : UserControl
    {
        public event EventHandler addMusicImageClick;
        public event EventHandler playListDoubleClick;
        public static ListBox playListBox;

        public PlayListPage()
        {
            InitializeComponent();
            PlayListBox.ItemsSource = PlayList.List;
            playListBox = PlayListBox;
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
                PlayList.randomSwap(currentMusic);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void IconMouseEnter(object sender, MouseEventArgs e)
        {
            addMusicImage.Source 
                = new BitmapImage(new Uri(@"\Image\AddMusicEnter.png", UriKind.Relative));
            Mouse.OverrideCursor = Cursors.Hand;
       }

        private void IconMouseLeave(object sender, MouseEventArgs e)
        {
            addMusicImage.Source 
                = new BitmapImage(new Uri(@"\Image\AddMusic.png", UriKind.Relative));
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void TrashImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            YoutubeSearchItem item = image.DataContext as YoutubeSearchItem;
            if(Player.currentMusic != item)
            {
                PlayList.List.Remove(item);
                PlayList.Save();
            }
            else
            {
                MessageBox.Show("현재 재생중인 곡입니다.");
            }
        }
    }
}
