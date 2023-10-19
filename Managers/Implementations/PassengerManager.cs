using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Implementations
{
    public class PassengerManager : IPassengerInterface
    {
        List<Passenger> passengerDb = Database.PassengerDb; 
        List<User> userDb = Database.UserDb;
        IUserInterface userInterface = new UserManager();
        UserManager userM = new UserManager();

        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\PassengerDb.txt";
        private string filePathComb;
        public PassengerManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "PassengerDb.txt");
        }

        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var passengers = File.ReadAllLines(filePathComb);
                foreach (var passenger in passengers)
                {
                    var a = Passenger.ToPassenger(passenger);
                    passengerDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "PassengerDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Passenger passenger)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(passenger.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in passengerDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string email)
        {
            var passenger = Get(email);
            if (passenger != null)
            {
                passengerDb.Remove(passenger);
                RefreshFile();
                return true;
            }
            return false;
        }

        public Passenger Get(string email)
        {
            foreach (var passenger in passengerDb)
            {
                if (passenger.UserEmail == email)
                {
                    return passenger;
                }
            }
            return null;
        }

        public List<Passenger> GetAll()
        {
            return passengerDb;
        }

        public Passenger Register(string name, string userEmail, string password, string address, string phoneNumber, Gender gender)
        {
            var exists = userInterface.Get(userEmail);
            if (exists != null)
            {
                System.Console.WriteLine("email already exists");
            }
            var user = new User(userDb.Count+1,name,userEmail,password,address,phoneNumber,gender,0,"Passenger");
            userDb.Add(user);
            userM.AddToFile(user);

            var passenger = new Passenger(passengerDb.Count+1,userEmail,0);
            passengerDb.Add(passenger);
            AddToFile(passenger);

            return passenger;
        }

        public bool Update(string email)
        {
            foreach (var passenger in passengerDb)
            {
                if (passenger.UserEmail == email)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;

            // var passenger = passengerDb.Find(a=> a.UserEmail == email);
            // RefreshFile();
            // return true;
        } 

        public bool Check(string email)
        {
            foreach (var passenger in passengerDb)
            {
                if (passenger.UserEmail == email)
                {
                    return false;
                }
            }
            return true;
        }
    }
}