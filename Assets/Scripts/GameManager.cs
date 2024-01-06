using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*[Header ("Texts")]
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
    public AudioSource uiSound;*/
    [SerializeField] public UIHandler uiHandler;
    private int score = 0;
    public int Score {get { return score; } private set { } }

    private int highScore = 0;
    public int HighScore { get { return highScore; } private set { } }
    private void Start()
    {
        Time.timeScale = 0f;
        score = 0;
        //PlayerPrefs.SetInt("Score", score);
        List<int> highscores = HighScoreHandler.DisplayHighScore();
        //highScore = PlayerPrefs.GetInt("HighScore");
        highScore = (highscores.Count > 0 ? highscores[0] : 0);
       
        //Debug.Log(Application.dataPath);
    }
    public void ScoreCounter()
    {
        score += 1;
        //PlayerPrefs.SetInt("Score", score);
        uiHandler.scoreText.text = score.ToString();
        if(score > highScore)
        {
            //PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
            uiHandler.hsPanel.SetActive(true);
        }
        uiHandler.highScoreText.text = "HighScore: " + highScore.ToString();

    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        uiHandler.gameOverPanel.SetActive(true);
        HighScoreHandler.AddHighScoreToJSON(score);
        uiHandler.goScore.text = "Score : " + PlayerPrefs.GetInt("Score");
       
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        uiHandler.uiSound.Play();
        uiHandler.uiPanel.SetActive(true);
        uiHandler.menuPanel.SetActive(false);
        uiHandler.screenButtonPanel.SetActive(false);
    }

    public void ResetScore()
    {
        //PlayerPrefs.DeleteKey("HighScore");
        HighScoreHandler.DeleteScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        uiHandler.uiSound.Play();
    }
}




