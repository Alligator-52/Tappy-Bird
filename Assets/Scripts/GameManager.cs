using System.Collections.Generic;
using UnityEngine;
using Support;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public UIHandler uiHandler;
    private int score = 0;
    public int Score {get { return score; } private set { } }

    private int highScore = 0;
    public int HighScore { get { return highScore; } private set { } }

    public PlayState GameState;

    public static GameManager Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        OnMenu();
        List<int> highscores = HighScoreHandler.GEtHighScores();
        highScore = (highscores.Count > 0 ? highscores[0] : 0);
        
    }

    public void CountScore()
    {
        score += 1;
        PlayerPrefs.SetInt("Score", score);
        uiHandler.scoreText.text = score.ToString();
        if(score > highScore)
        {
            highScore = score;
        }
        uiHandler.SetHighScoreText(highScore.ToString());

    }

    public void OnMenu()
    {
        score = 0;
        Time.timeScale = 0f;
        GameState = PlayState.Begin;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        GameState = PlayState.Playing;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameState = PlayState.Begin;
    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        GameState = PlayState.Dead;
        uiHandler.OnGameOver(score, score >= highScore);
        HighScoreHandler.AddHighScoreToJSON(score);
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameState = PlayState.Paused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameState = PlayState.Playing;
    }

    public void QuitGame()
    {
        uiHandler.uiSound.Play();
        Application.Quit();
    }

    public void ResetScores()
    {
        HighScoreHandler.DeleteScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnApplicationPause(bool pause)
    {
        if(GameState == PlayState.Playing)
        {
            PauseGame();
            uiHandler.OnPause();
        }
    }


}




