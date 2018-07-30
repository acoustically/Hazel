using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// SearchPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchPage : UserControl
    {
        public event EventHandler MusicListImageClicked;
        public SearchPage()
        {
            InitializeComponent();
        }

        private void MusicListImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            MusicListImageClicked?.Invoke(sender, EventArgs.Empty);
        }

        private void SearchMusicImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            string keyword = KeywordTextBox.Text;
            string key = Preference.youtebeApiKey;
            string part = "id, snippet";
            string searchResult = "";        

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BuildQueryaUrl(part, key, keyword));
            request.Method = "GET";
            request.Timeout = 10000;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string status = response.StatusCode.ToString();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    searchResult = reader.ReadToEnd();
                }
                JObject json = JObject.Parse(searchResult);
                JArray youtubeItems = JArray.Parse(json["items"].ToString());
                List<YoutubeSearchItem> youtubeSearchItems = new List<YoutubeSearchItem>();
                foreach (JObject youtubeItem in youtubeItems)
                {
                    String videoId = youtubeItem["id"]["videoId"].ToString();
                    String title = youtubeItem["snippet"]["title"].ToString();
                    String channel = youtubeItem["snippet"]["channelTitle"].ToString();
                    String thumbnail = youtubeItem["snippet"]["thumbnails"]["high"]["url"].ToString();
                    YoutubeSearchItem youtubeSearchItem = new YoutubeSearchItem(videoId, title, channel, thumbnail);
                    youtubeSearchItems.Add(youtubeSearchItem);
                }
                
                MessageBox.Show(String.Join("\n", youtubeSearchItems));
            }
        }
        private string BuildQueryaUrl(string part, string key, string keyword)
        {
            string url = "https://www.googleapis.com/youtube/v3/search";
            string query = url + "?part=" + part + "&key=" + key + "&q=" + keyword;
            return query;
        }
    }
}
