using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleArrivals
{
    private static readonly ImmutableArray<char> s_licenseLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
    private static readonly HashSet<string> s_validColors = new(StringComparer.OrdinalIgnoreCase)
        {
            "Röd",
            "Blå",
            "Grön",
            "Gul",
            "Svart",
            "Vit",
            "Grå",
            "Silver",
            "Brun",
            "Orange",
            "Lila",
            "Rosa",
            "Beige",
            "Guld",
            "Turkos",
            "Marinblå",
            "Mörkgrön",
            "Bordeaux",
            "Bronze",
            "Cyan",
            "Magenta",
            "Olivgrön",
            "Persika",
            "Plommon",
            "Korall",
            "Mintgrön",
            "Vinröd",
            "Krom"
        };
    private static readonly HashSet<string> s_validMotorcycleBrands = new(StringComparer.OrdinalIgnoreCase)
        {
            "Harley-Davidson",
            "Harley",
            "Honda",
            "Yamaha",
            "Kawasaki",
            "Suzuki",
            "Ducati",
            "BMW",
            "KTM",
            "Triumph",
            "Indian",
            "Aprilia",
            "Husqvarna",
            "Royal Enfield",
            "Moto Guzzi",
            "MV Agusta",
            "Benelli",
            "CFMoto",
            "Zero",
            "Energica",
            "Can-Am"
        };
    
    private const int MinBusPassengerCount = 7;
    private const int MaxBusPassengerCount = 60;

    private const int MaxLicenseNumberLength = 6;
    private const int MinLicenseNumberLetters = 3;

    internal static IVehicle? GetNewVehicle()
    {
        int randomVehicle = Random.Shared.Next(3);
        IVehicle? vehicle;
        string contextHeader;

        switch (randomVehicle)
        {
            case 0:
                contextHeader = "En ny bil anländer till parkeringen.";
                vehicle = new Car(
                    GetRandomLicenseNumber(),
                    GetColor(contextHeader),
                    IsElectricCar(contextHeader)
                    );
                break;
            case 1:
                contextHeader = "En ny motorcykel anländer till parkeringen.";
                vehicle = new Motorcycle(
                    GetRandomLicenseNumber(),
                    GetColor(contextHeader),
                    GetBrand(contextHeader)
                    );
                break;
            case 2:
                contextHeader = "En ny buss anländer till parkeringen.";
                vehicle = new Bus(
                    GetRandomLicenseNumber(),
                    GetColor(contextHeader),
                    GetPassengerCount(contextHeader)
                    );
                break;
            default:
                vehicle = null;
                break;
        }
        return vehicle;
    }
    private static int GetPassengerCount(string contextHeader)
    {
        int maxPassengersResult;

        while (true)
        {
            string maxPassengers = UserInput.GetUserInput(
                "Hur många passagerare får plats i bussen? " +
                "Minst 7 personer och max 60: ",
                contextHeader
                );
            bool passengerResult = int.TryParse(maxPassengers, out maxPassengersResult);
            if (!passengerResult ||
                maxPassengersResult < MinBusPassengerCount ||
                maxPassengersResult > MaxBusPassengerCount)
            {
                Console.WriteLine();
                Console.WriteLine("Var god mata in ett giltigt värde");
                Thread.Sleep(GlobalConstants.UserFeedbackDelay);
                Console.Clear();
            }
            else
            {
                break;
            }
        }
        return maxPassengersResult;
    }
    private static string GetColor(string contextHeader)
    {
        return GetValidatedInput(contextHeader, "Vad är det för färg? ", s_validColors, "är ingen giltig färg.");
    }
    private static string GetBrand(string contextHeader)
    {
        return GetValidatedInput(contextHeader, "Vad är det för märke? ", s_validMotorcycleBrands, "är inget giltigt märke.");
    }
    private static string GetValidatedInput(string contextHeader, string prompt, HashSet<string> validValues, string errorMessageSuffix)
    {
        string input;

        while (true)
        {
            input = UserInput.GetUserInput(prompt, contextHeader).Trim();

            if (!validValues.TryGetValue(input, out string? validInput))
            {
                Console.WriteLine();
                Console.WriteLine($"\"{input}\" {errorMessageSuffix}");
                Thread.Sleep(GlobalConstants.UserFeedbackDelay);
                continue;
            }
            else
            {
                return validInput;
            }
        }
    }
    private static bool IsElectricCar(string contextHeader)
    {
        ConsoleKeyInfo choice;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(contextHeader);
            Console.WriteLine();
            Console.Write("Är bilen elektrisk? Y/n ");
            choice = Console.ReadKey(true);
            Console.WriteLine();

            switch (choice.KeyChar)
            {
                case 'y':
                    return true;
                case 'n':
                    return false;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Var god välj Y eller N.");
                    Thread.Sleep(GlobalConstants.UserFeedbackDelay);
                    Console.Clear();
                    continue;
            }
        }
    }
    private static string GetRandomLicenseNumber()
    {
        Span<char> licenseNumber = stackalloc char[MaxLicenseNumberLength];
        bool lastSymbolIsLetter = Random.Shared.Next(2) == 0;

        for (int i = 0; i < MaxLicenseNumberLength; i++)
        {
            if (i < MinLicenseNumberLetters ||
               (i == MaxLicenseNumberLength - 1 && lastSymbolIsLetter))
            {
                licenseNumber[i] = s_licenseLetters[Random.Shared.Next(s_licenseLetters.Length)];
            }
            else
            {
                licenseNumber[i] = (char)('0' + Random.Shared.Next(10));
            }
        }
        return new string(licenseNumber);
    }
}