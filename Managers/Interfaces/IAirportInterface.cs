using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Enums;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IAirportInterface
    {
       public Airport Register(string name, string location, AirportType airportType);
       public Airport Get(string name);
       public List<Airport> GetAll();
       public bool Update(string name);
       public bool Delete(string name);
    }
}