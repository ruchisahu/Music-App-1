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
        List<Song> SongCollection = new List<Song>();
        User user;
        
        public DataAccess()
        {
            //Song Collection
            SongCollection.Add(new Song(id: 1, name: "Hello", artist: "Adele", imagepath: "SongImages/Adele.jpg",mp3path: "ms-appx:///Songmp3/Hello.mp3"));
            SongCollection.Add(new Song(id: 2, name: "Can'tStopTheFeeling", artist: "Justin TimberLake", imagepath: "SongImages/justintimberlake.jpg",mp3path: "ms-appx:///Songmp3/Can'tStopTheFeeling.mp3"));
            SongCollection.Add(new Song(id: 3, name: "HomeTown", artist: "Kane Brown", imagepath: "SongImages/KaneBrown.jpg",mp3path: "ms-appx:///Songmp3/HomeTown.mp3"));
            SongCollection.Add(new Song(id: 4, name: "WakaWaka", artist: "Shakira", imagepath: "SongImages/shakira.jpg",mp3path: "ms-appx:///Songmp3/WakaWaka.mp3"));
            SongCollection.Add(new Song(id: 5, name: "Wonderwall", artist: "Oasis", imagepath: "SongImages/oasis.jpg",mp3path: "ms-appx:///Songmp3/Wonderwall.mp3"));

            //User Object
            user = new User(1, "John", "xyz");
            //User user1 = new User(2, "Amy", "abc");
        }
        #region Method
        /// <summary>
        /// This method will return collection of song for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Song> GetSongsForUser(User user)
        {
            return SongCollection;
        }
        /// <summary>
        /// This Method will search songs by name from Song collection
        /// </summary>
        /// <returns></returns>
        /// <param name="searchQuery"></param>
        public List<Song> GetSongByName(String searchQuery)
        {
            ////List<Song> songCollectionByName = SongCollection.Where(item => nameOfTheSong.Any(stringToCheck =>
            ////item.Name.Contains(stringToCheck))).ToList();

            return SongCollection
                        .Where(item => (item.Name.ToLower().Contains(searchQuery.ToLower()) == true) ||
                                       (item.Artist.ToLower().Contains(searchQuery.ToLower()) == true))
                        .ToList();

            //return songCollectionByName;
        }
        public User GetLoggedInUser()
        {
            return user;
        }
        /// <summary>
        /// This method will add a selected song to a specific user's favoritesongs collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="song"></param>
        public void AddSongToFavorite(User u, Song song)
        {
            u.FavoriteSongs.Add(song);
        }
        /// <summary
        /// This method will delete a selected song from a specific user's favoritesongs collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="song"></param>
        public void DeleteSongFromFavorites(User u, Song song)
        {
            user.FavoriteSongs.Remove(song);
        }
        #endregion
    }
}
