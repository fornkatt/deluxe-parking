using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleHelpers {
    private static readonly ImmutableArray<string> _vehicleBrand =
        ["Honda", "Yamaha", "Harley-Davidson", "Kawasaki",
         "Suzuki", "BMW", "Royal Enfield", "Benelli"];

    private static readonly ImmutableArray<char> _licenseLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    private static readonly ImmutableArray<string> _vehicleColors =
        ["Vit", "Svart", "Grå", "Silver", "Röd", "Blå", "Brun", "Grön",
         "Beige", "Orange", "Guld", "Gul", "Lila", "Marinblå", "Vinröd"];

    //private static string GetRandomBrand() {
    //    return _vehicleBrand[Random.Shared.Next(_vehicleBrand.Length)];
    //}
    //private static string GetRandomColor() {
    //    return _vehicleColors[Random.Shared.Next(_vehicleColors.Length)];
    //}
    internal static Vehicle GenerateNewVehicle() {
        int randomVehicle = Random.Shared.Next(3);
        Vehicle vehicle;

        if (randomVehicle == 0) {
            vehicle = GenerateCar();
        }
        else if (randomVehicle == 1) {
            vehicle = GenerateMotorcycle();
        }
        else {
            vehicle = GenerateBus();
        }

        return vehicle;
    }
    private static Car GenerateCar() {
        string color;
        bool isElectric;
        string header = "En ny bil anländer till parkeringen.\n";

        color = GetColor(header);
        Console.Clear();
        isElectric = CheckIfElectricCar(header);

        var car = new Car(GenerateRandomLicenseNumber(), color, isElectric);

        return car;
    }
    private static Motorcycle GenerateMotorcycle() {
        string color;
        string brand;
        string header = "En ny motorcykel anländer till parkeringen.\n";

        color = GetColor(header);
        Console.Clear();
        brand = GetBrand(header);

        var motorcycle = new Motorcycle(GenerateRandomLicenseNumber(), color, brand);

        return motorcycle;
    }
    private static Bus GenerateBus() {
        string color;
        int maxPassengers;
        string header = "En ny buss anländer till parkeringen.\n";

        color = GetColor(header);
        Console.Clear();
        maxPassengers = TryConvertPassengerInput(header);

        var bus = new Bus(GenerateRandomLicenseNumber(), color, maxPassengers);

        return bus;
    }
    private static int TryConvertPassengerInput(string header) {
        string maxPassengers;
        int maxPassengersResult;

        while (true) {
            maxPassengers = GetNumberOfPassengers(header);
            bool passengerResult = int.TryParse(maxPassengers, out maxPassengersResult);
            if (!passengerResult || maxPassengersResult < 10) {
                Console.WriteLine("Var god mata in ett giltigt värde");
                Thread.Sleep(2000);
                Console.Clear();
                continue;
            }
            else {
                break;
            }
        }
        return maxPassengersResult;
    }
    private static string GetUserInput(string prompt, string header) {
        string? input;

        while (true) {
            Console.WriteLine(header);
            Console.WriteLine(prompt);
            input = Console.ReadLine();
            if (input == null || input.Length <= 0) {
                Console.WriteLine("Var god mata in ett värde.");
                Thread.Sleep(2000);
                Console.Clear();
                continue;
            }
            else {
                break;
            }
        }
        return input;
    }
    private static string GetNumberOfPassengers(string header) {
        return GetUserInput("Hur många passagerare får plats i bussen? Minst 10 personer. ", header);
    }
    private static string GetColor(string header) {
        return GetUserInput("Vad är det för färg? ", header);
    }
    private static string GetBrand(string header) {
        return GetUserInput("Vad är det för märke? ", header);
    }
    private static bool CheckIfElectricCar(string header) {
        bool isElectric = false;
        bool running = true;
        ConsoleKeyInfo choice;

        while (running) {
            Console.WriteLine(header);
            Console.WriteLine("Är bilen elektrisk? Y/n ");
            choice = Console.ReadKey(true);

            switch (choice.KeyChar) {
                case 'y':
                    isElectric = true;
                    running = false;
                    break;
                case 'n':
                    isElectric = false;
                    running = false;
                    break;
                default:
                    Console.WriteLine("Var god välj Y eller N.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
            }
        }

        return isElectric;
    }
    private static string GenerateRandomLicenseNumber() {
        Span<char> licenseNumber = stackalloc char[6];
        bool lastSymbolIsLetter = Random.Shared.Next(2) == 0;

        for (int i = 0; i < 6; i++) {
            if (i == 5 && lastSymbolIsLetter || i < 3) {
                licenseNumber[i] = _licenseLetters[Random.Shared.Next(_licenseLetters.Length)];
            }
            else {
                licenseNumber[i] = (char)('0' + Random.Shared.Next(10));
            }
        }
        return new string(licenseNumber);
    }
}