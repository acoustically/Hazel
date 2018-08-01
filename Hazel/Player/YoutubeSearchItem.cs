using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hazel.Player
{
    public class YoutubeSearchItemList : ObservableCollection<YoutubeSearchItem> { }
    public class YoutubeSearchItem
    {
        private String videoId;
        private String title;
        private String channel;
        private String thumbnail;

        public YoutubeSearchItem(string videoId, string title, string channel, string thumbnail)
        {
            this.videoId = videoId;
            this.title = title;
            this.channel = channel;
            this.thumbnail = thumbnail;
        }
        public String WatchUrl
        {
            get => "https://www.youtube.com/watch?v=" + this.videoId;
        }

        public String EmbedUrl
        {
            get => "http://www.youtube.com/embed/" + this.videoId;
        }

        public String VidioId
        {
            get => this.videoId;
        }

        public String Title
        {
            get => this.title;
        }

        public String Channel
        {
            get => this.channel;
        }

        public String Thumbnail
        {
            get => this.thumbnail;
        }

        public override String ToString()
        {
            return "videoId = " + videoId + " / title = " + title + " / channel = " + channel + " / thumbnail = " + thumbnail;
        }
    }
}
