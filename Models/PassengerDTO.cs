using AirportTickets.Models.Flight;

namespace AirportTickets.Models.Passenger
{
    public class PassengerDTO : ILoggable
    {
        private List<FlightDTO> _flights = new();

        public string Name { get; set; }
        public string Passport { get; set; }

        public PassengerDTO()
        {

        }

        public void AddFlight(FlightDTO flight) => _flights.Add(flight);

        public void CancelFlight(FlightDTO flight) => _flights.Remove(flight);

        public List<FlightDTO> ShowFlights() => _flights;

        public FlightDTO SeachForFlight(string id)
        {
            return _flights.Single(flight => flight.ID == id);
        }

        public void ModifyFlight(FlightDTO flight, FlightDTO.ClassType classType)
        {
            flight.ServiceClass = classType;
        }

        public override string ToString() => $"Person : {Name} , Passport.No : {Passport}";

        public void Log() => Console.WriteLine($"{Name} have {_flights.Count()} ");
    }
}

