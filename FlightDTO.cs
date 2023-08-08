using System;

namespace AirportTickets.Flight
{
    public class FlightDTO : ILoggable
    {
        public enum ClassType
        {
            EconEconomy,
            Business,
            FirstClass,
        }

        public string Departure { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureAirport { get; set; }
        public ClassType ServiceClass { get; set; }

        public FlightDTO()
        {

        }

        public override string ToString() => $"From : {Departure} -> To : {Destination}.";

        public void Log() => Console.WriteLine($"{DepartureAirport} -> {ArrivalAirport} At : {DepartureDate}");
    }
}

