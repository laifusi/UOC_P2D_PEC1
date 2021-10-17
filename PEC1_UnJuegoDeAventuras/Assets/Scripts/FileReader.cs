using System;
using UnityEngine;

public static class FileReader
{
    /// <summary>
    /// Method that loads a json and converts its data into arrays of strings.
    /// </summary>
    /// <param name="dataType">Type of date we want: insult or answer.</param>
    /// <returns>Array of strings corresponding to the specified type of data.</returns>
    public static string[] ReadFile(TypeOfTurn dataType)
    {
        string json = Resources.Load<TextAsset>("Data").text;
        Data data = JsonUtility.FromJson<Data>(json);

        string[] fileData = new string[data.data.Length];

        if(dataType == TypeOfTurn.Insult)
        {
            for(int i = 0; i < data.data.Length; i++)
            {
                fileData[i] = data.data[i].insult;
            }
        }
        else
        {
            for (int i = 0; i < data.data.Length; i++)
            {
                fileData[i] = data.data[i].answer;
            }
        }

        return fileData;
    }

    [Serializable]
    public struct Data
    {
        [Serializable]
        public struct DataEntry
        {
            public string insult;
            public string answer;
        }

        public DataEntry[] data;
    }
}
