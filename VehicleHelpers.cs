using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleHelpers {
    //private static readonly ImmutableArray<string> _vehicleBrand =
    //    ["Honda", "Yamaha", "Harley-Davidson", "Kawasaki",
    //     "Suzuki", "BMW", "Royal Enfield", "Benelli"];

    //private static readonly ImmutableArray<string> _vehicleColors =
    //    ["Vit", "Svart", "Grå", "Silver", "Röd", "Blå", "Brun", "Grön",
    //     "Beige", "Orange", "Guld", "Gul", "Lila", "Marinblå", "Vinröd"];

    private static readonly ImmutableArray<char> _licenseLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    private static readonly int _licenseLettersLength = _licenseLetters.Length;
    private const int _LICENSE_PLATE_LENGTH = 6;

    internal static IVehicle GenerateNewVehicle() {
        int randomVehicle = Random.Shared.Next(3);
        IVehicle vehicle;

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
        string descriptionHeader = "En ny bil anländer till parkeringen.";

        string color = GetColor(descriptionHeader);
        Console.Clear();
        bool isElectric = CheckIfElectricCar(descriptionHeader);

        return new Car(GenerateRandomLicenseNumber(), color, isElectric);
    }
    private static Motorcycle GenerateMotorcycle() {
        string descriptionHeader = "En ny motorcykel anländer till parkeringen.";

        string color = GetColor(descriptionHeader);
        Console.Clear();
        string brand = GetBrand(descriptionHeader);

        return new Motorcycle(GenerateRandomLicenseNumber(), color, brand);
    }
    private static Bus GenerateBus() {
        string descriptionHeader = "En ny buss anländer till parkeringen.";

        string color = GetColor(descriptionHeader);
        Console.Clear();
        int maxPassengers = GetNumberOfPassengers(descriptionHeader);

        return new Bus(GenerateRandomLicenseNumber(), color, maxPassengers);
    }
    private static int GetNumberOfPassengers(string descriptionHeader) {
        return TryConvertPassengerInput(descriptionHeader);
    }
    private static int TryConvertPassengerInput(string descriptionHeader) {
        string maxPassengers;
        int maxPassengersResult;

        while (true) {
            maxPassengers = GetUserInput("Hur många passagerare får plats i bussen? Minst 7 personer och max 60: ", descriptionHeader);
            bool passengerResult = int.TryParse(maxPassengers, out maxPassengersResult);
            if (!passengerResult || maxPassengersResult < 7 || maxPassengersResult > 60) {
                Console.WriteLine("\nVar god mata in ett giltigt värde");
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
    private static string GetColor(string descriptionHeader) {
        return GetUserInput("Vad är det för färg? ", descriptionHeader);
    }
    private static string GetBrand(string descriptionHeader) {
        return GetUserInput("Vad är det för märke? ", descriptionHeader);
    }
    private static bool CheckIfElectricCar(string descriptionHeader) {
        bool isElectric = false;
        ConsoleKeyInfo choice;

        bool chosen = false;
        while (!chosen) {
            Console.WriteLine(descriptionHeader);
            Console.Write("\nÄr bilen elektrisk? Y/n ");
            choice = Console.ReadKey(true);
            Console.WriteLine();

            switch (choice.KeyChar) {
                case 'y':
                    isElectric = true;
                    chosen = true;
                    break;
                case 'n':
                    isElectric = false;
                    chosen = true;
                    break;
                default:
                    Console.WriteLine("\nVar god välj Y eller N.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
            }
        }
        return isElectric;
    }
    private static string GetUserInput(string prompt, string descriptionHeader) {
        string? input;

        while (true) {
            Console.WriteLine($"{descriptionHeader}\n");
            Console.Write(prompt);
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
    private static string GenerateRandomLicenseNumber() {
        Span<char> licenseNumber = stackalloc char[6];
        bool lastSymbolIsLetter = Random.Shared.Next(2) == 0;

        for (int i = 0; i < _LICENSE_PLATE_LENGTH; i++) {
            if (i == _LICENSE_PLATE_LENGTH - 1 && lastSymbolIsLetter || i < 3) {
                licenseNumber[i] = _licenseLetters[Random.Shared.Next(_LICENSE_PLATE_LENGTH)];
            }
            else {
                licenseNumber[i] = (char)('0' + Random.Shared.Next(10));
            }
        }
        return new string(licenseNumber);
    }
    //private static string GetRandomBrand() {
    //    return _vehicleBrand[Random.Shared.Next(_vehicleBrand.Length)];
    //}
    //private static string GetRandomColor() {
    //    return _vehicleColors[Random.Shared.Next(_vehicleColors.Length)];
    //}
}