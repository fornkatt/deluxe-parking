using System.Drawing;

namespace DeluxeParking; 
internal class Vehicle(string regNmbr, string color) {
    public string RegNmbr { get; set; } = regNmbr;
    public string Color { get; set; } = color;

    internal virtual void Park() {

    }
}
internal class Car(string regNmbr, string color, bool isElectric) : Vehicle(regNmbr, color) {
    public bool IsElectric { get; set; } = isElectric;

    internal override void Park() { 
    }
}
internal class Motorcycle(string regNmbr, string color, string brand) : Vehicle(regNmbr, color) {
    public string Brand { get; set; } = brand;

    internal override void Park() { 
    }
}
internal class Bus(string regNmbr, string color, int maxPassangers) : Vehicle(regNmbr, color) {
    public int MaxPassangers { get; set; } = maxPassangers;

    internal override void Park() { 
    }
}
