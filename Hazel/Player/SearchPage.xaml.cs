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
            String keyword = KeywordTextBox.Text;
            String key = Preference.youtebeApiKey;
            String part = "id, snippet";
            String url = BuildQueryaUrl(part, key, keyword);

            String searchResult = HttpRequest.OpenUrl(url);
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
        private String BuildQueryaUrl(String part, String key, String keyword)
        {
            String url = "https://www.googleapis.com/youtube/v3/search/";
            String query = url + "?part=" + part + "&key=" + key + "&q=" + keyword;
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
            Image image = e.Source as Image;
            if(image.Name == "listImage")
            {
                image.Source 
                    = new BitmapImage(new Uri(@"\Image\ListEnter.png", UriKind.Relative));
            }
            else
            {
                image.Source 
                    = new BitmapImage(new Uri(@"\Image\YoutubeSearchMusicEnter.png"
                        , UriKind.Relative));
            }
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            Image image = e.Source as Image;
            if(image.Name == "listImage")
            {
                image.Source 
                    = new BitmapImage(new Uri(@"\Image\List.png", UriKind.Relative));
            }
            else
            {
                image.Source 
                    = new BitmapImage(new Uri(@"\Image\SearchMusic.png"
                        , UriKind.Relative));
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
