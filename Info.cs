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
                parkedVehiclesMessage.AppendLine(vehicle switch
                {
                    Car car =>
                        $"{car.Name,-15}{car.LicenseNumber,-15}" +
                        $"{car.Color,-15}" +
                        $"{(car.IsElectric ? "Elbil" : "Fossil")}",
                    Motorcycle motorcycle =>
                        $"{motorcycle.Name,-15}{motorcycle.LicenseNumber,-15}" +
                        $"{motorcycle.Color,-15}" +
                        $"{motorcycle.Brand}",
                    Bus bus =>
                        $"{bus.Name,-15}{bus.LicenseNumber,-15}" +
                        $"{bus.Color,-15}" +
                        $"{bus.MaxPassengers}",
                    _ =>
                        $"Okänt fordon\t{vehicle.LicenseNumber,-15}" +
                        $"{vehicle.Color}"
                });
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
            debugText.AppendLine($"" +
                $"{meters.LicenseNumber,-15}{meters.MinutesParked} min{meters.TotalParkingCost,15:0.0}kr");
        }
        debugText.AppendLine();
        debugText.AppendLine($"Upptagna platser: {parkingGarage.OccupiedParkingSpots}");
        Console.Write(debugText.ToString());
        Console.ReadKey(true);
    }
}
