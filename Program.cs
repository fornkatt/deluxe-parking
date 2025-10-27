namespace DeluxeParking;

internal class Program {
    static void Main() {
        ParkingGarage parkingGarage = new(15.0, 0.0, []);
        Queue<IVehicle> vehicleQueue = [];

        Menu.ShowMenu(parkingGarage, vehicleQueue);

        
    }
}
