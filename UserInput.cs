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
