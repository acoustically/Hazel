using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hazel.Player
{
    public class YoutubeSearchItemList : ObservableCollection<YoutubeSearchItem> { }
    public class YoutubeSearchItem
    {
        private String videoId;
        private String title;
        private String channel;
        private String thumbnail;
        private SolidColorBrush background;

        public YoutubeSearchItem(string videoId, string title, string channel, string thumbnail)
        {
            this.videoId = videoId;
            this.title = title;
            this.channel = channel;
            this.thumbnail = thumbnail;
            this.background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
        }

        public SolidColorBrush Background
        {
            get => this.background;
            set => this.background = value;
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
            return ToJson().ToString();
        }
        public JObject ToJson()
        {
            JObject json = new JObject();
            json.Add("videoId", videoId);
            json.Add("title", title);
            json.Add("channel", channel);
            json.Add("thumbnail", thumbnail);
            return json;
        }
    }
}
