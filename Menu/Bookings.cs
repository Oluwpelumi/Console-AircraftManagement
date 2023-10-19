using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class Bookings
    {
        IBookingInterface bookingInterface = new BookingManager();
        IFlightInterface flightInterface = new FlightManager();
        IPassengerInterface passengerInterface = new PassengerManager();
        IAircraftInterface aircraftInterface = new AircraftManager();
        IUserInterface userInterface = new UserManager();
         public void BookingMgtMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                Console.WriteLine("Enter 1 to make booking\nEnter 2 to view a booking / all bookings\nEnter 3 to exit");
                // int opt = int.Parse();
                if(int.TryParse(Console.ReadLine(), out int opt))
                {
                    if(opt == 1)
                    {
                        MakeBookingMenu();
                    }
                    else if (opt == 2)
                    {
                        ViewBookingsMenu();
                    }
                    else if (opt == 3)
                    {
                        opt2 = false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
                
            }
            
        }


        public void MakeBookingMenu()
            {
                var flights = flightInterface.GetAll();
                if(flights.Count() == 0)
                {
                    System.Console.WriteLine("No flight available");
                    Passenger p = new Passenger();
                    p.PassengerMenu();
                }
                else
                {
                    foreach (var item in flights)
                    {
                        System.Console.WriteLine(item);
                    }
                    System.Console.WriteLine("Enter email to book a flight");
                    string email = Console.ReadLine();
                    System.Console.WriteLine("Enter the Flight's Reference number");
                    string flightRef = Console.ReadLine();
                    var passeg = passengerInterface.Get(email);
                    var flight = flightInterface.Get(flightRef);
                    if (flight != null && passeg != null)
                    {
                        bool priceCheck = passeg.Wallet >= flight.Price;
                        bool spaceCheck = flight.Passengers.Count() < aircraftInterface.Get(flight.AircraftName).Capacity;
                        
                        var make = bookingInterface.Make(email, flightRef);
                        if (make != null)
                        {
                            // passeg.Wallet -= flight.Price;
                            // passengerInterface.Update(passeg.UserEmail);
                            userInterface.UpdateWallet(email, flight.Price);
                            // userInterface.Update(passeg.UserEmail);
                            flight.Passengers.Add(email);
                            flightInterface.Update(flightRef);
                            System.Console.WriteLine("Booking Successful!!");
                            BookingMgtMenu();
                        }
                        else
                        {
                            if (!priceCheck)
                            {
                                System.Console.WriteLine("Unsuccessful Booking due to insuficcient funds in your wallet!");
                            }
                            if (!spaceCheck)
                            {
                                System.Console.WriteLine($"Unsuccessful Booking, The Flight {flight.AircraftName} is full!");
                            }
                            BookingMgtMenu();
                        }   
                    }
                    else
                    {
                        System.Console.WriteLine($"Flight with the ref number {flightRef} does not exist");
                    }
                }
                
                // Console.WriteLine("enter the passenger's e-mail");
                // string passengerEmail = Console.ReadLine();
                // Console.WriteLine("enter the flight's reference number");
                // string flightReferenceNumber = Console.ReadLine();

                // var register =  bookingInterface.Make(passengerEmail,flightReferenceNumber);

                // if (register != null)
                // {
                //     System.Console.WriteLine("Done....");
                //     // AirPortMgtMenu();
                // }
                // else
                // {
                //     BookingMgtMenu();
                // }
            }


        public void ViewBookingsMenu()
        {
            System.Console.WriteLine("Enter 1 to view booking of a particular passenger\nEnter 2 to view all bookings\nEnter 3 to exit: ");
            int Input = int.Parse(Console.ReadLine());
            var bookings = bookingInterface.GetAll();
            if(Input == 1)
            {
                if (bookings.Count <= 0)
                {
                    System.Console.WriteLine("No booking made yet");
                }
                else
                {
                    System.Console.WriteLine("Enter the reference number for the booking: ");
                    string reff = Console.ReadLine();
                    var bk = bookingInterface.Get(reff);
                    if (bk == null)
                    {
                        System.Console.WriteLine($"There is no booking with the refrence number{reff}");
                    }
                    else
                    {
                        System.Console.WriteLine(bk);
                    }
                }
                
            }
            else if (Input == 2)
            {
                if (bookings.Count <= 0)
                {
                    System.Console.WriteLine("No bookings found");
                }
                else
                {
                    foreach (var booking in bookings)
                    {
                        System.Console.WriteLine(booking);
                    }
                }
            }
            else
            {
                System.Console.WriteLine("invalid input! ");
            }
        }

    }
}