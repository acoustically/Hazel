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
                String watchHtml = HttpRequest.OpenUrl(watchUrl);
                String pattern = @";ytplayer\.config\s*=\s*({.*?});";

                String doc = Pattern.match(pattern, watchHtml);
                Debug.WriteLine(doc);
                doc = doc.Substring(doc.IndexOf("{"));
                doc = doc.Substring(0, doc.Length - 1);
                JObject json = JObject.Parse(doc);
                String[] adaptiveFmts = json["args"]["adaptive_fmts"].ToString().Split(',');
                List<String> audioFmts = getAudioFmts(adaptiveFmts);
                foreach (String fmt in audioFmts)
                {
                    JObject infos = DescrambleFmt(fmt);
                    Debug.Write(infos.ToString());
                    Debug.Write("==============================================");
                }
            }
        }
        private List<String> getAudioFmts(String[] adaptiveFmts)
        {
            List<String> audioFmts = new List<string>();
            foreach(String fmt in adaptiveFmts)
            {
                if(fmt.Contains("type=audio"))
                {
                    audioFmts.Add(fmt);
                }
            }
            return audioFmts;
        }
        private JObject DescrambleFmt(String fmt)
        {
            String[] infos = fmt.Split('&');
            JObject json = new JObject();
            foreach(String info in infos)
            {
                String[] keyValue = info.Split('=');
                String key = keyValue[0];
                String value = keyValue[1];
                json.Add(key, value);
            }
            return json;
        }
    }
}
