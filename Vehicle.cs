namespace DeluxeParking;

internal abstract class Vehicle(string licenseNumber, string color) : IVehicle
{
    public string LicenseNumber { get; init; } = licenseNumber;
    public string Color { get; init; } = color;
    public abstract double RequiredParkingSpots { get; }
}

internal class Car(string licenseNumber, string color, bool isElectric) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = 1.0;
    public bool IsElectric { get; init; } = isElectric;
}
internal class Motorcycle(string licenseNumber, string color, string brand) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = 0.5;
    public string Brand { get; init; } = brand;
}
internal class Bus(string licenseNumber, string color, int maxPassengers) : Vehicle(licenseNumber, color)
{
    public override double RequiredParkingSpots { get; } = 2.0;
    public int MaxPassengers { get; init; } = maxPassengers;
}
