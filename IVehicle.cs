namespace DeluxeParking;

internal interface IVehicle
{
    string LicenseNumber { get; init; }
    string Color { get; init; }
    double RequiredParkingSpots { get; }
}