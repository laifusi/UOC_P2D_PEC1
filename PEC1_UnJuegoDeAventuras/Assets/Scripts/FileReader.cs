using System;
using System.Collections.Generic;
using UnityEngine;

public static class FileReader
{
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
