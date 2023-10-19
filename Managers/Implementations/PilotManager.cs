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
    public class PilotManager : IPilotInterface
    {
        List<Pilot> pilotDb = Database.PilotDb;
        List<User> userDb = Database.UserDb;
        IUserInterface userInterface = new UserManager();
        UserManager userM = new UserManager();

        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\PilotDb.txt";
        private string filePathComb;
        public PilotManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "PilotDb.txt");
        }

        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var pilots = File.ReadAllLines(filePathComb);
                foreach (var pilot in pilots)
                {
                    var a = Pilot.ToPilot(pilot);
                    pilotDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "PilotDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Pilot pilot)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(pilot.ToString());
            }
        }
 
        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in pilotDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string useremail)
        {
            var pilot = Get(useremail);
            if (pilot != null)
            {
                pilotDb.Remove(pilot);
                RefreshFile();
                return true;
            }
            return false;
        }

        public Pilot Get(string useremail)
        {
            var chk = pilotDb.SingleOrDefault(x=>x.UserEmail == useremail);
            return chk;
            // foreach (var pilot in pilotDb)
            // {
            //     if (pilot.UserEmail == useremail)
            //     {
            //         return pilot;
            //     }
            // }
            // return null;
        }

        public List<Pilot> GetAll()
        {
            return pilotDb;
        }

        public Pilot Register(string name, string email, string password, string address, string phoneNumber, Gender gender)
        {
            var exist = userInterface.Get(email);
            if(exist != null)
            {
                System.Console.WriteLine("Email already exist!");
                return null;
            }
            var user = new User(userDb.Count + 1, name, email, password, address, phoneNumber, gender, 0, "Pilot");
            userDb.Add(user);
            userM.AddToFile(user);

            var pilot = new Pilot(pilotDb.Count + 1, email, name+user.Id+"PT/CLH");
            pilotDb.Add(pilot);
            AddToFile(pilot);

            return pilot;
        }

        public bool Update(string useremail)
        {
            foreach (var pilot in pilotDb)
            {
                if(pilot.UserEmail == useremail)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;

            // var pilot = pilotDb.Find(a=> a.UserEmail == useremail);
            // RefreshFile();
            // return true;
        }

        public bool Check(string useremail)
        {
            var e = pilotDb.Any(x=> x.UserEmail == useremail);
            return e;
            // foreach (var pilot in pilotDb)
            // {
            //     if (pilot.UserEmail == useremail)
            //     {
            //         return false;
            //     }
            // }
            // return true;
        }
    }
}