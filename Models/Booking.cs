using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftManagementApp.Models
{
    public class Booking
    {
        public int Id;
        public string ReferenceNumber;
        public int SeatNumber;
        public string PassengerEmail;
        public string FlightReferenceNumber;

        public Booking(int id,string referenceNumber, int seatNumber, string passengerEmail, string flightReferenceNumber)
        {
            Id = id;
            ReferenceNumber = referenceNumber;
            SeatNumber = seatNumber;
            PassengerEmail = passengerEmail;
            FlightReferenceNumber = flightReferenceNumber;
        }

        public override string ToString()
        {
            return $"{Id}\t{ReferenceNumber}\t{SeatNumber}\t{PassengerEmail}\t{FlightReferenceNumber}";
        }

        public static Booking ToBooking(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string referenceNumber = asd[1];
            int seatNumber = int.Parse(asd[2]);
            string passengerEmail = asd[2];
            string flightReferenceNumber = asd[2];

            return new Booking(id,referenceNumber,seatNumber,passengerEmail,flightReferenceNumber );
        }

    }
}