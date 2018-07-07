using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;

namespace KalAcademyMusicApp.Config
{
    public class AuthHelper
    {
        public static ulong seekLocation = 0;

        // Write a text file to the app's local folder. 
        /*public static async Task<string> Save(Stream photoToSave, string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile photoFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var photoOutputStream = await photoFile.OpenStreamForWriteAsync())
            {
                await photoToSave.CopyToAsync(photoOutputStream);
            }

            return photoFile.Path;
        }*/

        public async Task<string> WriteTextFile(string filename, string contents)
        {
            var localFolder = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music);
            StorageFile textFile = await localFolder.SaveFolder.CreateFileAsync(filename,
               CreationCollisionOption.OpenIfExists);

            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                IOutputStream outputStream = textStream.GetOutputStreamAt(seekLocation);
                using (DataWriter textWriter = new DataWriter(outputStream))
                {
                    textWriter.WriteString(contents);
                    await textWriter.StoreAsync();
                    seekLocation += (ulong)contents.Length;
                }
            }

            return textFile.Path;
        }

        /// <summary>
        /// Read the contents of a text file from the app's local folder , if not found in Music folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<string> ReadTextFile(string filename)
        {
            string contents;
            var localFolder = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music);
            var file = localFolder.SaveFolder.TryGetItemAsync(filename).AsTask().Result;
            var textFile = file as IStorageFile;
            if (textFile == null)
            {
                textFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            }

            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
            {
                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    contents = textReader.ReadString(textLength);
                }
            }

            return contents;
        }
    }
}