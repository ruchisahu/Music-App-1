using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalAcademyMusicApp.Models;

namespace KalAcademyMusicApp
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Songs = GetSongsList();
        }

        public Playlist GetSongsList()
        {
            return new Playlist {
                new Song { Name = "Hello", Artist = "Adele", Album = "Hello", SongImagePath="SongImages/Adele.jpg", MusicMp3Path="ms-appx:///Songmp3/Hello.mp3" },
                new Song { Name = "Can'tStopTheFeeling", Artist = "Justin TimberLake", Album = "The Best", SongImagePath="SongImages/justintimberlake.jpg", MusicMp3Path="ms-appx:///Songmp3/Can'tStopTheFeeling.mp3" },
                new Song { Name = "HomeTown", Artist = "Kane Brown", Album = "The Black Album", SongImagePath="SongImages/KaneBrown.jpg", MusicMp3Path="ms-appx:///Songmp3/HomeTown.mp3" },
                new Song { Name = "WakaWaka", Artist = "Shakira", Album = "Beautiful", SongImagePath="SongImages/shakira.jpg", MusicMp3Path="ms-appx:///Songmp3/WakaWaka.mp3" },
                new Song { Name = "Wonderwall", Artist = "Oasis", Album = "Trolls", SongImagePath="SongImages/oasis.jpg", MusicMp3Path="ms-appx:///Songmp3/Wonderwall.mp3" }
               };

        }

        public Playlist Songs { get; set; }
    }
}
