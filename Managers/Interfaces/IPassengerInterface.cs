using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IPassengerInterface
    {
        public Passenger Register(string name, string userEmail, string password, string address,string phoneNumber,Gender gender);
        public Passenger Get(string email);
        public  List<Passenger> GetAll();
        public bool Update(string email);
        public bool Delete(string email);
        public bool Check(string email);
    }

}