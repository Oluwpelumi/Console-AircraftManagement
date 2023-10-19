using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftManagementApp.Models
{
    public class Flight
    {
        public int Id;
        public string Name;
        public string ReferenceNumber;
        public string TakeOffPoint;
        public string Destination;
        public DateTime TakeOfTime;
        public string PilotStaffNumber;
        public string AircraftName;
        public double Price;
        public List<string> Passengers = new List<string>();
        // public Dictionary<string, bool> passes = new Dictionary<string, bool>();

        public Flight(int id, string name, string referenceNumber, string takeOffPoint, string destination, DateTime takeOfTime, string pilotStaffNumber, string aircraftName, double price, List<string> passengers)
        {
            Id = id;
            Name = name;
            ReferenceNumber = referenceNumber;
            TakeOffPoint = takeOffPoint; 
            Destination = destination;
            TakeOfTime = takeOfTime;
            PilotStaffNumber = pilotStaffNumber;
            AircraftName = aircraftName;
            Price = price;
            Passengers = passengers;
        }

        public override string ToString()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append($"{Id}\t{Name}\t{ReferenceNumber}\t{TakeOffPoint}\t{Destination}\t{TakeOfTime}\t{PilotStaffNumber}\t{AircraftName}\t{Price}\t");

            foreach (var item in Passengers)
            {
                stb.Append($"{item},");
            }

            return stb.ToString();
        }

        public static Flight ToFlight(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string name = asd[1];
            string referenceNumber = asd[2];
            string takeOffPoint = asd[3];
            string Destination = asd[4];
            DateTime takeOfTime = (DateTime) DateTime.Parse(asd[5]);
            string pilotStaffNumber = asd[6];
            string aircraftName = asd[7];
            double price = int.Parse(asd[8]);

            List<string> passengers = new List<string>();

            var b = asd[9].Split(',');

            for(int i = 0; i<b.Length-1;i++ )
            {
                passengers.Add(b[i]);
            }

            return new Flight(id,name,referenceNumber,takeOffPoint,Destination,takeOfTime,pilotStaffNumber,aircraftName,price,passengers);
        }
    }
}