using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleArrivals
{
    private static readonly ImmutableArray<char> s_licenseLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    private const int LICENSE_PLATE_LENGTH = 6;
    private static int s_debugNumber;

    internal static IVehicle? GetNewVehicle()
    {
        //int randomVehicle = Random.Shared.Next(3);
        IVehicle? vehicle;
        string descriptionHeader;

        switch (s_debugNumber)
        {
            case 0:
                descriptionHeader = "En ny bil anländer till parkeringen.";
                vehicle = new Car(GetRandomLicenseNumber(), GetColor(descriptionHeader), CheckIfElectricCar(descriptionHeader));
                break;
            case 1:
                descriptionHeader = "En ny motorcykel anländer till parkeringen.";
                vehicle = new Motorcycle(GetRandomLicenseNumber(), GetColor(descriptionHeader), GetBrand(descriptionHeader));
                break;
            case 2:
                descriptionHeader = "En ny buss anländer till parkeringen.";
                vehicle = new Bus(GetRandomLicenseNumber(), GetColor(descriptionHeader), GetNumberOfPassengers(descriptionHeader));
                break;
            default:
                vehicle = null;
                break;
        }

        if (s_debugNumber == 2)
        {
            s_debugNumber = 0;
        }
        else
        {
            s_debugNumber++;
        }
        return vehicle;
    }
    // make generic vehicle creation method?
    private static int GetNumberOfPassengers(string descriptionHeader)
    {
        return TryConvertPassengerInput(descriptionHeader);
    }
    private static int TryConvertPassengerInput(string descriptionHeader)
    {
        int maxPassengersResult;

        while (true)
        {
            string maxPassengers = UserInput.GetUserInput("Hur många passagerare får plats i bussen? Minst 7 personer och max 60: ", descriptionHeader);
            bool passengerResult = int.TryParse(maxPassengers, out maxPassengersResult);
            if (!passengerResult || maxPassengersResult < 7 || maxPassengersResult > 60)
            {
                Console.WriteLine();
                Console.WriteLine("Var god mata in ett giltigt värde");
                Thread.Sleep(2000);
                Console.Clear();
            }
            else
            {
                break;
            }
        }
        return maxPassengersResult;
    }
    private static string GetColor(string descriptionHeader)
    {
        return UserInput.GetUserInput("Vad är det för färg? ", descriptionHeader);
    }
    private static string GetBrand(string descriptionHeader)
    {
        return UserInput.GetUserInput("Vad är det för märke? ", descriptionHeader);
    }
    private static bool CheckIfElectricCar(string descriptionHeader)
    {
        bool isElectric = false;
        ConsoleKeyInfo choice;
        bool chosen = false;

        while (!chosen)
        {
            Console.Clear();
            Console.WriteLine(descriptionHeader);
            Console.WriteLine();
            Console.Write("Är bilen elektrisk? Y/n ");
            choice = Console.ReadKey(true);
            Console.WriteLine();

            switch (choice.KeyChar)
            {
                case 'y':
                    isElectric = true;
                    break;
                case 'n':
                    isElectric = false;
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Var god välj Y eller N.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
            }
            chosen = true;
        }
        return isElectric;
    }
    private static string GetRandomLicenseNumber()
    {
        Span<char> licenseNumber = stackalloc char[6];
        bool lastSymbolIsLetter = Random.Shared.Next(2) == 0;

        for (int i = 0; i < LICENSE_PLATE_LENGTH; i++)
        {
            if (i == LICENSE_PLATE_LENGTH - 1 && lastSymbolIsLetter || i < 3)
            {
                licenseNumber[i] = s_licenseLetters[Random.Shared.Next(LICENSE_PLATE_LENGTH)];
            }
            else
            {
                licenseNumber[i] = (char)('0' + Random.Shared.Next(10));
            }
        }
        return new string(licenseNumber);
    }
}