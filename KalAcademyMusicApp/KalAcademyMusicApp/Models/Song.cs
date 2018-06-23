using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalAcademyMusicApp.Models
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string SongImagePath { get; set; }
        public string MusicMp3Path { get; set; }
        public bool IsFavorite { get; set; }

        public Song()
        { }

        public Song(string name, string artist, string album, string imagePath, string mp3Path, bool isFavorite)
        {
            Name = name;
            Artist = artist;
            Album = album;
            SongImagePath = imagePath;
            MusicMp3Path = mp3Path;
            IsFavorite = isFavorite;
        }
    }
}
