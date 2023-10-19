using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IPilotInterface
    {
        public Pilot Register(string name, string email,string password,string address,string phoneNumber,Gender gender);
        public Pilot Get(string useremail);
        public List<Pilot> GetAll();
        public bool Update (string useremail);
        public bool Delete (string useremail);
        public bool Check(string useremail);
         

    }
}