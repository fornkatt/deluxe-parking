using System.Diagnostics.Metrics;

namespace DeluxeParking;

internal class ParkingGarage(int maxParkingSpots) : IParkingGarage {
    public int MaxParkingSpots { get; init; } = maxParkingSpots;
    public double OccupiedParkingSpots { get; set; } = 0.0;
    public List<IParkingMeter> ParkingMeters { get; set; } = [];
    public List<IVehicle> ParkedVehicles { get; set; } = [];
    public SortedDictionary<int, List<IVehicle>> ParkingSpots { get; set; } = ParkingGarageHelpers.InitializeParkingSpots(maxParkingSpots);

    public void ParkVehicle(IVehicle newArrival) {
        if (OccupiedParkingSpots + newArrival.RequiredParkingSpots > MaxParkingSpots) {
            Console.WriteLine("\nParkeringshuset är fullt.");
            return;
        }

        int spotNumber = FindAvailableSpot(newArrival.RequiredParkingSpots);
        if (spotNumber == -1) {
            Console.WriteLine("\nGick inte att hitta lämplig plats.");
            return;
        }

        AssignVehicleToSpot(newArrival, spotNumber);
        ParkedVehicles.Add(newArrival);
        OccupiedParkingSpots += newArrival.RequiredParkingSpots;
        ParkingMeters.Add(new ParkingMeter(newArrival, newArrival.LicenseNumber));
        Console.WriteLine("\nNytt fordon parkerat.");
    }
    public void CheckoutVehicle() {
        Info.PrintParkedVehicles(ParkingSpots);
        Console.WriteLine();
        Console.Write("Ange registreringsnumret på fordonet du vill checka ut: ");
        string? licenseNumber = Console.ReadLine()?.ToUpper();
        IVehicle? vehicleToCheckout = null;
        IParkingMeter? parkingMeterToCheckout = null;

        if (string.IsNullOrWhiteSpace(licenseNumber) || licenseNumber.Length > 6) {
            Console.WriteLine();
            Console.WriteLine("Ogiltigt registreringsnummer.");
            Thread.Sleep(2000);
            return;
        }

        foreach (var vehicle in ParkedVehicles) {
            if (vehicle.LicenseNumber == licenseNumber) {
                vehicleToCheckout = vehicle;
            }
        }
        if (vehicleToCheckout == null) {
            Console.WriteLine();
            Console.WriteLine("Inget fordon med det registreringsnumret hittades.");
            Thread.Sleep(2000);
            return;
        }
        foreach (var meter in ParkingMeters) {
            if (meter.LicenseNumber == licenseNumber) {
                parkingMeterToCheckout = meter;
            }
        }
        if (parkingMeterToCheckout == null) {
            Console.WriteLine();
            Console.WriteLine("Ingen parkeringsmätaren till det fordonet hittades.");
            Thread.Sleep(2000);
            return;
        }


        parkingMeterToCheckout.CalculateCost();

        RemoveVehicleFromSpot(vehicleToCheckout);
        ParkedVehicles.Remove(vehicleToCheckout);
        ParkingMeters.Remove(parkingMeterToCheckout);
        OccupiedParkingSpots -= vehicleToCheckout.RequiredParkingSpots;

        Console.Clear();
        Console.WriteLine("Tack för att du valde Deluxe Parking!\n");
        Console.WriteLine($"Fordonet som checkades ut:\t{vehicleToCheckout.LicenseNumber}");
        Console.WriteLine($"Total kostnad:\t{parkingMeterToCheckout.TotalParkingCost}kr");
        Console.WriteLine();
        Console.WriteLine("Tryck på valfri tangent för att fortsätta.");
        Console.ReadKey(true);
    }
    private int FindAvailableSpot(double requiredSpots) {
        if (requiredSpots == 0.5) {
            foreach (var spot in ParkingSpots) {
                if (spot.Value.Count == 0) {
                    return spot.Key;
                }
                if (spot.Value.Count == 1 && spot.Value[0].RequiredParkingSpots == 0.5) {
                    return spot.Key;
                }
            }
        }
        else if (requiredSpots == 1.0) {
            foreach (var spot in ParkingSpots) {
                if (spot.Value.Count == 0) {
                    return spot.Key;
                }
            }
        }
        else if (requiredSpots == 2.0) {
            for (int i = 1; i <= MaxParkingSpots - 1; i++) {
                if (ParkingSpots[i].Count == 0 && ParkingSpots[i + 1].Count == 0) {
                    return i;
                }
            }
        }
        return -1;
    }
    private void AssignVehicleToSpot(IVehicle vehicle, int spotNumber) {
        if (vehicle.RequiredParkingSpots == 2.0) {
            ParkingSpots[spotNumber].Add(vehicle);
            ParkingSpots[spotNumber + 1].Add(vehicle);
        }
        else {
            ParkingSpots[spotNumber].Add(vehicle);
        }
    }
    private void RemoveVehicleFromSpot(IVehicle vehicle) {
        foreach (var spots in ParkingSpots) {
            if (spots.Value.Contains(vehicle)) {
                spots.Value.Remove(vehicle);
            }
        }
    }
}
