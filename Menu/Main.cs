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
    public class Main
    {
        IUserInterface userManager = new UserManager();
        IPassengerInterface passengerManager = new PassengerManager();
        public void MainMenu()
        {
                bool opt5 = true;
                while(opt5)
                {
                    Console.WriteLine("Enter 1 to register\nEnter 2 to login\nEnter 3 to exit");
                    bool check = int.TryParse(Console.ReadLine(), out int opt);
                    if (check)
                    {
                        if (opt == 1)
                        {
                            RegisterMenu();
                        }
                        else if (opt == 2)
                        {
                            LoginMenu();
                        }
                        else if (opt == 3)
                        {
                            System.Console.WriteLine("Thanks for coming");
                            opt5 = false;
                        }
                        else
                        {
                            System.Console.WriteLine("wrong input, not within the range of the given options: ");
                        }
                    }
                    
                    else
                    {
                        Console.WriteLine("invalid input, Your input is not an integer value");
                    }
                }
        }
        public void RegisterMenu()
        {
            Console.WriteLine("enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("enter your password");
            string password = Console.ReadLine();
            Console.WriteLine("enter your address");
            string address = Console.ReadLine();
            Console.WriteLine("enter your phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter 1 for male and enter 2 for female:");
            int gender = int.Parse(Console.ReadLine());

            var response = passengerManager.Register(name,email,password,address,phoneNumber,(Gender)gender);
            if(response != null) 
            {
                System.Console.WriteLine("your account is created succesfully");
                LoginMenu();
            }
            else
            {
                System.Console.WriteLine("Unable to register account!!");
                RegisterMenu();
            }

        }
        public void LoginMenu()
        {
            System.Console.WriteLine("Enter your credentials below to login: ");
            Console.WriteLine("enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("enter your pin");
            string pin = Console.ReadLine();
            var user = userManager.Login(email, pin);

            if (user == null)
            {
                Console.WriteLine("Invalid login credentials!");
                MainMenu();
            }
            if (user.Role == "Manager")
            {
                Manager m = new Manager();
                m.ManagerMenu();
            }
            else if (user.Role == "Passenger")
            {
                Passenger p = new Passenger();
                p.PassengerMenu();
            }
            else if (user.Role == "Pilot")
            {
                Pilot p = new Pilot();
                p.PilotMenu();
            }
            else if (user.Role == "SuperAdmin")
            {
                SuperAdmin sp = new SuperAdmin();
                sp.SuperMenu();
            }
        } 

    }
}