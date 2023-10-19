using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftManagementApp.Models
{
    public class Aircraft
    {
        public int Id;
        public string Name;
        public string EngineNumber;
        public int Capacity;

        public Aircraft(int id, string name, string engineNumber, int capacity)
        {
            Id = id;
            Name = name;
            EngineNumber = engineNumber;
            Capacity = capacity;
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{EngineNumber}\t{Capacity}";
        }

        public static Aircraft ToAircraft(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string name = asd[1];
            string engineNumber = asd[2];
            int capacity = int.Parse(asd[3]);
            
            return new Aircraft(id,name,engineNumber,capacity);
        }
    }
}