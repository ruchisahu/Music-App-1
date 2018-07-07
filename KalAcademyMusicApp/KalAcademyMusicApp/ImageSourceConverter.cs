using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

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
                    string imageFilename = (string)value;
                    return Helper.GetImage(imageFilename);
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
