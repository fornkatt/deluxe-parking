namespace DeluxeParking;
internal class Car(string licenseNumber, string color, bool isElectric) : IVehicle {
    public string LicenseNumber { get; init; } = licenseNumber;
    public string Color { get; init; } = color;
    public double RequiredParkingSpots { get; } = 1.0;
    public bool IsElectric { get; init; } = isElectric;
}
internal class Motorcycle(string licenseNumber, string color, string brand) : IVehicle {
    public string LicenseNumber { get; init; } = licenseNumber;
    public string Color { get; init; } = color;
    public double RequiredParkingSpots { get; } = 0.5;
    public string Brand { get; init; } = brand;
}
internal class Bus(string licenseNumber, string color, int maxPassangers) : IVehicle {
    public string LicenseNumber { get; init; } = licenseNumber;
    public string Color { get; init; } = color;
    public double RequiredParkingSpots { get; } = 2.0;
    public int MaxPassangers { get; init; } = maxPassangers;
}
