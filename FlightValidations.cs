using AirportTickets.Flight;
using AirportTickets.FutureDate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace AirportTickets.FlightValidation
{
    public static class FlightValidations
    {
        public static string GetFlightValidations()
        {
            Type flightType = typeof(FlightDTO);
            var sb = new StringBuilder();
            foreach (var property in flightType.GetProperties())
            {
                var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (displayName != null) sb.Append($"{displayName.DisplayName} \n");

                var hasRequiredAttribute = property.GetCustomAttribute(typeof(RequiredAttribute), true) as RequiredAttribute;
                if (hasRequiredAttribute != null) sb.Append($"    *Required\n");

                var stringLength = property.GetCustomAttribute(typeof(StringLengthAttribute), true) as StringLengthAttribute;
                if (stringLength != null) sb.Append($"    *Max Length:{stringLength.MaximumLength} , Min Length: {stringLength.MinimumLength}\n");

                var futureDate = property.GetCustomAttribute(typeof(FutureDateAttribute), true) as FutureDateAttribute;
                if (futureDate != null) sb.Append($"    *Date should be a futrue date\n");

                var attributeType = property.GetCustomAttribute(typeof(DataTypeAttribute), true) as DataTypeAttribute;
                if (attributeType != null)
                {
                    sb.Append($"    *{attributeType.DataType}\n");
                }
            }
            return sb.ToString();
        }
    }
}