using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Implementations
{
    public class FlightManager : IFlightInterface
    {
        List<Flight> fligthDb = Database.FlightDb;

        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\FlightDb.txt";
        
        private string filePathComb;
        public FlightManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "FlightDb.txt");
        }


        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var flights = File.ReadAllLines(filePathComb);
                foreach (var flight in flights)
                {
                    var a = Flight.ToFlight(flight);
                    fligthDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);
                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "FlightDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Flight flight)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(flight.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in fligthDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }

        public bool Cancel(string referenceNumber)
        {
            var flight = Get(referenceNumber);
            if (flight != null)
            {
                fligthDb.Remove(flight);
                RefreshFile();
                return true;
            }
            return false;
        }

        public Flight Get(string referenceNumber)
        {
            foreach (var flight in fligthDb)
            {
                if (flight.ReferenceNumber == referenceNumber)
                {
                    return flight;
                }
            }
            return null;
        }

        public List<Flight> GetAll()
        {
            return fligthDb;
        }

        public bool Update(string referenceNumber)
        {
            foreach (var flight in fligthDb)
            {
                if(flight.ReferenceNumber == referenceNumber)
                {
                    RefreshFile();
                    return true;
                }
            }
            return false;
            
            // var check = fligthDb.Find(a=> a.ReferenceNumber == referenceNumber);
            // RefreshFile();
            // return true;
        }

        private bool Check(string name)
        {
            foreach (var flight in fligthDb)
            {
                if (flight.Name == name)
                {
                    return false;
                }   
            }
            return true;
        }

        public Flight Create(string takeOffPoint, string destination, DateTime takeOfTime, string pilotStaffNumber, string aircraftName, double price, List<string> passengers)
        {
            string name = GenName(takeOffPoint, destination, takeOfTime);
            var exists = Check(name);
            if(exists == true)
            {
                var flight = new Flight(fligthDb.Count+1,name,GenRefNum(),takeOffPoint,destination,takeOfTime,pilotStaffNumber,aircraftName,price,passengers);
                fligthDb.Add(flight);
                AddToFile(flight);
                return flight;
            }

            Console.WriteLine("flight already");
            return null;

        }

        private string GenName(string takeOffPoint, string destination, DateTime takeOfTime)
        {
            return $"CLH/{takeOffPoint.Substring(0,3)}{destination.Substring(0,3)}{takeOfTime.Day}";
        }
        private string GenRefNum()
        {
            return  $"CLH/FLY/00/{fligthDb.Count+1}";
        }
    }
}