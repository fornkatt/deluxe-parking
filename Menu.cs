namespace DeluxeParking;

internal class Menu {
    private const int _MISCONDUCT_ALLOWANCE = 5;
    private static int _misconductCount = 0;
    public static void PrintMainMenu(ParkingGarage parkingGarage, Queue<IVehicle> vehicleQueue) {
        while (true) {
            Console.Clear();

            Console.WriteLine("""
                Deluxe Parking

                    [I]nvänta nytt fordon
                    [C]hecka ut fordon
                    [A]vsluta

                """);
            Info.PrintParkedVehicles(parkingGarage.ParkingSpots);

            GetUserChoice(parkingGarage);
        }
    }
    private static void GetUserChoice(ParkingGarage parkingGarage) {
        ConsoleKeyInfo choice = Console.ReadKey(true);
        switch (choice.KeyChar) {
            case 'i':
                Console.Clear();
                IVehicle newArrival = VehicleHelpers.GenerateNewVehicle();
                parkingGarage.ParkVehicle(newArrival);
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
                _misconductCount++;
                Console.WriteLine("""
                    Var god mata in ett giltigt värde. Upprepade tjänstefel leder till omedelbar disciplin.

                    """);
                Thread.Sleep(5000);
                Console.WriteLine($"Du har nu använt {_misconductCount} av {_MISCONDUCT_ALLOWANCE} poäng.");
                Thread.Sleep(5000);
                break;
        }
    }
}