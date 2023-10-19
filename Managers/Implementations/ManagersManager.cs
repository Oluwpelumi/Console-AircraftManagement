using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;
using Microsoft.VisualBasic;

namespace AircraftManagementApp.Managers.Implementations
{
    public class ManagersManager : IManagerInterface
    {
        List<Manager> managerDb = Database.ManagerDb;
        List<User> userDb = Database.UserDb;
        IUserInterface userInterface = new UserManager();
        UserManager userM = new UserManager();


        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\ManagerDb.txt";
        
        private string filePathComb;
        public ManagersManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "ManagerDb.txt");
        }

        private void ReadAllFromFile()
        {
            if (File.Exists(filePathComb))
            {
                var managers = File.ReadAllLines(filePathComb);
                foreach (var manager in managers)
                {
                    var a = Manager.ToManager(manager);
                    managerDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "ManagerDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Manager manager)
        {
            using (StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(manager.ToString());
            }
        }

        private void RefreshFile()
        {
            using (StreamWriter str = new StreamWriter(filePathComb, false))
            {
                foreach (var item in managerDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string staffNumber)
        {
            var manager = Get(staffNumber);
            if (manager != null)
            {
                managerDb.Remove(manager);
                RefreshFile();
                return true;
            }
            return false;
        }

        public List<Manager> GetAll()
        {
            return managerDb;
        }

        public Manager Get(string staffNumber)
        {
            foreach (var manager in managerDb)
            {
                if (manager.StaffNumber == staffNumber)
                {
                    return manager;
                }
            }
            return null;
        }
 
        public Manager Register(string name, string email, string password, string address, string phoneNumber, Gender gender)
        {
            var exists = userInterface.Get(email);
            if (exists != null)
            {
                System.Console.WriteLine("email already exists");
            }
            var user = new User(userDb.Count + 1, name, email, password, address, phoneNumber, gender, 0, "Manager");
            userDb.Add(user);
            userM.AddToFile(user);

            var manager = new Manager(managerDb.Count + 1, email, name+user.Id+"MG/CLH");
            managerDb.Add(manager);
            AddToFile(manager);

            return manager;
        }

        public bool Update(string staffNumber)
        {
            foreach (var manager in managerDb)
            {
                if(manager.StaffNumber == staffNumber)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;

            // var pilot = managerDb.Find(a => a.StaffNumber == staffNumber);
            // RefreshFile();
            // return true;
        }

        private bool Check(string staffNumber)
        {
            foreach (var manager in managerDb)
            {
                if (manager.StaffNumber == staffNumber)
                {
                    return false;
                }
            }
            return true;
        }
    }
}