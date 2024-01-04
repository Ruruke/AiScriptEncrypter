namespace AiScriptEncrypter.Other;

public class SequenceGenerator
{
    private List<string> sequences;
    private int currentIndex = new Random().Next(0,500);

    public SequenceGenerator()
    {
        sequences = new List<string>();
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        foreach (var c1 in chars)
            sequences.Add(c1.ToString());

        foreach (var c1 in chars)
        foreach (var c2 in chars)
            sequences.Add($"{c1}{c2}");
    }

    public string GetNext()
    {
        if (currentIndex >= sequences.Count)
            return null; // すべての組合せを出力した場合、nullを返します
        return sequences[currentIndex++];
    }
}