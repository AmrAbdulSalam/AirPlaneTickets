using System.ComponentModel.DataAnnotations;
using AirportTickets.Models.Flight;
using AirportTickets.Validation;
using static AirportTickets.Models.Flight.FlightDTO;

namespace AirportTickets.FileInfo
{
    public class FileReader
    {
        public static async Task<List<FlightDTO>> Read(string filePath)
        {
            if (!File.Exists(filePath)) throw new ValidationException();

            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                var list = new List<FlightDTO>();
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');
                    var flight = new FlightDTO()
                    {
                        ID = values[0],
                        Departure = values[1],
                        DepartureAirport = values[2],
                        Destination = values[3],
                        ArrivalAirport = values[4],
                        DepartureDate = DateTime.Parse(values[5]),
                    };
                    flight.AddPrice(ClassType.Economy, Decimal.Parse(values[6]));
                    flight.AddPrice(ClassType.Business, Decimal.Parse(values[7]));
                    flight.AddPrice(ClassType.FirstClass, Decimal.Parse(values[8]));

                    var lineNo = 0;
                    var valid = DataValidation.ValidData(flight, ref lineNo);
                    if (valid)
                    {
                        list.Add(flight);
                    }
                }
                return list;
            }
        }
    }
}