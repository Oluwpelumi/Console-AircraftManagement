using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IFlightInterface
    {
        public Flight Create(string takeOffPoint, string destination, DateTime takeOfTime, string pilotStaffNumber, string aircraftName, double price, List<string> passengers);
        public Flight Get(string referenceNumber);
        public List<Flight> GetAll();
        public bool Update(string referenceNumber);
        public bool Cancel(string referenceNumber);



    }
}