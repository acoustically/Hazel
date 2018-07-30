using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Hazel.Player
{
    static class PlayList
    {
        private static ObservableCollection<YoutubeSearchItem> list = new ObservableCollection<YoutubeSearchItem>();
        private static object _syncLock = new object();
        static void sync()
        {
            BindingOperations.EnableCollectionSynchronization(list, _syncLock);
        }
        public static void addItem(YoutubeSearchItem item)
        {
            lock (_syncLock)
            {
                list.Add(item);
            }
        }
        public static ObservableCollection<YoutubeSearchItem> List
        {
            get => list;
        }
        public static void Load()
        {
            sync();
           
            if (Preference.isPlayListFileExist())
            {
                try
                {
                    String playListString = File.ReadAllText(Preference.playListFilePath);
                    JArray playListsJson = JArray.Parse(playListString);
                    foreach (JObject playListJson in playListsJson)
                    {
                        String videoId = playListJson["videoId"].ToString();
                        String title = playListJson["title"].ToString();
                        String channel = playListJson["channel"].ToString();
                        String thumbnail = playListJson["thumbnail"].ToString();
                        YoutubeSearchItem item = new YoutubeSearchItem(videoId, title, channel, thumbnail);
                        addItem(item);
                    }
                } 
                catch (Exception)
                {
                    MessageBox.Show("플레이 리스트를 불러올 수 없습니다.");
                }
            }
        }
        public static void Save()
        {
            JArray jsonArray = new JArray();
            foreach (YoutubeSearchItem item in list)
            {
                JObject json = new JObject();
                json.Add("videoId", item.VidioId);
                json.Add("title", item.Title);
                json.Add("channel", item.Channel);
                json.Add("thumbnail", item.Thumbnail);
                jsonArray.Add(json);
            }
            String playList = jsonArray.ToString();
            Preference.isPlayListFileExist();
            File.WriteAllText(Preference.playListFilePath, playList);
        }
    }
}
