namespace DeluxeParking;

internal interface IParkingMeter
{
    double CostPerMinute { get; }
    int MinutesParked { get; set; }
    double TotalParkingCost { get; set; }
    string LicenseNumber { get; init; }
    IVehicle Vehicle { get; init; }
    DateTime ArrivalTime { get; init; }

    void CalculateTotalCost();
}
