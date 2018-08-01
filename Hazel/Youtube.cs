using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Hazel
{
    public class Youtube
    {
        public static JObject getAudioFmt(String watchUrl)
        {
            String watchHtml = HttpRequest.OpenUrl(watchUrl);
            String pattern = @";ytplayer\.config\s*=\s*({.*?});";

            String doc = Pattern.Match(pattern, watchHtml);
            doc = doc.Substring(doc.IndexOf("{"));
            doc = doc.Substring(0, doc.Length - 1);
            JObject json = JObject.Parse(doc);
            String baseJsUrl = "https://youtube.com" + json["assets"]["js"].ToString();
            String[] adaptiveFmts = json["args"]["adaptive_fmts"].ToString().Split(',');
            List<String> audioFmts = Youtube.getAudioFmts(adaptiveFmts);

            String signature = getSignature(baseJsUrl);

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

        private static String getSignateFunctionName(String baseJs)
        {
            String pattern = "\"signature\",\\s?([a-zA-Z0-9$]+)\\(";
            String signateFucntionName = Pattern.Match(pattern, baseJs);
            signateFucntionName = signateFucntionName.Split(',')[1];
            signateFucntionName = signateFucntionName.Substring(0, signateFucntionName.Length - 1);
            return signateFucntionName;
        }


        private static List<String> getSignateKeys(String baseJs)
        {
            String pattern = getSignateFunctionName(baseJs) + "=function\\(\\w\\){[a-z=\\.\\(\\\"\\)]*;(.*);(?:.+)}";
            String fuction = Pattern.Match(pattern, baseJs);
            String[] signateKey = fuction.Split(';');
            return signateKey.ToList().GetRange(1, signateKey.Length - 2);
        }

        private static String[] getSignateFunctions(String baseJs, String key)
        {
            String pattern = "var " + key + "={(.*?)};";
            String signateObject = Pattern.Match(pattern, baseJs, RegexOptions.Singleline);
            signateObject = signateObject.Substring(8);
            signateObject = signateObject.Substring(0, signateObject.Length - 2);
            String[] separator = { ",\n" };
            return signateObject.Split(separator, StringSplitOptions.None);
        }

        private static String getSignature(String baseJsUrl)
        {
            String baseJs = HttpRequest.OpenUrl(baseJsUrl);
            List<String> signateKeys = getSignateKeys(baseJs);
            String signateKey = signateKeys[0].Split('.')[0];
            String[] signateFunctions = getSignateFunctions(baseJs, signateKey);
            Debug.WriteLine(String.Join("/", signateFunctions));

            return "Test";
        }
    }
}
