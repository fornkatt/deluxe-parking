namespace DeluxeParking;

internal interface IParkingGarage {
    double MaxParkingSpots { get; init; }
    double OccupiedParkingSpots { get; set; }
    List<IVehicle> ParkedVehicles { get; set; }

    IVehicle ParkVehicle();
}
