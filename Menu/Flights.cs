using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class Flights
    {
        IFlightInterface flightInterface = new FlightManager();
        IPassengerInterface passengerInterface = new PassengerManager();
        public void FlightMgtMenu()
                {
                    bool opt2 = true;
                    while (opt2)
                    {
                        Console.WriteLine("Enter 1 to register Flight\nEnter 2 to view all flights\nEnter 3 to update flight\nEnter 4 to cancel an flight\nEnter 5 to exit");
                        // int opt = int.Parse();
                        if(int.TryParse(Console.ReadLine(), out int opt))
                        {
                            if(opt == 1)
                            {
                                RegisterFlightMenu();
                            }
                            else if (opt == 2)
                            {
                                ViewAllFlightMenu();
                            }
                            else if (opt == 3)
                            {
                                UpdateFlightMenu();
                            }
                            else if (opt == 4)
                            {
                                CancelFlightMenu();
                            }
                            else if (opt == 5)
                            {
                                opt2 = false;
                                // Manager m = new Manager();
                                // m.ManagerMenu();

                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid Input!!");
                            // Thread.Sleep(2000);
                        }
                        
                    }
                    
                }



                 public void RegisterFlightMenu()
                {
                    Console.WriteLine("enter the take-offpoint of the flight");
                    string takeOffPoint = Console.ReadLine();
                    Console.WriteLine("enter destination of the flight");
                    string destination = Console.ReadLine();
                    Console.WriteLine("enter the takeoff time of the flight");
                    bool isValidate = DateTime.TryParse(Console.ReadLine(), out DateTime takeOffTime);
                    // if (isValidate)
                    // {
                        
                    // }
                    // DateTime takeOfTime = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter the pilot staffnumber of the flight");
                    string pilotStaffNumber = Console.ReadLine();
                    Console.WriteLine("enter flight's aircraft name");
                    string aircraftName = Console.ReadLine();
                    Console.WriteLine("enter the price of the flight");
                    double price = double.Parse(Console.ReadLine());
                    System.Console.WriteLine();

                    var pases = passengerInterface.GetAll();
                    foreach (var item in pases)
                    {
                        System.Console.WriteLine(item);
                    }
                    var passengers = AllValidPassengers(price);
                    var register =  flightInterface.Create(takeOffPoint, destination, takeOffTime, pilotStaffNumber, aircraftName, price,passengers);

                    if (register != null)
                    {                    
                        System.Console.WriteLine($"The {register.Name} flight has been registered succesfully");
                    }
                    else
                    {
                        FlightMgtMenu();
                    }
                }                


            private List<string> AllValidPassengers(double price)
            {
                List<string> passengs = new List<string>();
                // bool chk = true;
                int count = 0;
                System.Console.WriteLine("Enter the number of passengers you want to book to this flight");
                bool ck = int.TryParse(Console.ReadLine(), out int num);
                if (ck == true)
                {
                    if (num > 0)
                    {
                        while (count < num)
                        {
                            System.Console.WriteLine("Enter the email of the passenger you want to add to this Flight");
                            string email =  Console.ReadLine();
                            // var isPresent = passengerInterface.Check(email);
                            var psgr = passengerInterface.Get(email);
                            if((psgr != null) && (psgr.Wallet >= price))
                            {
                                passengs.Add(email);
                            }
                            else
                            {
                                System.Console.WriteLine($"Passenger with the email {email} does not exist!");
                            }
                            count ++;
                        } 
                    }
                    else if(num == 0)
                    {
                        System.Console.WriteLine("No passenger booked with the flight yet");
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid Input! You've entered a negative value...... No passenger added");
                    }

                    return passengs;
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!...... No passenger added");
                    return passengs;
                }
            }


            public void ViewAllFlightMenu()
            {
                var flights = flightInterface.GetAll();
                foreach (var flight in flights)
                {
                    System.Console.WriteLine(flight);
                }
            }



            public void UpdateFlightMenu()
            {
                // var flights  = flightInterface.GetAll();
                // foreach (var flight in flights)
                // {
                //     System.Console.WriteLine(flight);
                // }

                ViewAllFlightMenu();

                System.Console.WriteLine("Enter the reference number of the flight you wanna update: ");
                string refNum = Console.ReadLine();

                var edit = flightInterface.Get(refNum);
                if (edit == null)
                {
                    System.Console.WriteLine($"Flight with the reference number {refNum} does not exist! ");
                }
                else
                    {
                        System.Console.WriteLine("Do you want to update the takeoff point of the Flight?: (y/n)");
                        string option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new takeoff point: ");
                            edit.TakeOffPoint = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the  destination of the Flight?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new destination: ");
                            edit.Destination = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the takeoff time of the Flight?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new takeoff time: ");
                            edit.TakeOfTime = DateTime.Parse(Console.ReadLine());
                        }

                        System.Console.WriteLine("Do you want to change the pilot staff number for the Flight?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new staff number: ");
                            edit.PilotStaffNumber = Console.ReadLine();
                        }

                        if (flightInterface.Update(refNum))
                        {
                            System.Console.WriteLine($"Successfully Updated the flight with the refrence number: {edit.ReferenceNumber}!");
                            FlightMgtMenu();
                        }
                        else
                        {
                            System.Console.WriteLine("An error has occured!");
                        }
                    }

            }


            public void CancelFlightMenu()
            {
                var flights  = flightInterface.GetAll();
                foreach (var flight in flights)
                {
                    System.Console.WriteLine(flight);
                }

                System.Console.WriteLine("Enter the reference number of the flight you wanna cancel: ");
                string refNum = Console.ReadLine();

                var delete = flightInterface.Cancel(refNum);
                if (delete == true)
                {
                    System.Console.WriteLine($"The Flight with the reference number {refNum} has been cancelled successfully!");
                    FlightMgtMenu();
                }
                else
                {
                    System.Console.WriteLine($"Unable to cancel flight with reference number {refNum}!");
                }
            }
    }
}