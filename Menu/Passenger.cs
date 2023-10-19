using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Menu
{
    public class Passenger
    {
        IAirportInterface airportInterface = new AirportManager();
        IAircraftInterface aircraftInterface = new AircraftManager();
        IFlightInterface flightInterface = new FlightManager();
        IPassengerInterface passengerInterface = new PassengerManager();
        IBookingInterface bookingInterface = new BookingManager();
        IUserInterface userManager = new UserManager();
        List<User> userDb = Database.UserDb;
        
        public void PassengerMenu()
        {
             bool opt3 = true;
            while (opt3)
            {
                Console.WriteLine("Enter 1 to Fund your wallet\nEnter 2 to Update your account\nEnter 3 to view all Airports\nEnter 4 to view all Aircrafts\nEnter 5 to view all avialable Flights\nEnter 6 to book a Flight\nEnter 7 to exit");
                // int input = int.Parse(Console.ReadLine());
                if(int.TryParse(Console.ReadLine(), out int input))
                {
                    if(input == 1)
                    {
                        FundWalletMenu();
                    }
                    else if(input == 2)
                    {
                        UpdateAccount();
                    }
                    else if(input == 3)
                    {
                        ViewAllAirports();
                    }
                    else if(input == 4)
                    {
                        ViewAllAircrafts();
                    }
                    else if(input == 5)
                    {
                        ViewAllFlights();
                    }
                    else if(input == 6)
                    {
                        // BookFlight();
                        Bookings bk = new Bookings();
                        bk.MakeBookingMenu();
                    }
                    else if(input == 7)
                    {
                        opt3 = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
            }
        }


        public void FundWalletMenu()
        {
            System.Console.WriteLine("Enter your email account to fund your wallet:");
            string email = Console.ReadLine();
            var passenger = passengerInterface.Get(email);
            if (passenger != null)
            {
                try
                {
                    System.Console.WriteLine("Enter the amount you want to add to your wallet:");
                    double amount = double.Parse(Console.ReadLine());
                    // bool chk = Double.TryParse(Console.ReadLine(), out double amount);
                    if (amount > 0)
                    {
                        bool isAdded = userManager.FundWallet(email, amount);
                        if (isAdded)
                        {
                            passenger.Wallet += amount;
                            passengerInterface.Update(email);
                            System.Console.WriteLine($"Wallet credited successfully! \t The updated current balance in your wallet is {passenger.Wallet}");
                        } 
                    }
                    else
                    {
                        System.Console.WriteLine("The amount you input is a negative value!");
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("Invalid input for the amount!");
                }
            }
            else
            {
                System.Console.WriteLine("The email you input does not exist");
            }
        }

        public void UpdateAccount()
        {
            System.Console.WriteLine("Enter your verified email account: ");
            string email = Console.ReadLine();
            var acct = passengerInterface.Get(email);
            if (acct == null)
            {
                System.Console.WriteLine("Inavali Email!! Enter a valid email");
                PassengerMenu();
            }
            else
            {
                // var acct2 = userManager.Get(email);
                var acct2 = userDb.Find(a=> a.Email == acct.UserEmail);
                System.Console.WriteLine($"Name: {acct2.Name} \nEmail: {acct2.Email} \nAddress: {acct2.Address} \nPhone Number: {acct2.PhoneNumber} \nGender: {acct2.Gender} \nWallet: {acct2.Wallet} \nRole: {acct2.Role}");
                
                                
                System.Console.WriteLine("Do you want to update your name?: (y/n)");
                string option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new name: ");
                    acct2.Name = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update your e-mail?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new e-mail: ");
                    acct2.Email = Console.ReadLine();
                    acct.UserEmail = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update your address?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new address: ");
                    acct2.Address = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update your Phone Number?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new phone number: ");
                    acct2.PhoneNumber = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update your gender?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new gender: ");
                    Console.WriteLine("Enter 1 for male and enter 2 for female:");       
                    acct2.Gender = (Gender)int.Parse(Console.ReadLine());
                }

                if (passengerInterface.Update(acct2.Email))
                {
                    System.Console.WriteLine($"You have successfully updated your account!");
                    PassengerMenu();
                }
                else
                {
                    System.Console.WriteLine("An error has occured!");
                }
            }
        }



        public void ViewAllAirports()
        {
            var airports = airportInterface.GetAll();
            foreach (var airport in airports)
            {
                System.Console.WriteLine(airport);
            }
        }



        public void ViewAllAircrafts()
        {
            var aircrafts = aircraftInterface.GetAll();
            foreach (var aircraft in aircrafts)
            {
                System.Console.WriteLine(aircraft);
            }
        }



        public void ViewAllFlights()
        {
            var flights = flightInterface.GetAll();
            foreach (var flight in flights)
            {
                System.Console.WriteLine(flight);
            }
        }


    }
}