namespace DeluxeParking;

internal class Menu
{
    public static void PrintMainMenu(ParkingGarage parkingGarage)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("""
                Deluxe Parking

                    [I]nvänta nytt fordon
                    [C]hecka ut fordon
                    [A]vsluta

                """);

            Console.Write(Info.PrintParkedVehicles(parkingGarage.ParkingSpots));

            GetUserMenuChoice(parkingGarage);
        }
    }
    private static void GetUserMenuChoice(ParkingGarage parkingGarage)
    {
        ConsoleKeyInfo choice = Console.ReadKey(true);
        switch (choice.KeyChar)
        {
            case 'i':
                Console.Clear();
                parkingGarage.ParkVehicle();
                Thread.Sleep(GlobalConstants.UserFeedbackDelay);
                break;
            case 'c':
                Console.Clear();
                parkingGarage.CheckoutVehicle();
                break;
            case 'a':
                Environment.Exit(0);
                break;
            case 'd':
                Console.Clear();
                Info.PrintDebugMenu(parkingGarage);
                break;
            default:
                Console.WriteLine();
                Console.WriteLine("Var god mata in ett giltigt värde.");
                Thread.Sleep(GlobalConstants.UserFeedbackDelay);
                break;
        }
    }
}