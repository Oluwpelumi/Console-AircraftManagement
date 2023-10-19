using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;

namespace AircraftManagementApp.Models
{
    public class Pilot
    {
        public int Id;
        public string UserEmail;
        public string StaffNumber;

        public Pilot(int id, string userEmail, string staffNumber)
        {
            Id = id;
            UserEmail = userEmail;
            StaffNumber = staffNumber;
        }

        public override string ToString()
        {
            return $"{Id}\t{UserEmail}\t{StaffNumber}";
        }

        public static Pilot ToPilot(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string userEmail = asd[1];
            string staffNumber = asd[2];
            
            return new Pilot(id,userEmail,staffNumber);
        }
    }
}