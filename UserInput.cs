namespace DeluxeParking;

internal class UserInput
{
    internal static string GetUserInput(string prompt, string contextHeader)
    {
        string? input;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{contextHeader}");
            Console.WriteLine();
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine();
                Console.WriteLine("Var god mata in ett värde.");
                Thread.Sleep(GlobalConstants.UserFeedbackDelay);
            }
            else
            {
                return input;
            }
        }
    }
}
