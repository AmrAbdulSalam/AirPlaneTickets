using System;
using AirportTickets.Flight;
using AirportTickets.Repository;
using AirportTickets.Passenger;

namespace AirPlanceTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the file path that have a .CSV file : ");
            string? path = Console.ReadLine();
            var flightRepository = new FlightRepository(path);
            var flightInfo = flightRepository.FlightsInfo();
            List<FlightDTO> filterdList = null;
            var newUser = true;
            var passenger = new PassengerDTO();
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
                            foreach (var flight in flightInfo)
                            {
                                flight.Log();
                            }
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
                    foreach (var flight in filterdList)
                    {
                        flight.Log();
                    }
                    filterdList.Clear();
                    Console.WriteLine("Book a flight y/n?");
                    var question = Console.ReadLine();
                    if (question == "y")
                    {
                        Console.WriteLine("To book a flight please enter the id of the flight");
                        var id = Console.ReadLine();
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
                        var bookedFlight = flightRepository.SearchForFlight(id);
                        bookedFlight.ServiceClass = bookType;
                        passenger.AddFlight(bookedFlight);
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
                    Console.WriteLine("Manager");
                }
                else if (userType == "4")
                {
                    break;
                }
            }
        }
    }
}
