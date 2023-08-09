using System;
using AirportTickets.Booking;
using AirportTickets.Flight;
using static AirportTickets.Flight.FlightDTO;

namespace AirportTickets
{
    public class ManagerDTO
    {
        public string Name { get; set; }

        public ManagerDTO()
        {

        }

        public List<ReservationDTO> FilterByFlight(FlightDTO flight)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight == flight)
                .ToList();
        }

        public List<ReservationDTO> FilterByPrice(decimal price)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item =>
                   item.Flight.GetPrices(price, ClassType.Economy).Equals(price)
                || item.Flight.GetPrices(price, ClassType.Business).Equals(price)
                || item.Flight.GetPrices(price, ClassType.FirstClass).Equals(price))
                .ToList();
        }

        public List<ReservationDTO> FilterByDepartureCountry(string departureCountry)
        {
            return
                BookingDTO.RetreiveBookings().
                Where(item => item.Flight.Departure == departureCountry)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByDestinationCountry(string destinationCountry)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight.Destination == destinationCountry)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByDepartureDate(DateTime date)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight.DepartureDate.Date == date)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByDepartureAirport(string departureAirport)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight.DepartureAirport == departureAirport)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByArrivalAirport(string arrivalAirport)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight.ArrivalAirport == arrivalAirport)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByName(string name)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Name == name)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public List<ReservationDTO> FilterByClassType(ClassType classType)
        {
            return
                BookingDTO.RetreiveBookings()
                .Where(item => item.Flight.ServiceClass == classType)
                .OrderBy(item => item.Name)
                .ToList();
        }
    }
}