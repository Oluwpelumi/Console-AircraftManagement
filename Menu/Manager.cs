using System.Data;
// using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Menu
{
    public class Manager
    {
        IAircraftInterface aircraftInterface = new AircraftManager();
        IFlightInterface flightInterface = new FlightManager();
        IPilotInterface pilotInterface = new PilotManager();
        IUserInterface userInterface = new UserManager();
        IPassengerInterface passengerInterface = new PassengerManager();

        public void ManagerMenu()
        {
            bool opt3 = true;
            while (opt3)
            {
                Console.WriteLine("Enter 1 for Airport Mgt\nEnter 2 for Aircraft Mgt\nEnter 3 for Flight Mgt\nEnter 4 for Passenger Mgt\nEnter 5 for Pilot Mgt\nEnter 6 for Bookings Mgt\nEnter 7 to exit");
                // int input = int.Parse(Console.ReadLine());
                if(int.TryParse(Console.ReadLine(), out int input))
                {
                    if(input == 1)
                    {
                        Airports airport = new Airports();
                        airport.AirPortMgtMenu();
                    }
                    else if(input == 2)
                    {
                        Aircrafts aircraft = new Aircrafts();
                        aircraft.AircraftMgtMenu();
                    }
                    else if(input == 3)
                    {
                        Flights flt = new Flights();
                        flt.FlightMgtMenu();
                    }
                    else if(input == 4)
                    {
                        PassengerMgtMenu();
                    }
                    else if(input == 5)
                    {
                        Pilots plt = new Pilots();
                        plt.PilotMgtMenu();
                    }
                    else if(input == 6)
                    {
                        Bookings bk = new Bookings();
                        bk.BookingMgtMenu();
                    }
                    else if(input == 7)
                    {
                        opt3 = false;
                    }
                    else if(input > 7)
                    {
                        System.Console.WriteLine("The integer value you enter is not within the options");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input! Your input is not an integer value");
                    // Thread.Sleep(2000);
                }
            }
            
        }


        public void PassengerMgtMenu()
        {
            bool opt =  true;
            while (opt)
            {
                System.Console.WriteLine("Enter 1 to view all passengers\nEnter 2 to remove passenger\nEnter 3 to go back to the Manager menu");
                bool chk = int.TryParse(Console.ReadLine(), out int k);
                if (chk)
                {
                    switch (k)
                    {
                        case 1:
                        ViewAllPassengers();
                        break;
                        
                        case 2:
                        RemovePassenger();
                        break;

                        case 3:
                        opt = false;
                        break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input!");
                }
            }
        }

        public void ViewAllPassengers()
        {
            var passengers = passengerInterface.GetAll();
            foreach (var passenger in passengers)
            {
                System.Console.WriteLine(passenger);
            }
        }

        public void RemovePassenger()
        {
            ViewAllPassengers();
            System.Console.WriteLine("Enter the e-mail of the passenger you want to delete: ");
            string email = Console.ReadLine();
            bool isDeleted = passengerInterface.Delete(email);
            if (isDeleted)
            {
                System.Console.WriteLine("Deleted Successful..");
            }
            else
            {
                System.Console.WriteLine($"Passenger with the email {email} does not exist! ");
            }
        }
    }
}