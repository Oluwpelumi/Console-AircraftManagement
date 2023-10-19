using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Models;
 
namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IAircraftInterface
    {
        public Aircraft Register(string name, string engineNumber, int capacity);
        public Aircraft Get(string name);
        public List<Aircraft> GetAll();
        public bool Update(string name);
        public bool Delete(string name);
        public Aircraft GetById(int id);
    }
}