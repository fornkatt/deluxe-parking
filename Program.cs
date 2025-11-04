namespace DeluxeParking;

internal class Program
{
    private static void Main()
    {
        ParkingGarage parkingGarage = new(GlobalConstants.DefaultParkingCapacity);

        Menu.PrintMainMenu(parkingGarage);
    }
}
