using System.Collections.Immutable;

namespace DeluxeParking;

internal class VehicleHelpers {
    private static readonly ImmutableArray<string> _vehicleBrand =
        ["Honda", "Yamaha", "Harley-Davidson", "Kawasaki",
         "Suzuki", "BMW", "Royal Enfield", "Benelli"];
    
    private static readonly ImmutableArray<char> _regLetters =
        ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
         'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
    
    private static readonly ImmutableArray<string> _vehicleColors =
        ["Vit", "Svart", "Grå", "Silver", "Röd", "Blå", "Brun", "Grön",
         "Beige", "Orange", "Guld", "Gul", "Lila", "Marinblå", "Vinröd"];

    private static string GetRandomBrand() {
        return _vehicleBrand[Random.Shared.Next(_vehicleBrand.Length)];
    }
    
    private static string GenerateRandomRegNmbr() {
        Span<char> regNmbr = stackalloc char[6];
        bool lastSymbolIsLetter = Random.Shared.Next(2) == 0;

        for (int i = 0; i < 6; i++) {
            if (i == 5 && lastSymbolIsLetter || i < 3) {
                regNmbr[i] = _regLetters[Random.Shared.Next(_regLetters.Length)];
            }
            else {
                regNmbr[i] = (char)('0' + Random.Shared.Next(10));
            }
        }
        return new string(regNmbr);
    }
    
    private static string GetRandomCarColor() {
        return _vehicleColors[Random.Shared.Next(_vehicleColors.Length)];
    }

    public static List<Vehicle> GenerateCar() {
        List<Vehicle> vehicles = [];
        for (int i = 0; i < 10; i++) {
            vehicles.Add(new Motorcycle(GenerateRandomRegNmbr(), GetRandomCarColor(), GetRandomBrand()));
        }
        return vehicles;
    }
}
