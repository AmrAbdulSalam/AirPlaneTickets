using AirportTickets.Flight;
using AirportTickets.Passenger;

namespace AirportTickets
{
    public class ReservationDTO
    {
        public PassengerDTO Passenger { get; private set; }
        public FlightDTO Flight { get; private set; }
        public string ID { get; set; }

        public ReservationDTO(PassengerDTO passenger, FlightDTO flight, string id)
        {
            Passenger = passenger;
            Flight = flight;
            ID = id;
        }
    }
}