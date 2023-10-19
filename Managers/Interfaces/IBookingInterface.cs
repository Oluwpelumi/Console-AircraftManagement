using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftManagementApp.Models;

namespace AircraftManagementApp.Managers.Interfaces
{
    public interface IBookingInterface
    {
        public Booking Make(string passengerEmail, string flightReferenceNumber);
        public Booking Get(string referenceNumber);
        public List<Booking> GetAll();
        public bool Delete(string referenceNumber);
    }
}