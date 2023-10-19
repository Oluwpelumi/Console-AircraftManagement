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
    public class AirportManager : IAirportInterface
    {
        // public AirportManager()
        // {
        //     ReadAllFromFile();
        // }

        List<Airport> airportDb = Database.AirportDb;
        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\AirportDb.txt";
        
        private string filePathComb;
        public AirportManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "AirportDb.txt");
        }

        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var airports = File.ReadAllLines(filePathComb);
                foreach (var airport in airports)
                {
                    var a = Airport.ToAirport(airport);
                    airportDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "AirportDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
                // StreamReader streamReader = new StreamReader(b);
                // streamReader.Close();
            }
        }

        public void AddToFile(Airport airport)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(airport.ToString());
            }
        }

        public void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in airportDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string name)
        {
            var airport = Get(name);
            if (airport != null)
            {
                airportDb.Remove(airport);
                RefreshFile();
                return true;
            }
            return false;
        }

        public Airport Get(string name)
        {
            RefreshFile();
            foreach (var airport in airportDb)
            {
                if (airport.Name == name)
                {
                    return airport;
                }
            }
            return null;
        }

        public List<Airport> GetAll()
        {
            // RefreshFile();
            return airportDb;
        }

        
        private bool Check(string name)
        {
            // RefreshFile();
            foreach (var airport in airportDb)
            {
                if (airport.Name==name)
                {
                    return false;
                }
            }
            return true;
        }  


        public bool Update(string name)
        {   
            // RefreshFile();
            foreach (var airport in airportDb)
            {
                if (airport.Name == name)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;
            // var airport = airportDb.Find(a=> a.Name == name);
            // RefreshFile();
            // return true;

        }


        public Airport Register(string name, string location, AirportType airportType)
        {
            var airportExists= Check(name);
            if (airportExists==false)
            {
                Console.WriteLine("Airport already exists");
                return null;
            } 
            Airport airport= new Airport(airportDb.Count+1,name,location,airportType);
            airportDb.Add(airport);
            AddToFile(airport);
            return airport;
        }

    }
}