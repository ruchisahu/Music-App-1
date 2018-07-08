using KalAcademyMusicApp.Models;
using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.IO;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Storage;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KalAcademyMusicApp
{
    public sealed partial class MainPage : Page
    {
        private List<UIElement> mainContentWindowVisibility;
        private List<UIElement> homeViewUIElements;
        private List<UIElement> songEditViewUIElements;

        /// <summary>
        /// the song selected for editing
        /// </summary>
        private Song selectedSong;

        /// <summary>
        /// this is a copy of the selected song to avoid changing it's properties until it is saved
        /// </summary>
        private Song editedSong;

        /// <summary>
        /// This is used to know where to navigate back after editing - home or favorites
        /// </summary>
        private ListBoxItem lastSelectedOption;

        public MainWindowViewModel MainModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            MainModel = new MainWindowViewModel();

            songEditViewUIElements = new List<UIElement> { EditInfoArea };
            homeViewUIElements = new List<UIElement> { SongCollection, Searchby };
            mainContentWindowVisibility = new List<UIElement> { MediaPlayerElement };
            mainContentWindowVisibility.AddRange(homeViewUIElements);
            mainContentWindowVisibility.AddRange(songEditViewUIElements);
            lastSelectedOption = HomeListBoxItem;
            ToggleMainContentWindow(homeViewUIElements);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.MainModel != null && e.NavigationMode != NavigationMode.Back)
            {
                MainModel.InitializeFromFile(@"Playlist.json");
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// On clicling Play button this method will get call 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaySong_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Song s = b.DataContext as Song;

            var musicFolder = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Music).AsTask().Result;

            var musicFile = musicFolder.SaveFolder.GetFileAsync(s.SongPath).AsTask().Result;
            MediaPlayerElement.Source = MediaSource.CreateFromStorageFile(musicFile);

            MediaPlayerElement.PosterSource = Helper.GetImage(s.ImagePath);

            MediaPlayerElement.MediaPlayer.Play();

            //ToggleMainContentWindow(MediaPlayerElement);
            HomeListBoxItem.IsSelected = false;
            MusicPlayerListBoxItem.IsSelected = true;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string searchcontent = t.Text;

            var songs = HomeListBoxItem.IsSelected ? MainModel.SearchAllSongsByNameOrArtist(searchcontent) : MainModel.SearchMySongs(searchcontent);
            //After calling an API we need to rebind GridView with new data(In this case its a collection of songs by name or artist)
            SongCollectionView.ItemsSource = songs;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void LeftMenuOptionSelected(object sender, SelectionChangedEventArgs e)
        {
            // If this is called during app startup because default SelectedIndex is set in xaml markup, all XAML controls aren't
            // initialized yet, so we just skip
            if (SongCollectionView != null)
            {
                //After calling an API we need to rebind GridView with new data.In this case we are refreshing the Gridview with new data

                if (!EditInfoListBoxItem.IsSelected)
                {
                    EditInfoListBoxItem.IsEnabled = false;
                }

                if (HomeListBoxItem.IsSelected)
                {
                    lastSelectedOption = HomeListBoxItem;
                    ToggleMainContentWindow(homeViewUIElements);
                    SongCollectionView.ItemsSource = MainModel.Songs;
                }
                else if (MusicPlayerListBoxItem.IsSelected)
                {
                    ToggleMainContentWindow(MediaPlayerElement);
                }
                else if (MyCollectionListBoxItem.IsSelected)
                {
                    lastSelectedOption = MyCollectionListBoxItem;
                    ToggleMainContentWindow(homeViewUIElements);
                    SongCollectionView.ItemsSource = MainModel.GetMySongs();
                }
                else if (AddSongListBoxItem.IsSelected)
                {
                    await AddMp3File();
                    SongCollectionView.ItemsSource = null;
                    SongCollectionView.ItemsSource = MainModel.Songs;
                }
                else if (EditInfoListBoxItem.IsSelected)
                {
                    EditSongInfo();
                }


                // User may have typed a search query when (s)he was on the other UI, so we clear the text since it is not relavent now.
                tbsearch.Text = "";
            }
        }

        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SymbolIcon icon = (SymbolIcon)button.Content;
            if (button.DataContext is Song song)
            {
                song.IsFavorite = song.IsFavorite ? false : true;
                icon.Symbol = song.IsFavorite ? Symbol.UnFavorite : Symbol.Favorite;
            }

            if (MyCollectionListBoxItem.IsSelected)
            {
                SongCollectionView.ItemsSource = MainModel.GetMySongs();
            }
        }

        //private void ChkAddtoFavorite_Checked(object sender, RoutedEventArgs e)
        //{
        //    CheckBox c = sender as CheckBox;
        //    Song s = c.DataContext as Song;
        //    if (s != null)
        //    {
        //        s.IsFavorite = true;
        //    }
        //}

        //private void ChkAddtoFavorite_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    CheckBox c = sender as CheckBox;
        //    Song s = c.DataContext as Song;
        //    if (s != null)
        //    {
        //        s.IsFavorite = false;
        //        if (MyCollectionListBoxItem.IsSelected == true)
        //        {
        //            var songs = MainModel.GetMySongs();
        //            SongCollectionView.ItemsSource = songs;
        //        }
        //    }
        //    //private async void Button_ClickSave(object sender, RoutedEventArgs e)
        //    //{
        //    //    var helper = new Helper();
        //    //    await helper.SerializeDataToJson(MainModel.Songs, "Playlist.json");

        //    //    MainModel.Songs = await helper.ReadPlaylist("Playlist.json");

        //    //}
        //}

        private void ToggleMainContentWindow(params UIElement[] currentElements)
        {
            mainContentWindowVisibility.ForEach(e => e.Visibility = currentElements.Any(ce => ce == e) ? Visibility.Visible : Visibility.Collapsed);
        }

        private void ToggleMainContentWindow(List<UIElement> visibleElements)
        {
            mainContentWindowVisibility.ForEach(e => e.Visibility = visibleElements.Any(ce => ce == e) ? Visibility.Visible : Visibility.Collapsed);
        }

        private async Task AddMp3File()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var wholePath = file.Path;
                var musicWord = @"Music\";
                string fileName = GetRelativePath(wholePath, musicWord);
                string songName = GetSongName(wholePath);

                MainModel.Songs.Add(new Song(songName, "", "", "", fileName, false));
                await Helper.WriteDataToJson(MainModel.Songs, "Playlist.json");
            }
        }

        private static string GetRelativePath(string wholePath, string musicWord)
        {
            int startIndex = wholePath.IndexOf(musicWord);
            string path = wholePath.Substring(startIndex + musicWord.Length);
            return path;
        }

        private static string GetSongName(string wholePath)
        {
            var fileInfo = new FileInfo(wholePath);
            return fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
        }


        private void EditSongInfo()
        {
            selectedSong = SongCollectionView.SelectedItem as Song;
            if (selectedSong != null)
            {
                editedSong = new Song(selectedSong);

                ToggleMainContentWindow(songEditViewUIElements);

                EditInfoArea_AlbumName.Text = editedSong.Album;
                EditInfoArea_SongName.Text = editedSong.Name;
                EditInfoArea_ArtistName.Text = editedSong.Artist;
                EditInfoArea_AlbumImage.Source = Helper.GetImage(editedSong.ImagePath);
            }
        }

        private async void ChangeAlbumImage(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var wholePath = file.Path;
                var imageWord = @"Pictures\";
                editedSong.ImagePath = GetRelativePath(wholePath, imageWord);
                EditInfoArea_AlbumImage.Source = Helper.GetImage(editedSong.ImagePath);
            }
        }

        private async void SaveSongInfo(object sender, RoutedEventArgs e)
        {
            if (selectedSong != null)
            {
                editedSong.Album = EditInfoArea_AlbumName.Text;
                editedSong.Name = EditInfoArea_SongName.Text;
                editedSong.Artist = EditInfoArea_ArtistName.Text;
                selectedSong.Update(editedSong);
                await Helper.WriteDataToJson(MainModel.Songs, "Playlist.json");
                SongCollectionView.ItemsSource = null;
                SongCollectionView.ItemsSource = MainModel.Songs;
                selectedSong = null;
                editedSong = null;

                // go back
                if (lastSelectedOption != null)
                {
                    var navigateTarget = lastSelectedOption;
                    lastSelectedOption = null;
                    IconsList.SelectedItem = navigateTarget;
                }
            }
        }

        private void SongCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
            {
                EditInfoListBoxItem.IsEnabled = false;
            }
            else
            {
                EditInfoListBoxItem.IsEnabled = gridView.SelectedItem != null;
            }
        }
    }
}