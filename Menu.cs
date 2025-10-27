namespace DeluxeParking;
internal class Menu {
    private const int _MISCONDUCT_ALLOWANCE = 5;
    private static int _misconductCount = 0;
    public static void ShowMenu(ParkingGarage parkingGarage, Queue<IVehicle> vehicleQueue) {
        ConsoleKeyInfo choice;
        List<IVehicle> test = [];

        test.Add(new Car("URL38B", "Vit", false));
        test.Add(new Motorcycle("YMV433", "Grön", "Yamaha"));
        test.Add(new Bus("JGP76R", "Blå", 44));
        test.Insert(2, new Motorcycle("KLG995", "Röd", "Benelli"));

        bool running = true;
        while (running) {
            Console.Clear();
            Info.PrintHeader();
            Console.WriteLine();

            Console.WriteLine("[I]nvänta nytt fordon.");
            Console.WriteLine("[C]hecka ut fordon.");
            Console.WriteLine("[A]vsluta.");
            Console.WriteLine();
            foreach (IVehicle vehicle in parkingGarage.ParkedVehicles) {
                if (vehicle is Car car) {
                    Console.WriteLine($"Bil: Elektrisk: {car.IsElectric}, {car.Color}, {car.LicenseNumber}");
                }
                else if (vehicle is Motorcycle motorcycle) {
                    Console.WriteLine($"Motorcykel: Märke: {motorcycle.Brand}, {motorcycle.Color}, {motorcycle.LicenseNumber}");
                }
                else if (vehicle is Bus bus) {
                    Console.WriteLine($"Buss: Passagerare: {bus.MaxPassangers}, {bus.Color}, {bus.LicenseNumber}");
                }
            }

            choice = Console.ReadKey(true);
            switch (choice.KeyChar) {
                case 'i':
                    Console.Clear();
                    parkingGarage.ParkedVehicles.Add(parkingGarage.ParkVehicle());
                    Console.WriteLine();
                    Console.WriteLine("\nNytt fordon parkerat.");
                    Thread.Sleep(2000);
                    break;
                case 'c':
                    Console.Clear();
                    break;
                case 'a':
                    Environment.Exit(0);
                    break;
                case 'd':
                    foreach (IVehicle vehicle in test) {
                        if (vehicle is Car car) {
                            Console.WriteLine($"{car} Bil: Elektrisk: {car.IsElectric}, {car.Color}, {car.LicenseNumber}");
                        }
                        else if (vehicle is Motorcycle motorcycle) {
                            Console.WriteLine($"{motorcycle} Motorcykel: Märke: {motorcycle.Brand}, {motorcycle.Color}, {motorcycle.LicenseNumber}");
                        }
                        else if (vehicle is Bus bus) {
                            Console.WriteLine($"{bus} Buss: Passagerare: {bus.MaxPassangers}, {bus.Color}, {bus.LicenseNumber}");
                        }
                    }
                    Console.WriteLine();
                    foreach (IVehicle vehicle in test) {
                        Console.WriteLine($"{vehicle.GetType()}");
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;
                default:
                    _misconductCount++;
                    Console.WriteLine();
                    Console.WriteLine("Var god mata in ett giltigt värde. Upprepade tjänstefel leder till omedelbar disciplin.");
                    Console.WriteLine();
                    Thread.Sleep(5000);
                    Console.WriteLine($"Du har nu använt {_misconductCount} av {_MISCONDUCT_ALLOWANCE} poäng.");
                    Thread.Sleep(5000);
                    continue;
            }
        }
    }
}
