namespace DeluxeParking;

internal class ParkingMeter(IVehicle vehicle, string licenseNumber) : IParkingMeter
{
    public double CostPerMinute { get; } = GlobalConstants.DefaultParkingCostMinute;
    public int MinutesParked { get; set; }
    public double TotalParkingCost { get; set; }
    public string LicenseNumber { get; init; } = licenseNumber;
    public IVehicle Vehicle { get; init; } = vehicle;
    public DateTime ArrivalTime { get; init; } = DateTime.Now;

    public void CalculateTotalCost()
    {
        MinutesParked = (int)(DateTime.Now - ArrivalTime).TotalMinutes;
        TotalParkingCost = MinutesParked * CostPerMinute;
    }
}
