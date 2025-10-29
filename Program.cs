namespace DeluxeParking;

internal class Program {
    static void Main() {
        ParkingGarage parkingGarage = new(15);
        Queue<IVehicle> vehicleQueue = [];

        Console.CursorVisible = false;

        Info.PrintHeader();
        Console.ReadKey(true);
        Menu.PrintMainMenu(parkingGarage, vehicleQueue); 
    }
}
