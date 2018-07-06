using KalAcademyMusicApp.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalAcademyMusicApp.Models
{
    public class User
    {
        AuthHelper fileHelper = new AuthHelper();
        const string FILE_NAME = "data.json";
        // public int Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        // public List<Song> OwnedSongs { get; set; }
        // public List<Song> FavoriteSongs { get; set; }

        public User( string name, string password)
        {
            // Id = id;
            Name = name;
            Password = password;
        }
        public bool Authenticate(string userContent, string UserName, string Password)
        {
            // var folder = ApplicationData.Current.LocalFolder;
            // var employeeFile = await folder.GetFileAsync(FILE_NAME);
            // var lines = await FileIO.ReadLinesAsync(employeeFile);
            List<User> myList = JsonConvert.DeserializeObject<List<User>>(userContent);
            if (myList == null)
                myList = new List<User>();

            // string pass = "";
            // string user = "";

            foreach (var con in myList)
            {
                string uName = con.Name;
                string uPass = con.Password;
                if (UserName == uName)
                {
                    if (uPass == Password)
                    {
                        return true;
                        //Console.WriteLine("password match");
                    }
                }
                //  Console.WriteLine("password DOES NOT match");
            }
            return false;
        }
    }
}


