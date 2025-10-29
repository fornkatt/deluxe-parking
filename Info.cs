namespace DeluxeParking;

internal class Info {
    internal static void PrintHeader() {
        Console.WriteLine("""
                Välkommen till Deluxe Parkings parkeringssystem.
                
                    Som värdefull medarbetare hos oss uppmanar vi Er att vara extra aktsam och noggrant kontrollera så att inmatade uppgifter stämmer.
                    Felinmatade uppgifter eller övrig misskötsel såsom överanvändning av raster och toalettbesök dras från nästkommande lön.

                    Vi hoppas Ni kommer trivas bra hos oss och ser fram emot våran tid tillsammans.
                    Och glöm inte; Vi på Deluxe Parking sätter "Deluxe" i "Familj".
                """);
    }
    internal static void PrintParkedVehicles(SortedDictionary<int, List<IVehicle>> parkingSpots) {
        HashSet<string> printedVehicles = [];

        foreach (var spot in parkingSpots) {
            if (spot.Value.Count == 0) {
                continue;
            }
            foreach (var vehicle in spot.Value) {
                if (printedVehicles.Contains(vehicle.LicenseNumber)) {
                    continue;
                }

                if (vehicle is Car car) {
                    Console.WriteLine($"Plats {spot.Key}\t\t\tBil\t\t{car.LicenseNumber}\t\t{car.Color}\t\t{(car.IsElectric ? "Elbil" : "Fossil")}");
                }
                else if (vehicle is Motorcycle motorcycle) {
                    Console.WriteLine($"Plats {spot.Key}\t\t\tMC\t\t{motorcycle.LicenseNumber}\t\t{motorcycle.Color}\t\t{motorcycle.Brand}");
                }
                else if (vehicle is Bus bus) {
                    Console.WriteLine($"Plats {spot.Key}-{spot.Key + 1}\t\tBuss\t\t{bus.LicenseNumber}\t\t{bus.Color}\t\t{bus.MaxPassangers}");
                }
                printedVehicles.Add(vehicle.LicenseNumber);
            }
        }
    }
    internal static void PrintDebugMenu(IParkingGarage parkingGarage) {
        foreach (var meters in parkingGarage.ParkingMeters) {
            meters.CalculateCost();
            Console.WriteLine($"{meters.LicenseNumber}: {meters.MinutesParked} minuter, {meters.TotalParkingCost}kr");
        }
        Console.WriteLine();
        Console.WriteLine($"Upptagna platser: {parkingGarage.OccupiedParkingSpots}");
        Console.ReadKey(true);
    }
}
