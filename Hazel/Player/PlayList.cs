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
        private static ObservableCollection<YoutubeSearchItem> randomList = new ObservableCollection<YoutubeSearchItem>();
        private static object _syncLock = new object();
        static void sync()
        {
            BindingOperations.EnableCollectionSynchronization(list, _syncLock);
        }
        public static void addItem(YoutubeSearchItem item)
        {
            lock (_syncLock)
            {
                Shuffle();
                list.Add(item);
                randomList.Add(item);
            }
        }
        public static void removeItem(YoutubeSearchItem item)
        {
            lock (_syncLock)
            {
                list.Remove(item);
                Shuffle();
            }
        }
        public static void randomSwap(YoutubeSearchItem item)
        {
            Shuffle();
            int index = randomList.IndexOf(item);
            YoutubeSearchItem temp = randomList[index];
            randomList[index] = randomList[0];
            randomList[0] = temp;
        }
        private static void Shuffle(int time = 1)
        {
            for(int t = 0; t < time; t++) {
                for(int i = 0; i < randomList.Count; i++)
                {
                    Random random = new Random();
                    int index = random.Next(0, randomList.Count);
                    YoutubeSearchItem temp = randomList[i];
                    randomList[i] = randomList[index];
                    randomList[index] = temp;
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
        public static ObservableCollection<YoutubeSearchItem> RandomList
        {
            get => randomList;
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
