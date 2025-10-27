namespace DeluxeParking;
internal class ParkingGarage(double maxParkingSpots, double occupiedParkingSpots, List<IVehicle> parkedVehicles) : IParkingGarage {
    public double MaxParkingSpots { get; init; } = maxParkingSpots;
    public double OccupiedParkingSpots { get; set; } = occupiedParkingSpots;
    public List<IVehicle> ParkedVehicles { get; set; } = parkedVehicles;

    public IVehicle ParkVehicle() {
        int randomVehicle = Random.Shared.Next(3);
        IVehicle vehicle;

        if (randomVehicle == 0) {
            vehicle = VehicleHelpers.GenerateCar();
        }
        else if (randomVehicle == 1) {
            vehicle = VehicleHelpers.GenerateMotorcycle();
        }
        else {
            vehicle = VehicleHelpers.GenerateBus();
        }
        return vehicle;
    }
}
