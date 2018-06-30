﻿using KalAcademyMusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using KalAcademyMusicApp.ViewModels;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KalAcademyMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Song> Songs;
        private DataAccess dataAccess;
        private UIElement[] mainContentWindowVisibility;

        public MainWindowViewModel MainModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            MainModel = new MainWindowViewModel();
            dataAccess = new DataAccess();
            Songs = dataAccess.GetAllSongs();
            mainContentWindowVisibility = new UIElement[] { SongCollection, MediaPlayerElement };
        }

        /// <summary>
        /// On clicling Play button this method will get call 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Song s = b.DataContext as Song;

            MediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(s.MusicMp3Path));
            MediaPlayerElement.MediaPlayer.Play();

            ToggleMainContentWindow(MediaPlayerElement);
            HomeListBoxItem.IsSelected = false;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string searchcontent = t.Text;

            if (HomeListBoxItem.IsSelected)
            {
                Songs = dataAccess.SearchAllSongsByNameOrArtist(searchcontent);
            }
            else
            {
                Songs = dataAccess.SearchMySongs(searchcontent);
            }
            //After calling an API we need to rebind GridView with new data(In this case its a collection of songs by name or artist)
            SongCollectionView.ItemsSource = Songs;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If this is called during app startup because default SelectedIndex is set in xaml markup, all XAML controls aren't
            // initialized yet, so we just skip
            if (SongCollectionView != null)
            {
                if (HomeListBoxItem.IsSelected)
                {
                    Songs = dataAccess.GetAllSongs();

                    ToggleMainContentWindow(SongCollection);

                }
                else if (MusicPlayerListBoxItem.IsSelected)
                {
                    ToggleMainContentWindow(MediaPlayerElement);
                }
                else if (MyCollectionListBoxItem.IsSelected)
                {
                    Songs = dataAccess.GetMySongs();

                    ToggleMainContentWindow(SongCollection);
                }
                //After calling an API we need to rebind GridView with new data.In this case we are refreshing the Gridview with new data
                SongCollectionView.ItemsSource = Songs;

                // User may have typed a search query when (s)he was on the other UI, so we clear the text since it is not relavent now.
                tbsearch.Text = "";
            }
        }

        private void chkaddtofavorite_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox c = sender as CheckBox;
            Song s = c.DataContext as Song;
            if (s != null)
            {
                dataAccess.AddSongToFavorite(s);
            }
        }

        private void chkaddtofavorite_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox c = sender as CheckBox;
            Song s = c.DataContext as Song;
            if (s != null)
            {
                dataAccess.DeleteSongFromFavorites(s);
                if (MyCollectionListBoxItem.IsSelected == true)
                {
                    Songs = dataAccess.GetMySongs();
                    SongCollectionView.ItemsSource = Songs;

                }
            }
            //private async void Button_ClickSave(object sender, RoutedEventArgs e)
            //{
            //    var helper = new Helper();
            //    await helper.SerializeDataToJson(MainModel.Songs, "Playlist.json");

            //    MainModel.Songs = await helper.ReadPlaylist("Playlist.json");

            //}
        }

        private void ToggleMainContentWindow(UIElement currentElement)
        {
            Array.ForEach(mainContentWindowVisibility, e => e.Visibility = e == currentElement ? Visibility.Visible : Visibility.Collapsed );
        }
    }
}