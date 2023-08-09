using System;
using AirportTickets.Flight;
using static AirportTickets.Flight.FlightDTO;

namespace AirportTickets.FileInfo
{
    public static class FileData
    {
        public static List<FlightDTO> ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                FlightDTO flight;
                var list = new List<FlightDTO>();
                StreamReader reader = new StreamReader(File.OpenRead(filePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    flight = new FlightDTO()
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
                    list.Add(flight);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}