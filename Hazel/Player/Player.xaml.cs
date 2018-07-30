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
    /// Player.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Player : UserControl
    {
        private YoutubeSearchItem currentMusic;
        public Player()
        {
            InitializeComponent();
        }
        public YoutubeSearchItem CurrentMusic
        {
            get => this.currentMusic;
            set {
                this.currentMusic = value;
                PlayerThumbnail.Source = new BitmapImage(new Uri(this.currentMusic.Thumbnail));
            }
        }
    }
}
