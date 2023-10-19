using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;

namespace AircraftManagementApp.Models
{
    public class Airport
    {
        public int Id;
        public string Name;
        public string Location;
        public AirportType AirportType;
        public Airport(int id, string name, string location, AirportType airportType)
        {
            Id = id;
            Name = name;
            Location = location;
            AirportType = airportType;
        }

        public Airport()
        {
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Location}\t{AirportType}";
        } 

        public static Airport ToAirport(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string name = asd[1];
            string location = asd[2];
            if (Enum.TryParse<AirportType>(asd[3], out AirportType airportType ))
            {
                System.Console.WriteLine();
            } 

            return new Airport(id,name,location,airportType);
        }
    }
}