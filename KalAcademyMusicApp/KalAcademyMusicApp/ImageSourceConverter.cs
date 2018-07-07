using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace KalAcademyMusicApp
{
    public class ImageSourceConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        //Takes the string and assumes it is a path to an image, then gets the image  and sets it as the source in the MainPage
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string)
                {
                    string str = (string)value;
                    if (string.IsNullOrEmpty(str))
                    {
                        var image = new BitmapImage();
                        image.UriSource = new Uri("ms-appx:///Assets/StoreLogo.scale-400.png");
                        return image;
                    }
                    else
                    {
                        var musicFolder = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music).AsTask().Result;

                        var file = musicFolder.SaveFolder.GetFileAsync(str).AsTask().Result;
                        using (var stream = file.OpenAsync(Windows.Storage.FileAccessMode.Read).AsTask().Result)
                        {
                            var image = new BitmapImage();
                            image.SetSource(stream);
                            return image;
                        }
                    }
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
