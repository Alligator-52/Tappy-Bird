using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class HighScoresData
{
    public List<int> highScores;

    public HighScoresData(List<int> scores)
    {
        highScores = scores;
    }
}
public static class HighScoreHandler
{
    private const string jsonFileName = "Highscores.json";
    private const int maxHighScores = 10;

    private static string path = Path.Combine(Application.persistentDataPath, jsonFileName);
    //private static string path = Application.dataPath + "/" + jsonFileName;

    public static void AddHighScoreToJSON(int score)
    {
        List<int> highScores = LoadHighScores();
        if (highScores.Contains(score))
        {
            Debug.Log("This score already exists");
            return;
        }
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort scores in descending order

        if (highScores.Count > maxHighScores)
        {
            highScores.RemoveAt(maxHighScores);
        }

        SaveHighScores(highScores);

        // DisplayHighScores(highScores);
    }

    private static List<int> LoadHighScores()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoresData highScoresData = JsonUtility.FromJson<HighScoresData>(json);

            if (highScoresData != null)
            {
                return highScoresData.highScores;
            }
        }

        return new List<int>();
    }

    private static void SaveHighScores(List<int> highScores)
    {
        HighScoresData highScoresData = new HighScoresData(highScores);
        string json = JsonUtility.ToJson(highScoresData);
        File.WriteAllText(path, json);
    }

    public static List<int> DisplayHighScore()
    {
        List<int> highscores = LoadHighScores();
        return highscores;
    }
}
