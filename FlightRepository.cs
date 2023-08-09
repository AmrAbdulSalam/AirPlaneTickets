using System;
using AirportTickets.FileInfo;
using AirportTickets.Flight;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportTickets.Repository
{
    public class FlightRepository
    {
        private List<FlightDTO> _flights;

        public FlightRepository(string filePath)
        {
            _flights = FileData.ReadFile(filePath);
        }

        public List<FlightDTO> FlightsInfo() => _flights;

        public List<FlightDTO> SearchByDeparture(string departureCountry)
        {
            return _flights.Where(flight => flight.Departure.Contains(departureCountry))
                .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SearchByDestination(string destinationCountry)
        {
            return _flights.Where(flight => flight.Destination.Contains(destinationCountry))
                .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SearchByDepartureDate(DateTime date)
        {
            return _flights.Where(flight => flight.DepartureDate.Date == date.Date)
                .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SearchByPrice(decimal price, FlightDTO.ClassType type)
        {
            return _flights.Where(flight => flight.ID == flight.GetPrices(price, type))
                .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SearchByDepartureAirport(string departureAirport)
        {
            return _flights.Where(flight => flight.DepartureAirport == departureAirport)
                .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SearchByArrivalAirport(string arrivalAirport)
        {
            return _flights.Where(flight => flight.ArrivalAirport == arrivalAirport)
               .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public List<FlightDTO> SeachByClassType(FlightDTO.ClassType type)
        {
            return _flights.Where(flight => flight.ID == flight.FindClassType(type))
               .OrderBy(flight => flight.DepartureDate).ToList();
        }

        public FlightDTO SearchForFlight(string id)
        {
            return _flights.Single(flight => flight.ID == id);
        }
    }
}