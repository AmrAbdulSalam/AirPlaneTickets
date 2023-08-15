using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AirportTickets.FutureDate;

namespace AirportTickets.Models.Flight
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
        [DisplayName("Flight unique ID :")]
        public string ID { get; init; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [DisplayName("Departure country :")]
        public string Departure { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [DisplayName("Destination country :")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "{0} Must be a futrue date")]
        [DisplayName("Departure Date :")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Arival Airport should only have 3 character according to  'IATA code'")]
        [DisplayName("Arrival Airport :")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Departure Airport should only have 3 character according to  'IATA code'")]
        [DisplayName("Departure Airport :")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(ClassType), ErrorMessage = "Invalid Class")]
        [DisplayName("Class Type (Econmeny/Bussniess/FirstClass) :")]
        public ClassType ServiceClass { get; set; }

        public FlightDTO()
        {

        }

        public void AddPrice(ClassType classType, decimal price)
        {
            _prices.Add(classType, price);
        }

        public string FindByClassType(ClassType classType) => GetFlightByPrice(_prices[classType], classType);

        public string GetFlightByPrice(decimal price, ClassType type) => _prices[type] == price ? ID : null;

        public override string ToString() => $"From : {Departure} -> To : {Destination}.";

        public void Log() => Console.WriteLine(
            $"FlightID : {ID} {Departure}:{DepartureAirport} -> {Destination}:{ArrivalAirport} At : {DepartureDate} \n" +
            $"{ClassType.Economy} = {_prices[ClassType.Economy]}$, {ClassType.Business} = {_prices[ClassType.Business]}$" +
            $", {ClassType.FirstClass} = {_prices[ClassType.FirstClass]}$ ");
    }
}