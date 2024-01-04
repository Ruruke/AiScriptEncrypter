namespace AiScriptEncrypter;

public class UniqueStringGenerator
{
    private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static Random random = new Random();
    private static HashSet<string> generatedStrings = new HashSet<string>();

    public static string GenerateUniqueRandomString(int length)
    {
        string newString;
        do
        {
            newString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        while (generatedStrings.Contains(newString));

        generatedStrings.Add(newString);

        return newString;
    }
}