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
                new Song { Name = "Closer", Artist = "The Chainsmokers Ft. Halsey", Album = "Closer", SongImagePath="path1", MusicMp3Path="image1" },
                new Song { Name = "Rendezvous", Artist = "Craig David", Album = "Born To Do It", SongImagePath="path2", MusicMp3Path="image2" },
                new Song { Name = "Change Clothes ft. Pharrell", Artist = "Jay Z", Album = "The Black Album", SongImagePath="path3", MusicMp3Path="image3" },
                new Song { Name = "Up & Up", Artist = "Coldplay", Album = "A Head Full of Dreams", SongImagePath="path4", MusicMp3Path="image4" },
                new Song { Name = "Can't Stop The Feeling!", Album = "Trolls", SongImagePath="path5", MusicMp3Path="image5" },
                new Song { Name = "Singles", Artist = "Bruno Mars", Album = "24K Magic", SongImagePath="path6", MusicMp3Path="image6" }
               };
        }

        public Playlist Songs { get; set; }
    }
}
