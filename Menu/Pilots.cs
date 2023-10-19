using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class Pilots
    {
        IPilotInterface pilotInterface = new PilotManager();
        IUserInterface userInterface = new UserManager();

        public void PilotMgtMenu()
            {

                bool check = true;
                while (check)
                {
                    Console.WriteLine("Enter 1 to register a pilot\nEnter 2 to view all pilots\nEnter 3 to update a pilot account\nEnter 4 to delete a pilot\nEnter 5 to exit");
                    if(int.TryParse(Console.ReadLine(), out int opt4))
                    {
                        if(opt4 == 1)
                        {
                            RegisterPilotMenu();
                        }
                        else if (opt4 == 2)
                        {
                            ViewAllPilotMenu();
                        }
                        else if (opt4 == 3)
                        {
                            UpdatePilotMenu();
                        }
                        else if (opt4 == 4)
                        {
                            DeletePilotMenu();
                        }
                        else if (opt4 == 5)
                        {
                            check = false;
                            // Manager m = new Manager();
                            // m.ManagerMenu();
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid Input!!");
                        // PilotMgtMenu();
                        // Thread.Sleep(2000);
                    }
                }

            }

        public void RegisterPilotMenu()
                {
                    Console.WriteLine("enter pilot name");
                    string name = Console.ReadLine();
                    Console.WriteLine("enter pilot email");
                    string email = Console.ReadLine();
                    Console.WriteLine("enter the pilot's password:");
                    string password = Console.ReadLine();
                    Console.WriteLine("enter the pilot's address:");
                    string address = Console.ReadLine();
                    Console.WriteLine("enter the pilot's phone number:");
                    string phoneNumber = Console.ReadLine();
                    Console.WriteLine("Enter 1 for male and enter 2 for female:");
                    int gender = int.Parse(Console.ReadLine());

                    var register =  pilotInterface.Register(name,email,password,address,phoneNumber,(Gender)gender);

                    if (register != null)
                    {
                        System.Console.WriteLine($"The pilot with the staff number {register.StaffNumber}  has been registered succesfully");
                    }
                    else
                    {
                        RegisterPilotMenu();
                    }
                }

                public void ViewAllPilotMenu()
                {
                    var pilots = pilotInterface.GetAll();
                    foreach (var pilot in pilots)
                    {
                        System.Console.WriteLine(pilot);
                    }
                    // AirPortMgtMenu();
                } 

                public void UpdatePilotMenu()
                {
                    ViewAllPilotMenu();
                    System.Console.WriteLine("Enter the user email of the Pilot you want to Update: ");
                    string email = Console.ReadLine();
                    bool check2 = pilotInterface.Check(email);
                    if (check2 == false)
                    {
                        System.Console.WriteLine($"The Pilot with the email {email} doesn't exist!");
                        PilotMgtMenu();
                    }
                    var edit = userInterface.Get(email);
                    
                    if (edit == null)
                    {
                        System.Console.WriteLine($"The Pilot with the email {email} doesn't exist!");
                    }
                    else
                    {
                        System.Console.WriteLine("Do you want to update the name of the Pilot?: (y/n)");
                        string option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new name: ");
                            edit.Name = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the e-mail of the Pilot?:: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new e-mail : ");
                            edit.Email = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the Address of the Pilot?:?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new Address: ");
                            edit.Address = Console.ReadLine();
                        }

                        System.Console.WriteLine("Do you want to update the Phone Number of the Pilot?:?: (y/n)");
                        option = Console.ReadLine().Trim().ToLower();
                        if (option == "y")
                        {
                            Console.Write("Enter the new Phone Number: ");
                            edit.PhoneNumber = Console.ReadLine();
                        }

                        if (pilotInterface.Update(edit.Email) && userInterface.Update(edit.Email))
                        {
                            System.Console.WriteLine($"Successfully Updated Pilot {edit.Name} Profile!");
                            PilotMgtMenu();
                        }
                        else
                        {
                            System.Console.WriteLine("An error has occured!");
                        }
                    }
                }

        public void DeletePilotMenu()
                {
                    ViewAllPilotMenu();
                    System.Console.WriteLine("Enter the email of the pilot you want to delete: ");
                    string email = Console.ReadLine();

                    var delete = pilotInterface.Delete(email);
                    var udelete = userInterface.Delete(email);
                    if ((delete == true) && (udelete == true))
                    {
                        System.Console.WriteLine($"Pilot with the {email} has been deleted Successfully! ");
                        PilotMgtMenu();
                    }
                    else
                    {
                        System.Console.WriteLine($"Pilot with the {email} does not exist, Invalid Pilot email!!");
                    }
                }
    }
}