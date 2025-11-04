using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleArrivals
{
    private static readonly ImmutableArray<char> s_licenseLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    private const int MinBusPassengerCount = 7;
    private const int MaxBusPassengerCount = 60;

    private const int MaxLicenseNumberLength = 6;
    private const int MinLicenseNumberLetters = 3;

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
                vehicle = new Car(GetRandomLicenseNumber(), GetColor(descriptionHeader), IsElectricCar(descriptionHeader));
                break;
            case 1:
                descriptionHeader = "En ny motorcykel anländer till parkeringen.";
                vehicle = new Motorcycle(GetRandomLicenseNumber(), GetColor(descriptionHeader), GetBrand(descriptionHeader));
                break;
            case 2:
                descriptionHeader = "En ny buss anländer till parkeringen.";
                vehicle = new Bus(GetRandomLicenseNumber(), GetColor(descriptionHeader), GetPassengerCount(descriptionHeader));
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
    private static int GetPassengerCount(string descriptionHeader)
    {
        int maxPassengersResult;

        while (true)
        {
            string maxPassengers = UserInput.GetUserInput("Hur många passagerare får plats i bussen? Minst 7 personer och max 60: ", descriptionHeader);
            bool passengerResult = int.TryParse(maxPassengers, out maxPassengersResult);
            if (!passengerResult || maxPassengersResult < MinBusPassengerCount || maxPassengersResult > MaxBusPassengerCount)
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
    private static string GetColor(string descriptionHeader)
    {
        return UserInput.GetUserInput("Vad är det för färg? ", descriptionHeader);
    }
    private static string GetBrand(string descriptionHeader)
    {
        return UserInput.GetUserInput("Vad är det för märke? ", descriptionHeader);
    }
    private static bool IsElectricCar(string descriptionHeader)
    {
        ConsoleKeyInfo choice;

        while (true)
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
            if (i < MinLicenseNumberLetters || (i == MaxLicenseNumberLength - 1 && lastSymbolIsLetter))
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