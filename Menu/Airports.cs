using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class Airports
    {
        IAirportInterface airportInterface = new AirportManager();
         public void AirPortMgtMenu()
        {
            bool opt2 = true;
            while (opt2)
            {
                Console.WriteLine("Enter 1 to register airport\nEnter 2 to view all airports\nEnter 3 to update airport\nEnter 4 to delete an airport\nEnter 5 to exit");
                // int opt = int.Parse();
                if(int.TryParse(Console.ReadLine(), out int opt))
                {
                    if(opt == 1)
                    {
                        RegisterAirportMenu();
                    }
                    else if (opt == 2)
                    {
                        ViewAllAirportMenu();
                    }
                    else if (opt == 3)
                    {
                        UpdateAirportMenu();
                    }
                    else if (opt == 4)
                    {
                        DeleteAirportMenu();
                    }
                    else if (opt == 5)
                    {
                        opt2 = false;
                        // Manager k = new Manager();
                        // k.ManagerMenu();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
                
            }
            
        }


        public void RegisterAirportMenu()
            {
                Console.WriteLine("enter airport name");
                string name = Console.ReadLine();
                Console.WriteLine("enter airport location");
                string location = Console.ReadLine();
                Console.WriteLine("enter 1 for local and enter 2 for international");
                int airportType = int.Parse(Console.ReadLine());

                var register =  airportInterface.Register(name,location,(AirportType)airportType);

                if (register != null)
                {
                    System.Console.WriteLine($"The {register.Name} airport has been registered succesfully");
                    // AirPortMgtMenu();
                }
                else
                {
                    RegisterAirportMenu();
                }
            }

        public void ViewAllAirportMenu()
        {
            var airports = airportInterface.GetAll();
            foreach (var airport in airports)
            {
                System.Console.WriteLine(airport);
            }
           
        } 

        public void UpdateAirportMenu()
        {
            var airports = airportInterface.GetAll();
            foreach (var airport in airports)
            {
                System.Console.WriteLine(airport);
            }
            System.Console.WriteLine("Enter the name of the Airport you want to Update: ");
            string name = Console.ReadLine();
            var edit = airportInterface.Get(name);
            if (edit == null)
            {
                System.Console.WriteLine($"Airport with the name {name} does not exist!");
            }
            else
            {
                System.Console.WriteLine("Do you want to update the name of the Airport?: (y/n)");
                string option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new name: ");
                    edit.Name = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update the location of the Airport?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new location: ");
                    edit.Location = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update the airport-type of the Airport?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new airport-type: ");
                    edit.AirportType = (AirportType)int.Parse(Console.ReadLine());
                }

                if (airportInterface.Update(edit.Name))
                {
                    System.Console.WriteLine($"Successfully Updated {edit.Name} Airport!");
                    AirPortMgtMenu();
                }
                else
                {
                    System.Console.WriteLine("An error has occured!");
                }
            }
        }


        public void DeleteAirportMenu()
        {
            var airports = airportInterface.GetAll();
            foreach (var airport in airports)
            {
                System.Console.WriteLine(airport);
            }
            System.Console.WriteLine("Enter the name of the airport you want to delete: ");
            string name = Console.ReadLine();

            var delete = airportInterface.Delete(name);
            if (delete == true)
            {
                System.Console.WriteLine($"{name} Airport has been deleted Successfully! ");
                AirPortMgtMenu();
            }
            else
            {
                System.Console.WriteLine($"{name} Airport does not exist, Invalid Airport name!!");
            }
        }

    }
}