using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IManagerInterface
    {
        public Manager Register(string name, string email,string password,string address,string phoneNumber,Gender gender);
        public  Manager Get(string staffNumber);
        public   List<Manager> GetAll();    
        public  bool Update(string staffNumber);
        public bool Delete(string staffNumber);
         
    }
}