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
        public event EventHandler musicListImageClick;
        public SearchPage()
        {
            InitializeComponent();
        }

        private void MusicListImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            musicListImageClick?.Invoke(sender, EventArgs.Empty);
        }

        private void SearchMusicImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            searchMusic();
        }
        private void searchMusic()
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
                    try
                    {
                        String videoId = youtubeItem["id"]["videoId"].ToString();
                        String title = youtubeItem["snippet"]["title"].ToString();
                        String channel = youtubeItem["snippet"]["channelTitle"].ToString();
                        String thumbnail = youtubeItem["snippet"]["thumbnails"]["high"]["url"].ToString();
                        YoutubeSearchItem youtubeSearchItem = new YoutubeSearchItem(videoId, title, channel, thumbnail);
                        youtubeSearchItems.Add(youtubeSearchItem);
                    } catch(Exception)
                    {
                        continue;
                    }
                }
                youtubeSearchListBox.ItemsSource = youtubeSearchItems;
            }
        }
        private string BuildQueryaUrl(string part, string key, string keyword)
        {
            string url = "https://www.googleapis.com/youtube/v3/search/";
            string query = url + "?part=" + part + "&key=" + key + "&q=" + keyword;
            query += "&type=video&maxResults=50";
            return query;
        }

        private void YoutubeItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            try
            {
                YoutubeSearchItem selectedItem = item.Content as YoutubeSearchItem;
                if (item != null)
                {
                    PlayList.addItem(selectedItem);
                    PlayList.Save();
                    musicListImageClick?.Invoke(sender, EventArgs.Empty);
                }
            }
            catch(Exception)
            {

            }
        }

        private void KeywordTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                searchMusic();
            }
        }

        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            (e.Source as Border).Background = new SolidColorBrush(Colors.LightGray);
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            (e.Source as Border).Background = new SolidColorBrush(Colors.White);
        }
    }
}
