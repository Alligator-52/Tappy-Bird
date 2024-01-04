using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header ("Texts")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI titleHsText;
    [SerializeField] private TextMeshProUGUI goScore;

    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject hsPanel;
    [SerializeField] private GameObject screenButtonPanel;

    [Header("Audio")]
    public AudioSource uiSound;

    private int score = 0;
    private int highScore = 0;
    private void Start()
    {
        Time.timeScale = 0f;
        score = 0;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = titleHsText.text = "HighScore: " + highScore.ToString();
        gameOverPanel.SetActive(false);
        uiPanel.SetActive(false);
        menuPanel.SetActive(true);
        screenButtonPanel.SetActive(true);
        hsPanel.SetActive(false);
        Debug.Log(Application.dataPath);      

        foreach(int highscore in HighScoreHandler.DisplayHighScore())
        {
            Debug.Log(highscore.ToString());
        }
    }
    public void ScoreCounter()
    {
        score += 1;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
            hsPanel.SetActive(true);
        }
        highScoreText.text = "HighScore: " + highScore.ToString();

    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        HighScoreHandler.AddHighScoreToJSON(highScore);
        goScore.text = "Score : " + PlayerPrefs.GetInt("Score");
       
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        uiSound.Play();
        uiPanel.SetActive(true);
        menuPanel.SetActive(false);
        screenButtonPanel.SetActive(false);
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        uiSound.Play();
    }
}

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

    //private static string path = Path.Combine(Application.persistentDataPath, jsonFileName);
    private static string path = Application.dataPath + "/" + jsonFileName;

    public static void AddHighScoreToJSON(int score)
    {
        List<int> highScores = LoadHighScores();
        if(!highScores.Contains(score))
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
