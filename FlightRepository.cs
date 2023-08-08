using System;
using AirportTickets.FileInfo;
using AirportTickets.Flight;

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
    }
}