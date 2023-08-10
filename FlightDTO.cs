using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AirportTickets.FutureDate;

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

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        public string ID { get; init; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        public string Departure { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        public string Destination { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "{0} Must be a futrue date")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Arival Airport should only have 3 character according to  'IATA code'")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Departure Airport should only have 3 character according to  'IATA code'")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(ClassType), ErrorMessage = "Invalid Class")]
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
                if (item.Key == classType && item.Value != null)
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