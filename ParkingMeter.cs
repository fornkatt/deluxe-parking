namespace DeluxeParking;

internal class ParkingMeter(IVehicle vehicle, string licenseNumber) : IParkingMeter {
    public double CostPerMinute { get; init; } = 1.5;
    public int MinutesParked { get; set; } = 0;
    public double TotalParkingCost { get; set; } = 0.0;
    public string LicenseNumber { get; init; } = licenseNumber;
    public IVehicle Vehicle { get; init; } = vehicle;
    public DateTime ArrivalTime { get; init; } = DateTime.Now;

    public void CalculateCost() {
        MinutesParked = (int)(DateTime.Now - ArrivalTime).TotalMinutes;
        TotalParkingCost = MinutesParked * CostPerMinute;
    }
}
