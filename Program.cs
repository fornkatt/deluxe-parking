namespace DeluxeParking;

internal class Program {
    static void Main() {
        List<Vehicle> vehicles = VehicleHelpers.GenerateCar();

        foreach (Vehicle vehicle in vehicles) {
            if (vehicle is Motorcycle motorcycle) {
                Console.WriteLine($"Motorcykel: {motorcycle.Brand}, {motorcycle.Color}, {motorcycle.RegNmbr}");
            }
        }
    }
}
