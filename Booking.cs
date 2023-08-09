using System;
namespace AirportTickets.Booking
{
    public class BookingDTO
    {
        List<ReservationDTO> _booked = new();

        public BookingDTO()
        {

        }

        public void AddBooking(ReservationDTO reservation)
        {
            _booked.Add(reservation);
        }

        public List<ReservationDTO> RetreiveBookings()
        {
            return _booked;
        }
    }
}