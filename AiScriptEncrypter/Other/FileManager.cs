using System.Collections;
using System.Text;

namespace WhiteCssGenerater;

public static class FileManager
{
    public static string ReadFile(string path)
    {
        //TODO: なんか信用性ないので別の読み取りかたすべき。ちゃんと解放されていなさそう。
        var arrayList = new ArrayList();
        var s = 0;
        using var sr = new StreamReader(path, Encoding.UTF8);
        while (sr.ReadLine() is { } line)
        {
            if (s >= 3000) break;
            arrayList.Add(line);
            s++;
        }

        return String.Join(Environment.NewLine, arrayList.Cast<object>().Select(x => x.ToString()));
    }

    public static void WriteFile(string path, ArrayList text)
    {
        using var writer = new StreamWriter(path, false, Encoding.UTF8);
        foreach (var s in text) writer.WriteLine(s);
    }
}