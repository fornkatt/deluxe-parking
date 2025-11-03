namespace DeluxeParking;

internal class UserInput
{
    internal static string GetUserInput(string prompt, string descriptionHeader)
    {
        string? input;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{descriptionHeader}");
            Console.WriteLine();
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || input.Length <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Var god mata in ett värde.");
                Thread.Sleep(2000);
            }
            else
            {
                break;
            }
        }
        return input;
    }
}
