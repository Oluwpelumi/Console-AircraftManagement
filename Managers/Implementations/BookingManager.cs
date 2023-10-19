using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Implementations
{
    public class BookingManager : IBookingInterface
    {
        List<Booking> bookingDb = Database.BookingDb;
        IUserInterface userInterface = new UserManager();
        IFlightInterface flightInterface = new FlightManager();
        IAircraftInterface aircraftInterface = new AircraftManager();
        IPassengerInterface passengerInterface = new PassengerManager();

        // string file = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\BookingDb.txt";
        private string filePathComb;
        public BookingManager()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            filePathComb = Path.Combine(currentDirectory, "File", "BookingDb.txt");
        }


        public void ReadAllFromFile()
        {
            if(File.Exists(filePathComb))
            {
                var bookings = File.ReadAllLines(filePathComb);
                foreach (var booking in bookings)
                {
                    var a = Booking.ToBooking(booking);
                    bookingDb.Add(a);
                }
            }
            else
            {
                string directory = Path.GetDirectoryName(filePathComb);
                Directory.CreateDirectory(directory);
                File.Create(filePathComb);

                // string path = @"C:\Users\USER\OneDrive\Desktop\AircraftManagement\AircraftManagementApp\Files\";
                // Directory.CreateDirectory(path);
                // string fileName = "BookingDb.txt";
                // var b = Path.Combine(path,fileName);
                // File.Create(b);
            }
        }

        private void AddToFile(Booking booking)
        {
            using(StreamWriter str = new StreamWriter(filePathComb, true))
            {
                str.WriteLine(booking.ToString());
            }
        }

        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(filePathComb,false))
            {
                foreach (var item in bookingDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public bool Delete(string referenceNumber)
        {
           var booking = Get(referenceNumber);
           if (booking != null)
           {
                bookingDb.Remove(booking);
                RefreshFile();
                return true;
           }
           return false;
        }

        public Booking Get(string referenceNumber)
        {
            foreach (var booking in bookingDb)
            {
                if (booking.ReferenceNumber == referenceNumber)
                {
                    return booking;
                }
            }
            return null;
        }

        public List<Booking> GetAll()
        {
            return bookingDb;
        }

        public Booking Make(string passengerEmail, string flightReferenceNumber)
        {
            // var user = userInterface.Get(passengerEmail);
            var passenger = passengerInterface.Get(passengerEmail);
            var flight = flightInterface.Get(flightReferenceNumber);
            var aircraft = aircraftInterface.Get(flight.AircraftName);
            if(flight.Passengers.Count < aircraft.Capacity)
            { 
                if(flight.Price <= passenger.Wallet)
                {
                    passenger.Wallet -= flight.Price;
                    passengerInterface.Update(passenger.UserEmail);
                    userInterface.Update(passengerEmail);
                    flight.Passengers.Add(passengerEmail);
                    string refNum = flight.Passengers.Count+"AirLine"+ new Random().Next(1,99);
                    var booking = new Booking(bookingDb.Count+1,refNum,flight.Passengers.Count,passengerEmail,flightReferenceNumber);
                    bookingDb.Add(booking);
                    AddToFile(booking);
                    Console.WriteLine($"booking with ref {booking.ReferenceNumber} is successful, your seat number is {booking.SeatNumber}, you are going with aircraft {aircraft.Name}");
                    return booking;
                }
                Console.WriteLine("insufficient balance");
                return null;

            }
            Console.WriteLine("flight not available");
            return null;
        }
    }
}