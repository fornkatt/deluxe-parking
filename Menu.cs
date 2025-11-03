namespace DeluxeParking;

internal class Menu
{
    public static void PrintMainMenu(ParkingGarage parkingGarage, Queue<IVehicle> vehicleQueue)
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

            Console.WriteLine(Info.PrintParkedVehicles(parkingGarage.ParkingSpots));

            GetUserChoice(parkingGarage);
        }
    }
    private static void GetUserChoice(ParkingGarage parkingGarage)
    {
        ConsoleKeyInfo choice = Console.ReadKey(true);
        switch (choice.KeyChar)
        {
            case 'i':
                Console.Clear();
                parkingGarage.ParkVehicle();
                Thread.Sleep(2000);
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
                Console.WriteLine("""

                    Var god mata in ett giltigt värde.

                    """);
                Thread.Sleep(5000);
                break;
        }
    }
}