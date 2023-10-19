using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Implementations
{
    public class UserManager : IUserInterface
    {
    
        List<User> userDb = Database.UserDb;
        
        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\UserDb.txt";


        private string filePathComb;
        public UserManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "userDb.txt");
        }
        


        // private string filePathComb;
        // public UserManager()
        // {
        //     string dirPath = "Files"; string fileName = "UserDb.txt";
        //     string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //     filePathComb = Path.Combine(baseDirectory, dirPath, fileName);
        // }

        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var users = File.ReadAllLines(filePathComb);
                foreach (var user in users)
                {
                    var a = User.ToUser(user);
                    userDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "UserDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        public void AddToFile(User user)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(user.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in userDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        
        public bool FundWallet(string email, double amount)
        {
            var user = Get(email);
            if (user != null)
            {
                user.Wallet += amount;
                RefreshFile();
                return true;
            }
            return false;
        }

        public bool UpdateWallet(string email, double amount)
        {
            var user = Get(email);
            if (user != null)
            {
                user.Wallet -= amount;
                RefreshFile();
                return true;
            }
            return false;
        }
        
        public bool Delete(string email)
        {
            var user = Get(email);
            if(user == null)
            {
                return false;
            }
            else
            {
                userDb.Remove(user);
                RefreshFile();
                return true;
            }
           
        }

        public User Get(string email)
        {
            foreach (var user in userDb)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }

        public List<User> GetAll()
        {
            return userDb;
        }

        public User Login(string email, string password)
        {
            foreach (var user in userDb)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public bool Update(string email)
        {
            var k = userDb.Any(x=> x.Email == email);
            RefreshFile();
            return k;
        }
    }
}