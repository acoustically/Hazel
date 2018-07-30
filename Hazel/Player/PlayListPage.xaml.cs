using System;
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
        public event EventHandler AddMusicImageClicked;
        public event EventHandler PlayListClicked;

        public PlayListPage()
        {
            InitializeComponent();
        }

        private void PlayListPageLoaded(object sender, RoutedEventArgs e)
        {
            PlayListBox.ItemsSource = PlayList.List;
        }

        private void AddMusicImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            AddMusicImageClicked?.Invoke(this, EventArgs.Empty);
        }

        private void PlayListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            YoutubeSearchItem currentMusic = item.Content as YoutubeSearchItem;
            PlayList.CurrentMusic = currentMusic;
        }
    }
}
