using System;
using AirportTickets.Flight;
using AirportTickets.Repository;
using AirportTickets.Passenger;
using AirportTickets.Booking;
using AirportTickets;
using AirportTickets.FlightValidation;

namespace AirPlanceTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine(FlightValidations.GetFlightValidations());

            Console.WriteLine("Enter the file path that have a .CSV file : ");
            string? path = Console.ReadLine();
            var flightRepository = new FlightRepository(path);
            await flightRepository.ReadAsyncFile();
            var flightInfo = flightRepository.GetAllFlightsInfo();
            List<FlightDTO> filterdList = null;
            var newUser = true;
            var passenger = new PassengerDTO();
            var manager = new ManagerDTO();
            List<ReservationDTO> mangerList = null;
            while (true)
            {
                Console.WriteLine
                    ($"Sign in as a Passenger or Manager : \n" +
                    $"1-New Passenger\n" +
                    $"2- Passenger. \n" +
                    $"3- Manager. \n" +
                    $"4- Exit");
                if (newUser == true)
                {
                    Console.WriteLine("Name :");
                    var name = Console.ReadLine();
                    Console.WriteLine("Passport No. :");
                    var passport = Console.ReadLine();
                    passenger = new PassengerDTO()
                    {
                        Name = name,
                        Passport = passport
                    };
                    newUser = false;
                    Console.WriteLine
                            (
                             $"2- Passenger. \n" +
                             $"3- Manager. \n" +
                             $"4- Exit");
                }

                var userType = Console.ReadLine();
                if (userType == "1")
                {
                    newUser = true;
                }
                if (userType == "2")
                {
                    Console.WriteLine
                        ($"1-Show All Flights. \n" +
                        $"2-Search by Price. \n" +
                        $"3-Departure Country. \n" +
                        $"4-Destination Country \n" +
                        $"5-Departure Date. \n" +
                        $"6-Departure Airport. \n" +
                        $"7-Arrival Airport. \n" +
                        $"8-Class Type. \n" +
                        $"9-Cancel booking.\n" +
                        $"10-Modify booking ");
                    var userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            filterdList = flightInfo;
                            break;
                        case "2":
                            var price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine($"Which class\n" +
                               $"1-Econmey \n" +
                               $"2-Business \n" +
                               $"3-FirstClass \n" +
                               $"Write the price \n");
                            var choice = Console.ReadLine();
                            FlightDTO.ClassType type = FlightDTO.ClassType.Economy;
                            if (choice == "1")
                            {
                                type = FlightDTO.ClassType.Economy;
                            }
                            else if (choice == "2")
                            {
                                type = FlightDTO.ClassType.Business;
                            }
                            else if (choice == "3")
                            {
                                type = FlightDTO.ClassType.FirstClass;
                            }
                            filterdList = flightRepository.SearchByPrice(price, type);
                            break;
                        case "3":
                            var departureCity = Console.ReadLine();
                            filterdList = flightRepository.SearchByDeparture(departureCity);
                            break;
                        case "4":
                            var destinationCity = Console.ReadLine();
                            filterdList = flightRepository.SearchByDestination(destinationCity);
                            break;
                        case "5":
                            var departureDate = Console.ReadLine();
                            filterdList = flightRepository.SearchByDepartureDate(DateTime.Parse(departureDate));
                            break;
                        case "6":
                            var departureAirport = Console.ReadLine();
                            filterdList = flightRepository.SearchByDepartureAirport(departureAirport);
                            break;
                        case "7":
                            var arrivalAirport = Console.ReadLine();
                            filterdList = flightRepository.SearchByArrivalAirport(arrivalAirport);
                            break;
                        case "8":
                            Console.WriteLine($"Which class\n" +
                               $"1-Econmey \n" +
                               $"2-Business \n" +
                               $"3-FirstClass \n");
                            choice = Console.ReadLine();
                            type = FlightDTO.ClassType.Economy;
                            if (choice == "1")
                            {
                                type = FlightDTO.ClassType.Economy;
                            }
                            else if (choice == "2")
                            {
                                type = FlightDTO.ClassType.Business;
                            }
                            else if (choice == "3")
                            {
                                type = FlightDTO.ClassType.FirstClass;
                            }
                            filterdList = flightRepository.SeachByClassType(type);
                            break;
                        case "9":
                            Console.WriteLine("Put ID for a flight");
                            var flightId = Console.ReadLine();
                            var canceldFlight = flightRepository.SearchForFlight(flightId);
                            passenger.CancelFlight(canceldFlight);
                            break;
                        case "10":
                            Console.WriteLine("Put ID for a flight");
                            flightId = Console.ReadLine();
                            var modifyFlight = passenger.SeachForFlight(flightId);
                            Console.WriteLine("Change it to : 1-Econmey 2-Bussnies 3-FirstClass");
                            var bookChoice = Console.ReadLine();
                            FlightDTO.ClassType bookType = modifyFlight.ServiceClass;
                            if (bookChoice == "1")
                            {
                                bookType = FlightDTO.ClassType.Economy;
                            }
                            else if (bookChoice == "2")
                            {
                                bookType = FlightDTO.ClassType.Business;
                            }
                            else if (bookChoice == "3")
                            {
                                bookType = FlightDTO.ClassType.FirstClass;
                            }
                            passenger.ModifyFlight(modifyFlight, bookType);

                            break;
                    }
                    if (filterdList.Count() > 0)
                    {
                        foreach (var flight in filterdList)
                        {
                            flight.Log();
                        }
                        filterdList.Clear();
                    }
                    Console.WriteLine("Book a flight y/n?");
                    var question = Console.ReadLine();
                    if (question == "y")
                    {
                        Console.WriteLine("To book a flight please enter the id of the flight");
                        var flightId = Console.ReadLine();
                        Console.WriteLine($"Which class\n" +
                                   $"1-Econmey \n" +
                                   $"2-Business \n" +
                                   $"3-FirstClass \n");
                        var bookChoice = Console.ReadLine();
                        var bookType = FlightDTO.ClassType.Economy;
                        if (bookChoice == "1")
                        {
                            bookType = FlightDTO.ClassType.Economy;
                        }
                        else if (bookChoice == "2")
                        {
                            bookType = FlightDTO.ClassType.Business;
                        }
                        else if (bookChoice == "3")
                        {
                            bookType = FlightDTO.ClassType.FirstClass;
                        }
                        var bookedFlight = flightRepository.SearchForFlight(flightId);
                        bookedFlight.ServiceClass = bookType;
                        passenger.AddFlight(bookedFlight);
                        BookingDTO.AddBooking(new ReservationDTO(passenger, bookedFlight, flightId));
                        Console.WriteLine($"You have {passenger.ShowFlights().Count()} flights");
                        foreach (var flight in passenger.ShowFlights())
                        {
                            Console.WriteLine($"You booked {flight.ToString()} At : {flight.DepartureDate} \n" +
                                $"And you booked {flight.ServiceClass}");
                        }
                    }

                }
                else if (userType == "3")
                {

                    Console.WriteLine("1-Filter by Flight\n" +
                        "2-Filter by Price\n" +
                        "3-Filter by Departure Countrt\n" +
                        "4-Filter by Destenation Country\n" +
                        "5-Filter by Departure Date\n" +
                        "6-Filter by Deaprture Airport\n" +
                        "7-Filter by Arrival Airport\n" +
                        "8-Filter by Passenger name\n" +
                        "9-Filter by Class Type\n" +
                        "10-Validation Attributes");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Flight id :");
                            var id = Console.ReadLine();
                            FlightDTO flight = flightRepository.SearchForFlight(id);
                            mangerList = manager.FilterByFlight(flight);
                            break;
                        case "2":
                            Console.WriteLine("Price :");
                            var price = Decimal.Parse(Console.ReadLine());
                            mangerList = manager.FilterByPrice(price);
                            break;
                        case "3":
                            Console.WriteLine("Departure Country");
                            var departureCountry = Console.ReadLine();
                            mangerList = manager.FilterByDepartureCountry(departureCountry);
                            break;
                        case "4":
                            Console.WriteLine("Destenation Country");
                            var destinationCountry = Console.ReadLine();
                            mangerList = manager.FilterByDestinationCountry(destinationCountry);
                            break;
                        case "5":
                            Console.WriteLine("Departure Date");
                            var departureDate = DateTime.Parse(Console.ReadLine());
                            mangerList = manager.FilterByDepartureDate(departureDate);
                            break;
                        case "6":
                            Console.WriteLine("Departure Airport");
                            var departureAirport = Console.ReadLine();
                            mangerList = manager.FilterByDepartureAirport(departureAirport);
                            break;
                        case "7":
                            Console.WriteLine("Arrival Airport");
                            var arrivalAirport = Console.ReadLine();
                            mangerList = manager.FilterByArrivalAirport(arrivalAirport);
                            break;
                        case "8":
                            Console.WriteLine("Passenger Name");
                            var passengerName = Console.ReadLine();
                            mangerList = manager.FilterByName(passengerName);
                            break;
                        case "9":
                            Console.WriteLine($"Which class\n" +
                                   $"1-Econmey \n" +
                                   $"2-Business \n" +
                                   $"3-FirstClass \n");
                            var bookChoice = Console.ReadLine();
                            var bookType = FlightDTO.ClassType.Economy;
                            if (bookChoice == "1")
                            {
                                bookType = FlightDTO.ClassType.Economy;
                            }
                            else if (bookChoice == "2")
                            {
                                bookType = FlightDTO.ClassType.Business;
                            }
                            else if (bookChoice == "3")
                            {
                                bookType = FlightDTO.ClassType.FirstClass;
                            }
                            mangerList = manager.FilterByClassType(bookType);
                            break;
                        case "10":
                            Console.WriteLine(FlightValidations.GetFlightValidations());
                            break;
                    }
                    if (mangerList != null)
                    {
                        foreach (var item in mangerList)
                        {
                            Console.WriteLine($"Name : {item.Passenger.Name} & Paasport number : {item.Passenger.Passport} \n{item.Flight}");
                        }
                    }
                }
                else if (userType == "4")
                {
                    break;
                }
            }
        }
    }
}