using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalAcademyMusicApp.Models
{
    public class Song
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Artist { get; private set; }
        public string SongImagePath { get; private set; }
        public string MusicMp3Path { get; private set; }

        public Song(int id, string name, string artist,string imagepath,string mp3path)
        {
            Id = id;
            Name = name;
            Artist = artist;
            SongImagePath = imagepath;
            MusicMp3Path = mp3path;
        }
    }
}
