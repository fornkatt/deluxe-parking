namespace DeluxeParking; 
internal class ParkingGarage(double maxParkingSpots, List<Vehicle> parkedVehicles) {
    internal double MaxParkingSpots { get; init; } = maxParkingSpots;
    internal List<Vehicle> ParkedVehicles { get; set; } = parkedVehicles;
}
