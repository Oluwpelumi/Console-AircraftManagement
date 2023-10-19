using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IUserInterface
    {
        public User Get(string email);
        public List<User>GetAll();
        public User Login(string email, string password);
        public bool FundWallet(string email, double amount);
        public bool UpdateWallet(string email, double amount);
        public bool Delete(string email);
        public bool Update(string email);
    }
}