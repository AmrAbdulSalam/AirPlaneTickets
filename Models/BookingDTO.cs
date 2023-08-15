using AirportTickets.Models.Reservation;

namespace AirportTickets.Models.Booking
{
    public static class BookingDTO
    {
        private static List<ReservationDTO> _booked = new();

        public static void AddBooking(ReservationDTO reservation)
        {
            _booked.Add(reservation);
        }

        public static List<ReservationDTO> RetreiveBookings()
        {
            return _booked;
        }
    }
}