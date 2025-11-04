namespace DeluxeParking;

internal class ParkingGarage(int maxParkingSpots) : IParkingGarage
{
    public int MaxParkingSpots { get; init; } = maxParkingSpots;
    public double OccupiedParkingSpots { get; set; }
    public List<IParkingMeter> ParkingMeters { get; set; } = [];
    public List<IVehicle> ParkedVehicles { get; set; } = [];
    public SortedDictionary<int, List<IVehicle>> ParkingSpots { get; set; } = ParkingGarageHelpers.InitializeParkingSpots(maxParkingSpots);

    public void ParkVehicle()
    {
        IVehicle? newArrival = VehicleArrivals.GetNewVehicle();

        if (newArrival == null || (OccupiedParkingSpots + newArrival.RequiredParkingSpots > MaxParkingSpots))
        {
            Console.WriteLine();
            Console.WriteLine("Parkeringshuset kan inte ta emot detta fordon just nu.");
            return;
        }

        int spotNumber = FindAvailableSpot(newArrival.RequiredParkingSpots);
        if (spotNumber == -1)
        {
            Console.WriteLine();
            Console.WriteLine("Gick inte att hitta lämplig plats.");
            return;
        }

        AssignVehicleToParking(newArrival, spotNumber);

        Console.WriteLine();
        Console.WriteLine("Nytt fordon parkerat.");
    }
    public void CheckoutVehicle()
    {
        string licenseNumber = UserInput.GetUserInput("Ange registreringsnumret på fordonet du vill checka ut: ", Info.PrintParkedVehicles(ParkingSpots)).ToUpper();

        IVehicle? vehicleToCheckout = GetCheckoutObject(licenseNumber, ParkedVehicles);
        IParkingMeter? parkingMeterToCheckout = GetCheckoutObject(licenseNumber, ParkingMeters);

        if (vehicleToCheckout == null || parkingMeterToCheckout == null)
        {
            Console.WriteLine();
            Console.WriteLine("Fordonet hittades inte");
            Thread.Sleep(GlobalConstants.UserFeedbackDelay);
            return;
        }
        parkingMeterToCheckout.CalculateTotalCost();
        RemoveVehicleFromParking(parkingMeterToCheckout, vehicleToCheckout);
        Info.PrintCheckoutMessage(parkingMeterToCheckout, vehicleToCheckout);
    }
    private int FindAvailableSpot(double requiredSpots)
    {
        if (requiredSpots == GlobalConstants.RequiredMotorcycleParkingSpots)
        {
            foreach (var spot in ParkingSpots)
            {
                double currentOccupancy = 0.0;
                foreach (var vehicle in spot.Value)
                {
                    currentOccupancy += vehicle.RequiredParkingSpots;
                }
                if (currentOccupancy + requiredSpots <= 1.0)
                {
                    return spot.Key;
                }
            }
        }
        else if (requiredSpots == GlobalConstants.RequiredCarParkingSpots)
        {
            foreach (var spot in ParkingSpots)
            {
                if (spot.Value.Count == 0)
                {
                    return spot.Key;
                }
            }
        }
        else if (requiredSpots == GlobalConstants.RequiredBusParkingSpots)
        {
            for (int i = 1; i <= MaxParkingSpots - 1; i++)
            {
                if (ParkingSpots[i].Count == 0 && ParkingSpots[i + 1].Count == 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    private void AssignVehicleToParking(IVehicle newArrival, int spotNumber)
    {
        AssignVehicleToSpot(newArrival, spotNumber);
        ParkedVehicles.Add(newArrival);
        ParkingMeters.Add(new ParkingMeter(newArrival, newArrival.LicenseNumber));
        OccupiedParkingSpots += newArrival.RequiredParkingSpots;
    }
    private void AssignVehicleToSpot(IVehicle newArrival, int spotNumber)
    {
        if (newArrival.RequiredParkingSpots == GlobalConstants.RequiredBusParkingSpots)
        {
            ParkingSpots[spotNumber].Add(newArrival);
            ParkingSpots[spotNumber + 1].Add(newArrival);
        }
        else
        {
            ParkingSpots[spotNumber].Add(newArrival);
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1822:Mark members as static",
        Justification = "This method access instance data for specific parking garages passed through CheckoutVehicle()")]
    private T? GetCheckoutObject<T>(string licenseNumber, List<T> objectList) where T : class
    {
        foreach (var item in objectList)
        {
            string? itemLicenseNumber = item switch
            {
                IVehicle vehicle => vehicle.LicenseNumber,
                IParkingMeter meter => meter.LicenseNumber,
                _ => null
            };
            if (itemLicenseNumber == licenseNumber)
            {
                return item;
            }
        }
        return null;
    }
    private void RemoveVehicleFromParking(IParkingMeter parkingMeterToCheckout, IVehicle vehicleToCheckout)
    {
        RemoveVehicleFromSpot(vehicleToCheckout);
        ParkedVehicles.Remove(vehicleToCheckout);
        ParkingMeters.Remove(parkingMeterToCheckout);
        OccupiedParkingSpots -= vehicleToCheckout.RequiredParkingSpots;
    }
    private void RemoveVehicleFromSpot(IVehicle vehicleToCheckout)
    {
        foreach (var spots in ParkingSpots)
        {
            spots.Value.Remove(vehicleToCheckout);
        }
    }
}
