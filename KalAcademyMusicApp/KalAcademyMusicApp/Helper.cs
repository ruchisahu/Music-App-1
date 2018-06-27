using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using KalAcademyMusicApp.Models;

namespace KalAcademyMusicApp
{
    public class Helper
    {
        public async Task SerializeDataToJson(Playlist playlist, string path)
        {
            try
            {
                var musicFolder = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music);
                var file = await musicFolder.SaveFolder.CreateFileAsync(path, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                using (StreamWriter r = new StreamWriter(data))
                {
                    var serializedfile = JsonConvert.SerializeObject(playlist);
                    r.Write(serializedfile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<Playlist> ReadPlaylist(string fileName)
        {
            try
            {
                var musicFolder = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music);
                string playlistPath = Path.Combine(musicFolder.SaveFolder.Path, fileName);

                var file = await musicFolder.SaveFolder.GetFileAsync(fileName);
                var data = await file.OpenReadAsync();

                using (StreamReader fileReader = new StreamReader(data.AsStream()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Playlist playList = serializer.Deserialize(fileReader, typeof(Playlist)) as Playlist;
                    return playList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
