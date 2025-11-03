using System.Text;

namespace DeluxeParking;

internal class Info
{
    internal static string PrintParkedVehicles(SortedDictionary<int, List<IVehicle>> parkingSpots)
    {
        HashSet<string> printedVehicles = [];
        StringBuilder parkedVehiclesMessage = new();

        parkedVehiclesMessage.AppendLine("Parkerade fordon:");
        parkedVehiclesMessage.AppendLine();

        foreach (var spot in parkingSpots)
        {
            if (spot.Value.Count == 0)
            {
                continue;
            }
            foreach (var vehicle in spot.Value)
            {
                if (!printedVehicles.Add(vehicle.LicenseNumber))
                {
                    continue;
                }

                string spotLabel = vehicle is Bus ? $"{spot.Key}-{spot.Key + 1}" : spot.Key.ToString();

                parkedVehiclesMessage.Append($"Plats {spotLabel,-15}");
                parkedVehiclesMessage.Append(vehicle switch
                {
                    Car car => $"Bil\t{car.LicenseNumber,-15}{car.Color,-15}{(car.IsElectric ? "Elbil" : "Fossil")}",
                    Motorcycle motorcycle => $"MC\t\t{motorcycle.LicenseNumber,-15}{motorcycle.Color,-15}{motorcycle.Brand}",
                    Bus bus => $"Buss\t{bus.LicenseNumber,-15}{bus.Color,-15}{bus.MaxPassengers}",
                    _ => $"Okänt fordon\t{vehicle.LicenseNumber,-15}{vehicle.Color}"
                });
                parkedVehiclesMessage.AppendLine();
            }
        }
        return parkedVehiclesMessage.ToString();
    }
    internal static void PrintCheckoutMessage(IParkingMeter parkingMeterToCheckout, IVehicle vehicleToCheckout)
    {
        Console.Clear();
        Console.WriteLine("Tack för att du valde Deluxe Parking!");
        Console.WriteLine();
        Console.WriteLine($"Fordonet som checkades ut:\t{vehicleToCheckout.LicenseNumber}");
        Console.WriteLine($"Total kostnad:\t\t\t{parkingMeterToCheckout.TotalParkingCost}kr");
        Console.WriteLine();
        Console.Write("Tryck på valfri tangent för att fortsätta.");
        Console.ReadKey(true);
    }
    internal static void PrintDebugMenu(IParkingGarage parkingGarage)
    {
        StringBuilder debugText = new();

        foreach (var meters in parkingGarage.ParkingMeters)
        {
            meters.CalculateTotalCost();
            debugText.AppendLine($"{meters.LicenseNumber}: {meters.MinutesParked} minuter, {meters.TotalParkingCost}kr");
        }
        debugText.AppendLine();
        debugText.AppendLine($"Upptagna platser: {parkingGarage.OccupiedParkingSpots}");
        Console.WriteLine(debugText.ToString());
        Console.ReadKey(true);
    }
}
