using UnityEngine;

public static class FileReader
{
    public static string[] ReadFile(string fileName)
    {
        string fileText = Resources.Load<TextAsset>(fileName).text;
        string[] fileLines = fileText.Split("\n"[0]);

        return fileLines;
    }
}
