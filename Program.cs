namespace DeluxeParking;

internal class Program
{
    private static void Main()
    {
        ParkingGarage parkingGarage = new(15);
        Queue<IVehicle> vehicleQueue = [];

        Menu.PrintMainMenu(parkingGarage, vehicleQueue);
    }
}
