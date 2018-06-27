using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalAcademyMusicApp.Models;

namespace KalAcademyMusicApp.ViewModels
{
    public class DataAccess
    {
        public void SavePlaylist(Playlist playlist, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, playlist);
            }
        }

        public Playlist ReadPlaylist(string path)
        {
            using (StreamReader file = new StreamReader(File.OpenRead(path)))
            {
                JsonSerializer serializer = new JsonSerializer();
                Playlist playList = serializer.Deserialize(file, typeof(Playlist)) as Playlist;
                return playList;
            }
        }
    }
}
