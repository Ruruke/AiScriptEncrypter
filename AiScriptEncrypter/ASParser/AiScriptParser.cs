using System.Text.RegularExpressions;
using AiScriptEncrypter.Other;

namespace AiScriptEncrypter.ASParser;

public class AiScriptParser
{
    private static SequenceGenerator SequenceGenerator = new SequenceGenerator();
    private static string RandomString(int length)
    {
        // return SequenceGenerator.GetNext();
        return UniqueStringGenerator.GenerateUniqueRandomString(length);
    }

    public static string MinifyCode(string code)
    {
        // Remove comments
        string noComments = Regex.Replace(code, @"//.*$", "", RegexOptions.Multiline);
        
        // Remove empty lines
        string noEmptyLines = Regex.Replace(noComments, @"^\s*$\n", "", RegexOptions.Multiline);
        
        // Remove indentation
        return Regex.Replace(noEmptyLines, @"^\s+", "", RegexOptions.Multiline);
    }

    public static string aiscript(string code)
    {
        code = MinifyCode(code);
        code = code.Replace("<i>", "<dummy_i_desuwa>");
        code = code.Replace("text:", "<dummy_text_desuwa>");
        code = code.Replace("</i>", "</dummy_tozi_i_desuwa>");
        List<string> bypassList = new List<string> { "Ui:", "[\"", ".\"", "<i>", "</i>" };
        Dictionary<string, string> replacements = new Dictionary<string, string>();

        // Modify variable names
        var matchesVars = Regex.Matches(code, @"\b(let|var)\s+(\b\w+\b)(?=\s*(?![\.\[]))");  
        foreach (Match match in matchesVars)
        {
            string variableName = match.Groups[2].Value;
            if (bypassList.Any(b => code.Substring(match.Index, b.Length) == b)) continue;
            string randomVariableName = RandomString(5); // Generate a random string of length 5
            replacements[variableName] = randomVariableName;
        }

// Modify parameters in @()
        var matchesParams = Regex.Matches(code, @"@\w+\(([^)]+)\)\s*\{");  
        foreach (Match match in matchesParams)
        {
            string paramsString = match.Groups[1].Value;
            string[] paramsArray = paramsString.Split(',');

            foreach (string param in paramsArray)
            {
                string paramTrimmed = param.Trim();
                if (bypassList.Any(b => paramTrimmed == b)) 
                    continue;
        
                string randomParamName = RandomString(5); // Generate a random string of length 5
                replacements[paramTrimmed] = randomParamName;
            }
        }
// Modify function references
        var matchesFuncs = Regex.Matches(code, @"@([A-Za-z0-9_]+)\b").Cast<Match>()
            .OrderByDescending(m => m.Value.Length);  // Order by length of match descendingly to ensure longest matches are processed first
        foreach (Match match in matchesFuncs)
        {
            string functionName = match.Groups[1].Value;
            if (bypassList.Any(b => ("@" + functionName).StartsWith(b))) continue; 
            string randomFunctionName = RandomString(5); // Generate a random string of length 5
            replacements[functionName] = randomFunctionName;
        }

// Now replace all instances of the old names with the new names
        foreach (KeyValuePair<string, string> replacement in replacements)
        {
            if(replacement.Key.Contains(" ")) continue;
            Console.WriteLine($"Conv: {replacement.Key} to {replacement.Value}");
            code = Regex.Replace(code, @"\b" + replacement.Key + @"\b", replacement.Value);
        }
        
        code = code.Replace("<dummy_i_desuwa>", "<i>");
        code = code.Replace("<dummy_text_desuwa>", "text:");
        code = code.Replace("</dummy_tozi_i_desuwa>", "</i>");
        return code;
    } 
}