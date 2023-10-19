using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Data;
using AircraftManagementApp.Enums;

namespace AircraftManagementApp.Models
{

    public class User
    {

        public int Id;
        public string Name;
        public string Email;
        public string Password;
        public string Address;
        public string PhoneNumber;
        public Gender Gender;
        public double Wallet;
        public string Role;

        public User(int id, string name, string email, string password, string address, string phoneNumber, Gender gender, double wallet, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Wallet = wallet;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Email}\t{Password}\t{Address}\t{PhoneNumber}\t{Gender}\t{Wallet}\t{Role}";
        }

        public static User ToUser(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string name = asd[1];
            string email = asd[2];
            string password = asd[3];
            string address = asd[4];
            string phoneNumber = asd[5];
            // Gender gender = (Gender)int.Parse(asd[6]);
            if (Enum.TryParse<Gender>(asd[6], out Gender gender))
            {
                System.Console.WriteLine();
            }
            double wallet = double.Parse(asd[7]);
            string role = asd[8];

            return new User(id,name,email,password,address,phoneNumber,gender,wallet,role);
        }

       
    }
}