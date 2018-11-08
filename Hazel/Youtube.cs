using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using System.IO;
using Jint;

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
            JObject infos = DescrambleFmt(audioFmts[0]);
            if(!infos["url"].ToString().Contains("signature"))
            {
                String signature = getSignature(baseJsUrl, infos["s"].ToString());
                String url = infos["url"].ToString();
                url += "&signature=" + signature;
                infos.Property("url").Remove();
                infos.Add("url", url);
            }
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
            String pattern = @"\bc\s*&&\s*d\.set\([^,]+\s*,\s*\([^)]*\)\s*\(\s*(?<sig>[a-zA-Z0-9$]+)\(";

            String signateFucntionName = Pattern.Match(pattern, baseJs);
            signateFucntionName = signateFucntionName.Split(')')[1];
            signateFucntionName = signateFucntionName.Substring(1, signateFucntionName.Length - 1);
            signateFucntionName = signateFucntionName.Substring(0, 2);
            return signateFucntionName;
        }


        private static List<String> getSignateKeys(String baseJs)
        {
            String pattern = getSignateFunctionName(baseJs) + "=function\\(\\w\\){[a-z=\\.\\(\\\"\\)]*;(.*);(?:.+)}";
            String fuction = Pattern.Match(pattern, baseJs);
            String[] signateKey = fuction.Split(';');
            return signateKey.ToList().GetRange(1, signateKey.Length - 2);
        }

        private static JObject getSignateFunctions(String baseJs, String signateKey)
        {
            String pattern = "var " + signateKey + "={(.*?)};";
            String signateObject = Pattern.Match(pattern, baseJs, RegexOptions.Singleline);
            signateObject = signateObject.Substring(8);
            signateObject = signateObject.Substring(0, signateObject.Length - 2);
            String[] separator = { ",\n" };
            String[] functions = signateObject.Split(separator, StringSplitOptions.None);
            JObject json = new JObject();
            foreach(String function in functions)
            {
                String[] keyValue = function.Split(':');
                String key = keyValue[0];
                String value = keyValue[1];
                json.Add(key, value);
            }
            return json;
        }

        private static String getSignature(String baseJsUrl, String s)
        {
            String baseJs = HttpRequest.OpenUrl(baseJsUrl);
            List<String> signateKeys = getSignateKeys(baseJs);
            String signateKey = signateKeys[0].Split('.')[0];
            JObject signateFunctions = getSignateFunctions(baseJs, signateKey);
            String signature = s;
            String javascript = "var a = '" + s + "'.split('');\n";
            foreach(String key in signateKeys)
            {
                String[] keyParam = splitSignateKey(key);
                String jsFunction = signateFunctions[keyParam[0]].ToString();
                String param = keyParam[1];
                javascript += "(" + jsFunction + ")(a, "+ param +");\n";
            }
            javascript += "var signature = a.join('')";
            return ExecuteJavascript(javascript);
        }
        private static String[] splitSignateKey(String signateKey)
        {
            String[] temp = signateKey.Split('.');
            String key = temp[1].Split('(')[0];
            String param = temp[1].Split(',')[1];
            param = param.Substring(0, param.Length - 1);
            String[] keyParam = { key, param };
            return keyParam;
        }

        private static String ExecuteJavascript(String javascript)
        {
            return new Engine().Execute(javascript).GetValue("signature").ToString();
        }
    }
}
