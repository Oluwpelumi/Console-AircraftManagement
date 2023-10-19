// See https://aka.ms/new-console-template for more information
using AircraftManagementApp;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Menu;
using AircraftManagementApp.Models;
using MenuManager = AircraftManagementApp.Menu.Manager;

UserManager userManager = new UserManager();
PassengerManager passengerManager = new PassengerManager();
AirportManager airportManager = new AirportManager();
AircraftManager aircraftManager = new AircraftManager();
FlightManager flightManager = new FlightManager();
PilotManager pilotManager = new PilotManager();
BookingManager bookingManager = new BookingManager();
Main menu = new Main();


userManager.ReadAllFromFile();
passengerManager.ReadAllFromFile();
airportManager.ReadAllFromFile();
aircraftManager.ReadAllFromFile();
flightManager.ReadAllFromFile();
pilotManager.ReadAllFromFile();
bookingManager.ReadAllFromFile();


menu.MainMenu();



