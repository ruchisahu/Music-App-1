using KalAcademyMusicApp.Config;
using KalAcademyMusicApp.Models;
using Newtonsoft.Json;
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
    public sealed partial class AddUser : Page
    {
        const string FILE_NAME = "data.json";
        AuthHelper fileHelper = new AuthHelper();
        // User user = new User();
        List<User> myList;
        String userContent;
        public AddUser()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadFiles();
            //this.DataContext = Model.User.Authenticate();
        }

        private async void LoadFiles()
        {
            userContent = await fileHelper.ReadTextFile(FILE_NAME);
        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {

            this.Frame.Navigate(typeof(MainPage));

        }

       private async void ADD(object sender, RoutedEventArgs e)

        {
            Error.Visibility = Visibility.Visible;
            myList = JsonConvert.DeserializeObject<List<User>>(userContent);

            string Name = txtName.Text;
            string Pass= txtPassword.Text;
            Error.Visibility = Visibility.Visible;
            if (Name.Length < 4 || Pass.Length < 4)
            {
                Error.Text = "Usrname/Password length should be more than 4.";
            }
            else
            {
                Error.Visibility = Visibility.Collapsed;
                


                //  myList.Add(new User(Name, Pass));
                //  Error.Text = "Your information has been submitted successfully.";
                msg.Visibility = Visibility.Visible;
               

                myList.Add(new User(Name, Pass));

                string data = JsonConvert.SerializeObject(myList);
                await fileHelper.WriteTextFile("data.json", data);
                

            }



        }
        private void btnEnterName_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }


        }

    }

