using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazel
{
    class Preference
    {
        public const string youtebeApiKey = "AIzaSyBc6lLc0pwu6cR1dzuxaK5MwEqvla1GT44";
        public static string homeDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string playListFilePath = Preference.homeDirectoryPath + "\\playList.ini";
        public static bool isPlayListFileExist()
        {
            if(!File.Exists(playListFilePath)) {
                if (!Directory.Exists(homeDirectoryPath))
                {
                    Directory.CreateDirectory(homeDirectoryPath);
                }
                File.Create(playListFilePath);
                return false;
            } else
            {
                return true;
            }
        }
    }
}
