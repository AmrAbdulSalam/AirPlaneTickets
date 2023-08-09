using System;
using AirportTickets.Flight;

namespace AirportTickets
{
    public class ReservationDTO
    {
        public string Name { get; private set; }
        public FlightDTO Flight { get; private set; }

        public ReservationDTO(string name, FlightDTO flight)
        {
            Name = name;
            Flight = flight;
        }
    }
}
