namespace DeluxeParking;

internal class Program {
    private static Queue<Vehicle> vehicleQueue = [];
    static void Main() {
        ParkingGarage parkingGarage = new(15.0, []);

        parkingGarage.ParkedVehicles.Add(VehicleHelpers.GenerateNewVehicle());
        Console.WriteLine("Nytt fordon parkerat.");
        Thread.Sleep(2000);
        Console.Clear();

        foreach (Vehicle vehicle in parkingGarage.ParkedVehicles) {
            if (vehicle is Car car) {
                Console.WriteLine($"Bil: Elektrisk: {car.IsElectric}, {car.Color}, {car.LicenseNumber}");
            }
            else if (vehicle is Motorcycle motorcycle) {
                Console.WriteLine($"Motorcykel: Märke: {motorcycle.Brand}, {motorcycle.Color}, {motorcycle.LicenseNumber}");
            }
            else if (vehicle is Bus bus) {
                Console.WriteLine($"Buss: Passagerare: {bus.MaxPassangers}, {bus.Color}, {bus.LicenseNumber}");
            }
        }
    }
}
