using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Menu
{
    public class Pilot
    {
        IAircraftInterface aircraftInterface = new AircraftManager();
        IFlightInterface flightInterface = new FlightManager();


        public void PilotMenu()
        {
            bool opt = true;
            while (opt)
            {
                Console.WriteLine("Enter 1 to view all Aircrafts\nEnter 2 to view all avialable flights\nEnter 3 to exit");
                // int input = int.Parse(Console.ReadLine());
                if(int.TryParse(Console.ReadLine(), out int input))
                {
                    if(input == 1)
                    {
                        ViewAllAircrafts();
                    }
                    else if(input == 2)
                    {
                        ViewAllFlights();
                    }
                    else if(input == 3)
                    {
                        opt = false;
                    }
                    else if(input < 0)
                    {
                        System.Console.WriteLine("The input value you entered is a negative number");
                    }
                    else
                    {
                        System.Console.WriteLine("The value you entered is out of range");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
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