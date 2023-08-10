using System;
using System.ComponentModel.DataAnnotations;

namespace AirportTickets.FutureDate
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (value is DateTime date)
            {
                return date >= DateTime.Today;
            }
            return false;
        }
    }
}