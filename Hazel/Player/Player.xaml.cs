using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
                String watchUrl = this.currentMusic.WatchUrl;
                WebRequest request = WebRequest.Create(watchUrl);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        String watchHtml = reader.ReadToEnd();
                        String pattern = @";ytplayer\.config\s*=\s*({.*?});";
                        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                        MatchCollection matches = regex.Matches(watchHtml);
                        if(matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                String doc = match.Value.Substring(match.Value.IndexOf("{"));
                                doc = doc.Substring(0, doc.Length - 1);
                                JObject json = JObject.Parse(doc);
                                String[] adaptiveFmts = json["args"]["adaptive_fmts"].ToString().Split(',');
                                Debug.WriteLine(String.Join("\n", adaptiveFmts));
                            }
                        }
                    }
                        
                }

                
            }
        }
    }
}
