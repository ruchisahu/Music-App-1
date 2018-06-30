using System;
using System.Collections.Generic;
using System.Linq;
using KalAcademyMusicApp.Models;

namespace KalAcademyMusicApp
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Songs = new Playlist();
        }

        public Playlist Songs { get; set; }

        public void InitializeFromFile(string playlist)
        {
            Songs = DataAccess.InitializeFromFile(playlist);
        }
        public List<Song> GetMySongs()
        {
            return Songs?.Where(item => item.IsFavorite).ToList();
        }

        /// <summary>
        /// This method will search by name of the song or artist name
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public List<Song> SearchAllSongsByNameOrArtist(string searchQuery)
        {
            return Songs.Where(item => item.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                       item.Artist.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        /// <summary>
        /// This method will search for a song by name of the song or by name of the artist from mycollection
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public List<Song> SearchMySongs(string searchQuery)
        {
            return Songs.Where(item => item.IsFavorite &&
                                (item.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                 item.Artist.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))).ToList();
        }
    }
}
