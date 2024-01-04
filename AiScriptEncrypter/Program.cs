// See https://aka.ms/new-console-template for more information

using System.Collections;
using AiScriptEncrypter.ASParser;
using WhiteCssGenerater;

class Program
{
    static void Main(string[] args)
    {
        string originalPath = @"C:\Users\ruru\Desktop\misskey-play\country-quiz\main.aiscript";
        // string originalPath = @"C:\Users\ruru\Desktop\misskey-play\country-quiz\debug.aiscript";
        
        Console.Write("ファイルパスを入力してください: ");
        originalPath = Console.ReadLine()!;

        string directory = Path.GetDirectoryName(originalPath)!;
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
