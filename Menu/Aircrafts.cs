using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class Aircrafts
    {
        IAircraftInterface aircraftInterface = new AircraftManager();
        public void AircraftMgtMenu()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Enter 1 to register aircraft\nEnter 2 to view all aircrafts\nEnter 3 to update aircraft\nEnter 4 to delete an aircraft\nEnter 5 to exit");
                if(int.TryParse(Console.ReadLine(), out int opt4))
                {
                    if(opt4 == 1)
                    {
                        RegisterAircraftMenu();
                    }
                    else if (opt4 == 2)
                    {
                        ViewAllAircraftMenu();
                    }
                    else if (opt4 == 3)
                    {
                        UpdateAircraftMenu();
                    }
                    else if (opt4 == 4)
                    {
                        DeleteAircraftMenu();
                    }
                    else if (opt4 == 5)
                    {
                        check = false;
                        // Manager mm = new Manager();
                        // mm.ManagerMenu();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input!!");
                    // Thread.Sleep(2000);
                }
            }
            
        }


             public void RegisterAircraftMenu()
                {
                    Console.WriteLine("enter aircraft name");
                    string name = Console.ReadLine();
                    Console.WriteLine("enter the engine number of the aircraft");
                    string engineNumber = Console.ReadLine();
                    Console.WriteLine("enter the capacity of the aircraftf");
                    int capacity = int.Parse(Console.ReadLine());

                    var register =  aircraftInterface.Register(name,engineNumber,capacity);

                    if (register != null)
                    {
                        System.Console.WriteLine($"The {register.Name} aircraft has been registered succesfully");
                    }
                    else
                    {
                        RegisterAircraftMenu();
                    }
                }

                public void ViewAllAircraftMenu()
                {
                    var aircrafts = aircraftInterface.GetAll();
                    foreach (var aircraft in aircrafts)
                    {
                        System.Console.WriteLine(aircraft);
                    }
                    // AirPortMgtMenu();
                } 

                public void UpdateAircraftMenu()
                {
                    var aircrafts = aircraftInterface.GetAll();
                    foreach (var aircraft in aircrafts)
                    {
                        System.Console.WriteLine(aircraft);
                    }
                    System.Console.WriteLine("Enter the id of the Aircraft you want to Update: ");
                    bool check2 = int.TryParse(Console.ReadLine(), out int idNum);
                    if (check2 == false)
                    {
                        System.Console.WriteLine("Invalid Input!!");
                        AircraftMgtMenu();
                    }
                    var edit = aircraftInterface.GetById(idNum);
                    
                    if (edit == null)
                    {
                        System.Console.WriteLine($"Aircraft with the id number {idNum} does not exist!");
                    }
                    else
                    {
                        System.Console.WriteLine("Do you want to update the name of the Aircraft?: (y/n)");
                        string option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new name: ");
                            edit.Name = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the engine number of the Aircraft?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new engine number: ");
                            edit.EngineNumber = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the capacity of the Aircraft?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new capacity: ");
                            edit.Capacity = int.Parse(Console.ReadLine());
                        }

                        if (aircraftInterface.Update(edit.Name))
                        {
                            System.Console.WriteLine($"Successfully Updated {edit.Name} Aircraft!");
                            AircraftMgtMenu();
                        }
                        else
                        {
                            System.Console.WriteLine("An error has occured!");
                        }
                    }
                }


                public void DeleteAircraftMenu()
                {
                    var aircrafts = aircraftInterface.GetAll();
                    foreach (var aircraft in aircrafts)
                    {
                        System.Console.WriteLine(aircraft);
                    }
                    System.Console.WriteLine("Enter the name of the aircraft you want to delete: ");
                    string name = Console.ReadLine();

                    var delete = aircraftInterface.Delete(name);
                    if (delete == true)
                    {
                        System.Console.WriteLine($"{name} Aircraft has been deleted Successfully! ");
                        AircraftMgtMenu();
                    }
                    else
                    {
                        System.Console.WriteLine($"{name} Aircraft does not exist, Invalid Aircraft name!!");
                    }
                }
    }
}