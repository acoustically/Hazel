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
            string part = "snippet";

            string responseText = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BuildQueryaUrl(part, key, keyword));
            request.Method = "GET";
            request.Timeout = 10000;
            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
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
