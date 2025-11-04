namespace DeluxeParking;

internal abstract class Vehicle(string licenseNumber, string color) : IVehicle
{
    public string LicenseNumber { get; init; } = licenseNumber;
    public string Color { get; init; } = color;
    public abstract double RequiredParkingSpots { get; }
}

internal class Car(string licenseNumber, string color, bool isElectric) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = GlobalConstants.RequiredCarParkingSpots;
    public bool IsElectric { get; init; } = isElectric;
}
internal class Motorcycle(string licenseNumber, string color, string brand) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = GlobalConstants.RequiredMotorcycleParkingSpots;
    public string Brand { get; init; } = brand;
}
internal class Bus(string licenseNumber, string color, int maxPassengers) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = GlobalConstants.RequiredBusParkingSpots;
    public int MaxPassengers { get; init; } = maxPassengers;
}
