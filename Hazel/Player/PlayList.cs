using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazel.Player
{
    static class PlayList
    {
        static private List<YoutubeSearchItem> list = new List<YoutubeSearchItem>();
        static private YoutubeSearchItem currentMusic;
        public static void addItem(YoutubeSearchItem item)
        {
            list.Add(item);
        }
        public static List<YoutubeSearchItem> List
        {
            get => list;
        }
        public static YoutubeSearchItem CurrentMusic
        {
            get => currentMusic;
            set => currentMusic = value;
        }
    }
}
