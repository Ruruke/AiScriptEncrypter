using System.Collections;
using AiScriptEncrypter.ASParser;
using WhiteCssGenerater;

namespace AiScriptEncrypter.Cui;

public class MainWindow
{
    public MainWindow()
    {
        string originalPath = @"";
        // string originalPath = @"C:\Users\ruru\Desktop\misskey-play\country-quiz\debug.aiscript";
        
        Console.Write("Please enter the file path: ");
        originalPath = Console.ReadLine()!;

        string directory = Path.GetDirectoryName(originalPath)!;
        if (! Directory.Exists(directory) &&  !File.Exists(originalPath)  )
        {
            Console.WriteLine($"WARN! The provided path is not correct. [path: {originalPath}]");
            Environment.Exit(-1);
        }
        string filenameWithoutExtension = Path.GetFileNameWithoutExtension(originalPath);
        string extension = Path.GetExtension(originalPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExtension}.min{extension}");
        String code = FileManager.ReadFile(originalPath);

        string res = AiScriptParser.aiscript(code);
        ArrayList arrayList = new ArrayList();

        arrayList.AddRange(res.Split(new[] { "\n", Environment.NewLine }, StringSplitOptions.None));
        Console.WriteLine("Output File Path > "+outputPath);
        FileManager.WriteFile(outputPath,arrayList);
    }
}