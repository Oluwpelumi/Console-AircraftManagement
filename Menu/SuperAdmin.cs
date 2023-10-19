using System.Reflection;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Managers.Implementations;
using AircraftManagementApp.Managers.Interfaces;

namespace AircraftManagementApp.Menu
{
    public class SuperAdmin
    {
        IManagerInterface managerInterface = new ManagersManager();
        public void SuperMenu()
        {
            bool opt2 = true;
            while(opt2)
            {
                System.Console.WriteLine("enter 1 to register manager\nenter 2 to view all managers\nenter 3 to update manager account\nenter 4 to delete manager\nenter 5 to logout");
                // int opt = int.Parse(Console.ReadLine());
                bool opt = int.TryParse(Console.ReadLine(), out int num);
                if (opt == true)
                {
                    if(num == 1)
                    {
                        RegisterManagerMenu();
                    }
                    else if(num == 2)
                    {
                        ViewAllManagersMenu();
                    }
                    else if(num == 3)
                    {
                        UpdateManagerMenu();
                    }
                    else if(num == 4)
                    {
                        DeleteManagerMenu();
                    }
                    else if(num == 5)
                    {
                        opt2 =  false;
                    }
                    else
                    {
                        System.Console.WriteLine("The value you input is out of range.....try again");
                        SuperMenu();
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid Input");
                }
            }
        }

        public void RegisterManagerMenu()
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

            var response = managerInterface.Register(name,email,password,address,phoneNumber,(Gender)gender);
            if(response != null) 
            {
                System.Console.WriteLine("succesfull");
            }
            else
            {
                System.Console.WriteLine("Unable to register manager !!!");
                SuperMenu();
            }
            

        }


        public void ViewAllManagersMenu()
        {
            var managers = managerInterface.GetAll();
            foreach (var manager in managers)
            {
                System.Console.WriteLine(manager);
            }
        }



        public void UpdateManagerMenu()
        {
            ViewAllManagersMenu();

            System.Console.WriteLine("Enter the staffNumber of the manager you want to update: ");
            string staffnum = Console.ReadLine();
            var edit = managerInterface.Get(staffnum);
            if (edit == null)
            {
                System.Console.WriteLine($"The manager with the staffNumber {staffnum} doesn't exist");
            }
            else
            {
                System.Console.WriteLine("Do you want to update the email of the Manager?: (y/n)");
                string option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new email: ");
                    edit.UserEmail = Console.ReadLine();
                }

                System.Console.WriteLine("Do you want to update the staffNumber of the Manager?: (y/n)");
                option = Console.ReadLine().Trim().ToLower();
                if (option == "y")
                {
                    Console.Write("Enter the new staffNumber: ");
                    edit.StaffNumber = Console.ReadLine();
                }
                else
                {
                    System.Console.WriteLine("An error has occured!");
                }
            }
        }



        public void DeleteManagerMenu()
        {
            var managers = managerInterface.GetAll();
    
            foreach (var manager in managers)
            {
                System.Console.WriteLine(manager);
            }

            System.Console.WriteLine("Enter the staff Number of the manager you want to delete: ");
            string staffnum = Console.ReadLine();
             var delete = managerInterface.Delete(staffnum);

             if(delete == true)
             {
                System.Console.WriteLine($"The manager with the staff Number {staffnum} has been deleted successfully!1");
                SuperMenu();
             }
             else
             {
                System.Console.WriteLine($"Unable to delete! Manager with the {staffnum} does not exist");
             }
        }


    }
}