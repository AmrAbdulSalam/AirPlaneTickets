using System.ComponentModel.DataAnnotations;
using AirportTickets.Flight;

namespace AirportTickets.Validation
{
    public static class DataValidation
    {
        public static bool ValidData(FlightDTO flight, ref int lineNo)
        {
            lineNo++;
            var context = new ValidationContext(flight, null, null);
            var validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(flight, context, validationResults, true);
            if (!valid)
            {
                Console.WriteLine($"At line {lineNo}");
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}", validationResult.ErrorMessage);
                }
            }
            return valid;
        }
    }
}