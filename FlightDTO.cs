using System;

namespace AirportTickets.Flight
{
    public class FlightDTO : ILoggable
    {
        public enum ClassType
        {
            Economy,
            Business,
            FirstClass,
        }

        private Dictionary<ClassType, decimal> _prices = new();

        public string ID { get; init; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureAirport { get; set; }
        public ClassType ServiceClass { get; set; }

        public FlightDTO()
        {

        }

        public void AddPrice(ClassType classType, decimal price)
        {
            _prices.Add(classType, price);
        }

        public string FindClassType(ClassType classType)
        {
            foreach (var item in _prices)
            {
                if(item.Key == classType && item.Value != null)
                {
                    return ID;
                }
            }
            return null;
        }

        public string GetPrices(decimal price, ClassType type) => _prices[type] == price ? ID : null;

        public override string ToString() => $"From : {Departure} -> To : {Destination}.";

        public void Log() => Console.WriteLine(
            $"FlightID : {ID} {Departure}:{DepartureAirport} -> {Destination}:{ArrivalAirport} At : {DepartureDate} \n" +
            $"{ClassType.Economy} = {_prices[ClassType.Economy]}$, {ClassType.Business} = {_prices[ClassType.Business]}$" +
            $", {ClassType.FirstClass} = {_prices[ClassType.FirstClass]}$ ");
    }
}