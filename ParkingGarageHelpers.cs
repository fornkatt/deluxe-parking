namespace DeluxeParking;

internal class ParkingGarageHelpers
{
    internal static SortedDictionary<int, List<IVehicle>> InitializeParkingSpots(int maxSpots)
    {
        var spots = new SortedDictionary<int, List<IVehicle>>();
        for (int i = 1; i <= maxSpots; i++)
        {
            spots[i] = [];
        }
        return spots;
    }
}
