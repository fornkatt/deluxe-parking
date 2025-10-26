namespace DeluxeParking; 
internal class Vehicle(string licenceNumber, string color, double parkingSpots) {
    internal string LicenseNumber { get; init; } = licenceNumber;
    internal string Color { get; init; } = color;
    internal double ParkingSpots { get; } = parkingSpots;

    }
internal class Car(string licenceNumber, string color, bool isElectric) : Vehicle(licenceNumber, color, 1.0) {
    internal bool IsElectric { get; init; } = isElectric;
    }
internal class Motorcycle(string licenceNumber, string color, string brand) : Vehicle(licenceNumber, color, 0.5) {
    internal string Brand { get; init; } = brand;
    }
internal class Bus(string licenceNumber, string color, int maxPassangers) : Vehicle(licenceNumber, color, 2.0) {
    internal int MaxPassangers { get; init; } = maxPassangers;
    }
