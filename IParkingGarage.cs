namespace DeluxeParking;

internal interface IParkingGarage
{
    int MaxParkingSpots { get; init; }
    double OccupiedParkingSpots { get; set; }
    List<IParkingMeter> ParkingMeters { get; set; }
    List<IVehicle> ParkedVehicles { get; set; }
    SortedDictionary<int, List<IVehicle>> ParkingSpots { get; set; }

    void ParkVehicle();
    void CheckoutVehicle();
}
