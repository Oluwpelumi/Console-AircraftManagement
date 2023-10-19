using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Implementations
{
    public class AircraftManager : IAircraftInterface
    {
        List<Aircraft> aircraftDb = Database.AircraftDb;
        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\AircraftDb.txt";

        private string filePathComb;
        public AircraftManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "AircraftDb.txt");
        }

        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var aircrafts = File.ReadAllLines(filePathComb);
                foreach (var aircraft in aircrafts)
                {
                    var a = Aircraft.ToAircraft(aircraft);
                    aircraftDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);
                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "AircraftDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Aircraft aircraft)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(aircraft.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in aircraftDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string name)
        {
            var aircraft = Get(name);
            if(aircraft != null)
            {
                aircraftDb.Remove(aircraft);
                RefreshFile();
                return true;
            }
            return false;
        }

        public Aircraft Get(string name)
        {
            foreach (var aircraft in aircraftDb)
            {
                if(aircraft.Name == name)
                {
                    return aircraft;
                }
            }
            return null;
        }

        public List<Aircraft> GetAll()
        {
            return aircraftDb;
        }

        public Aircraft Register(string name, string engineNumber, int capacity)
        {
            var exists = Check(name);
            if(exists == false)
            {
                System.Console.WriteLine("aircraft already exist");
                return null;
            }
            Aircraft aircraft = new Aircraft(aircraftDb.Count + 1, name, engineNumber, capacity);
            aircraftDb.Add(aircraft);
            AddToFile(aircraft);
            return aircraft;

        }

        private bool Check(string name)
        {
            foreach (var aircraft in aircraftDb)
            {
                if (aircraft.Name == name)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Update(string name) 
        {
            foreach (var aircraft in aircraftDb)
            {
                if (aircraft.Name == name)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;
        }

        public Aircraft GetById(int id)
        {
            var aircraft = aircraftDb.Find(a=> a.Id == id);
            return aircraft;
        }
    }
}