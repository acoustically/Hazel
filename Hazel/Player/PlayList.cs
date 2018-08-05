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
        private static List<int> order = new List<int>();
        private static object _syncLock = new object();
        static void sync()
        {
            BindingOperations.EnableCollectionSynchronization(list, _syncLock);
        }
        public static void addItem(YoutubeSearchItem item)
        {
            lock (_syncLock)
            {
                order.Add(list.Count);
                Shuffle();
                list.Add(item);
            }
        }
        public static void removeItem(YoutubeSearchItem item)
        {
            lock (_syncLock)
            {
                list.Remove(item);
                order.Clear();
                for(int i = 0; i < list.Count; i++)
                {
                    order.Add(i);
                }
                Shuffle();
            }
        }
        private static void Shuffle(int time = 1)
        {
            for(int t = 0; t < time; t++) {
                for(int i = 0; i < order.Count; i++)
                {
                    Random random = new Random();
                    int index = random.Next(0, order.Count);
                    int temp = order[i];
                    order[i] = order[index];
                    order[index] = temp;
                }
            }
           
        }

        public static int Count
        {
            get => list.Count;
        }

        public static ObservableCollection<YoutubeSearchItem> List
        {
            get => list;
        }

        public static List<int> Order
        {
            get => order;
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
