using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace KalAcademyMusicApp.Models
{
    class Album
    {
        //minor change
        public Image Image { get; private set; }
        public string Name { get; private set; }
        public List<Song> CollectedSongs { get; private set; }

        public Album(Image image, string name)
        {
            Image = image;
            Name = name;
        }
    }
}
