using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftManagementApp.Models
{
    public class Passenger
    {
        public int Id;
        public string UserEmail;
        public double Wallet;

        public Passenger(int id, string userEmail, double wallet)
        {
            Id = id;
            UserEmail = userEmail;
            Wallet = wallet;
        }

        public override string ToString()
        {
            return $"{Id}\t{UserEmail}\t{Wallet}";
        }

        public static Passenger ToPassenger(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string userEmail = asd[1];
            double wallet = double.Parse(asd[2]);
            
            return new Passenger(id,userEmail,wallet);
        }

    }
}