using KalAcademyMusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalAcademyMusicApp
{
    public class DataAccess
    {
        List<Song> AllSongCollection = new List<Song>();
        List<Song> MySongCollection = new List<Song>();
        
        
        public DataAccess()
        {
            //Song Collection
            AllSongCollection.Add(new Song(id: 1, name: "Hello", artist: "Adele", imagepath: "SongImages/Adele.jpg",mp3path: "ms-appx:///Songmp3/Hello.mp3"));
            AllSongCollection.Add(new Song(id: 2, name: "Can'tStopTheFeeling", artist: "Justin TimberLake", imagepath: "SongImages/justintimberlake.jpg",mp3path: "ms-appx:///Songmp3/Can'tStopTheFeeling.mp3"));
            AllSongCollection.Add(new Song(id: 3, name: "HomeTown", artist: "Kane Brown", imagepath: "SongImages/KaneBrown.jpg",mp3path: "ms-appx:///Songmp3/HomeTown.mp3"));
            AllSongCollection.Add(new Song(id: 4, name: "WakaWaka", artist: "Shakira", imagepath: "SongImages/shakira.jpg",mp3path: "ms-appx:///Songmp3/WakaWaka.mp3"));
            AllSongCollection.Add(new Song(id: 5, name: "Wonderwall", artist: "Oasis", imagepath: "SongImages/oasis.jpg",mp3path: "ms-appx:///Songmp3/Wonderwall.mp3"));
        }
        #region Method
        /// <summary>
        /// This method will return collection of song for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Song> GetAllSongs()
        {
            return AllSongCollection;
        }
        /// <summary>
        /// This Method will return songs by from my collection
        /// </summary>
        /// <returns></returns>
        /// <param name="searchQuery"></param>
        public List<Song>GetMySongs()
        {
            return MySongCollection;
        }
        /// <summary>
        /// This method will search in the Global selection by name of the song or artist name
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public List<Song> SearchAllSongsByNameOrArtist(String searchQuery)
        {
            return AllSongCollection
                        .Where(item => (item.Name.ToLower().Contains(searchQuery.ToLower()) == true) ||
                                       (item.Artist.ToLower().Contains(searchQuery.ToLower()) == true))
                        .ToList();
        }
        /// <summary>
        /// This method will search for a song by name of the song or by name of the artist from mycollection
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public List<Song>SearchMySongs(string searchQuery)
        {
            return MySongCollection
                        .Where(item => (item.Name.ToLower().Contains(searchQuery.ToLower()) == true) ||
                                       (item.Artist.ToLower().Contains(searchQuery.ToLower()) == true))
                        .ToList();
        }
        
        /// <summary>
        /// This method will add a selected song to my favorite songs collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="song"></param>
        public void AddSongToFavorite(Song song)
        {
            MySongCollection.Add(song);
        }
        /// <summary
        /// This method will delete a selected song from my favorite songs collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="song"></param>
        public void DeleteSongFromFavorites(Song song)
        {
            MySongCollection.Add(song);
        }
        #endregion
    }
}
