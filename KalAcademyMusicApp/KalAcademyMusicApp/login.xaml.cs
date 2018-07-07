using KalAcademyMusicApp.Config;
using KalAcademyMusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KalAcademyMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class login : Page
    {
        const string FILE_NAME = "data.json";
        AuthHelper fileHelper = new AuthHelper();
        //  User user = new User();
        String userContent;
        public login()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadFiles();
        }

        private async void LoadFiles()
        {
            userContent = await fileHelper.ReadTextFile(FILE_NAME);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddUser));
        }
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string pass = txtPassword.Password;

            User user = new User(name, pass);
            if (user.Authenticate(userContent, name, pass))
                this.Frame.Navigate(typeof(MainPage));
            else
                Error.Text = "User name or password is incorrect.";
        }
    }
}
