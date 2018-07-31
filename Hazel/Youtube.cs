using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazel
{
    public class Youtube
    {
        public static JObject getAudioFmt(String watchUrl)
        {
            String watchHtml = HttpRequest.OpenUrl(watchUrl);
            String pattern = @";ytplayer\.config\s*=\s*({.*?});";

            String doc = Pattern.match(pattern, watchHtml);
            doc = doc.Substring(doc.IndexOf("{"));
            doc = doc.Substring(0, doc.Length - 1);
            JObject json = JObject.Parse(doc);
            String[] adaptiveFmts = json["args"]["adaptive_fmts"].ToString().Split(',');
            List<String> audioFmts = Youtube.getAudioFmts(adaptiveFmts);
            JObject infos = DescrambleFmt(audioFmts[0]);
            return infos;
        }

        private static List<String> getAudioFmts(String[] adaptiveFmts)
        {
            List<String> audioFmts = new List<string>();
            foreach (String fmt in adaptiveFmts)
            {
                if (fmt.Contains("type=audio"))
                {
                    audioFmts.Add(fmt);
                }
            }
            return audioFmts;
        }

        private static JObject DescrambleFmt(String fmt)
        {
            String[] infos = fmt.Split('&');
            JObject json = new JObject();
            foreach (String info in infos)
            {
                String[] keyValue = info.Split('=');
                String key = keyValue[0];
                String value = DecodeUrl(keyValue[1]);
                if (value.Contains("codecs=\\"))
                {
                    String[] type = value.Split(';');
                    value = type[0];
                    String codec = type[1].Split('=')[1];
                    codec = codec.Substring(1, codec.Length - 2);
                    json.Add("codec", codec);
                }
                json.Add(key, value);
            }
            return json;
        }

        private static String DecodeUrl(String text)
        {
            text = text.Replace("%25", "%");
            text = text.Replace("%2F", "/");
            text = text.Replace("%3A", ":");
            text = text.Replace("%2C", ",");
            text = text.Replace("%3D", "=");
            text = text.Replace("%3F", "?");
            text = text.Replace("%26", "&");
            text = text.Replace("%3B", ";");
            text = text.Replace("%22", "\\");
            return text;
        }
    }
}
