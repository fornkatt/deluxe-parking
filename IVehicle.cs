namespace DeluxeParking;

internal interface IVehicle
{
    string Name { get; }
    string LicenseNumber { get; init; }
    string Color { get; init; }
    double RequiredParkingSpots { get; }
}