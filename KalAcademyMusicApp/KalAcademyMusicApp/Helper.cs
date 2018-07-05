using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using KalAcademyMusicApp.Models;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace KalAcademyMusicApp
{
    public class Helper
    {
        public static async Task WriteDataToJson(Playlist playlist, string path)
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

        public Playlist ReadDataJson(string fileName)
        {
            var musicFolder = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music).AsTask().Result;
            var file = musicFolder.SaveFolder.GetFileAsync(fileName).AsTask().Result;
            var data = file.OpenReadAsync().AsTask().Result;

            using (StreamReader fileReader = new StreamReader(data.AsStream()))
            {
                JsonSerializer serializer = new JsonSerializer();
                Playlist playList = serializer.Deserialize(fileReader, typeof(Playlist)) as Playlist;
                return playList;
            }
        }

        public static async Task<BitmapImage> ConvertStorageToImage(StorageFile savedStorageFile)
        {
            using (IRandomAccessStream fileStream = await savedStorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }
    }
}
