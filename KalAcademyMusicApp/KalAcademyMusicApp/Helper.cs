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
using Windows.UI.Xaml.Media;

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

        public static Playlist ReadDataJson(string fileName)
        {
            var musicFolder = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music).AsTask().Result;
            var file = musicFolder.SaveFolder.TryGetItemAsync(fileName).AsTask().Result;
            var dataFile = file as IStorageFile;
            if (dataFile == null)
            {
                return new Playlist();
            }
            else
            {
                var data = dataFile.OpenReadAsync().AsTask().Result;

                using (StreamReader fileReader = new StreamReader(data.AsStream()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Playlist playList = serializer.Deserialize(fileReader, typeof(Playlist)) as Playlist;
                    return playList;
                }
            }
        }

        public static async Task<BitmapImage> ConvertStorageToImage(IStorageFile savedStorageFile)
        {
            using (IRandomAccessStream fileStream = await savedStorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }

        public static BitmapImage GetDefaultSongImage()
        {
            var image = new BitmapImage();
            image.UriSource = new Uri("ms-appx:///Assets/StoreLogo.scale-400.png");
            return image;
        }

        public static ImageSource GetImage(string imageFilename)
        {
            if (string.IsNullOrEmpty(imageFilename))
            {
                return GetDefaultSongImage();
            }
            else
            {
                var imageFolder = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures).AsTask().Result;

                var file = imageFolder.SaveFolder.GetFileAsync(imageFilename).AsTask().Result;
                using (var stream = file.OpenAsync(Windows.Storage.FileAccessMode.Read).AsTask().Result)
                {
                    var image = new BitmapImage();
                    image.SetSource(stream);
                    return image;
                }
            }
        }
    }
}
