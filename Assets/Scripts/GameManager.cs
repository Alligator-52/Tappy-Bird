using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        highScoreText.text = "HighScore: " + highScore.ToString();
        titleHsText.text = "HighScore: " + highScore.ToString();
        gameOverPanel.SetActive(false);
        uiPanel.SetActive(false);
        menuPanel.SetActive(true);
        screenButtonPanel.SetActive(true);
        hsPanel.SetActive(false);

    }
    public void ScoreCounter()
    {
        score += 1;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        if(score >= highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = PlayerPrefs.GetInt("HighScore");
            hsPanel.SetActive(true);
        }
        highScoreText.text = "HighScore: " + highScore.ToString();

    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
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
