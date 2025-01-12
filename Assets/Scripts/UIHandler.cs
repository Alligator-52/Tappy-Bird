using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject highscoreElement;
    [SerializeField] private RectTransform highscoreHolder;
    [Header("Main Menu")]
    public GameObject mainMenu;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button resetScoreButton;
    [SerializeField] private Button gameBeginButton;
    [SerializeField] private TextMeshProUGUI titleHsText;
    [SerializeField] private TextMeshProUGUI noScoresText;

    [Header("HighScores menu")]
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Button leaderboardCloseButton;

    [Header("In Game UI")]
    public GameObject uiPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    [SerializeField] private Button pauseButton;

    [Header("Pause Menu")]
    public GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseMenuRestartButton;
    [SerializeField] private Button pauseMenuExitBtn;

    [Header("GameOver Menu")]
    public GameObject gameOverHS;
    public GameObject gameOverPanel;
    public TextMeshProUGUI goScore;
    public Button gameOverRestartBtn;
    public Button gameOverExitBtn;

    [Header("Audio")]
    public AudioSource uiSound;

    private GameManager gameManager;
    private List<GameObject> highScoreElements = new();

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); 
        
        scoreText.text = gameManager.Score.ToString();
        List<int> highscores = HighScoreHandler.GEtHighScores();
        highScoreText.text = titleHsText.text = $"HighScore: {(highscores.Count > 0 ? highscores[0] : 0)}";
        gameOverPanel.SetActive(false);
        uiPanel.SetActive(false);
        //menuPanel.SetActive(true);
        mainMenu.SetActive(true);
        gameOverHS.SetActive(false);
    }
    void OnEnable()
    {
        noScoresText.gameObject.SetActive(false);
        leaderboardPanel.SetActive(false);

        AddButtonListeners();

        LoadHighscoreElements();
    }

    private void AddButtonListeners()
    {
        leaderboardButton.onClick.AddListener(OnLeaderBoardButton);
        leaderboardCloseButton.onClick.AddListener(() =>
        {
            leaderboardPanel.SetActive(false);
        });
        resetScoreButton.onClick.AddListener(OnResetScore);
        gameBeginButton.onClick.AddListener(() =>
        {
            OnGameBegin();
        });
        pauseButton.onClick.AddListener(() => OnPause());

        pauseMenuExitBtn.onClick.AddListener(() => gameManager.QuitGame());
        pauseMenuRestartButton.onClick.AddListener(() => gameManager.RestartGame());
        resumeButton.onClick.AddListener(() => OnResume());

        gameOverExitBtn.onClick.AddListener(() => gameManager.QuitGame());
        gameOverRestartBtn.onClick.AddListener(() => gameManager.RestartGame());
    }

    public void OnGameBegin()
    {
        uiSound.Play();
        uiPanel.SetActive(true);
        //menuPanel.SetActive(false);
        mainMenu.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        gameManager.StartGame();
    }

    public void OnGameOver(int score, bool isHighScore)
    {
        gameOverPanel.SetActive(true);
        goScore.text = "Score : " + score;
        pauseButton.gameObject.SetActive(false);
        if (isHighScore ) 
            gameOverHS.SetActive(true);
    }

    public void OnCloseButton()
    {
        leaderboardPanel.SetActive(false);
    }
    public void OnResetScore()
    {
        gameManager.ResetScores();
        uiSound.Play();
    }

    public void OnPause()
    {
        uiSound.Play();
        gameManager.PauseGame();
        pausePanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void OnResume()
    {
        uiSound.Play();
        gameManager.ResumeGame();
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void SetHighScoreText(string highScore)
    {
        highScoreText.text = "HighScore: " + highScore;
    }

    private void LoadHighscoreElements()
    {
        if (HighScoreHandler.GEtHighScores().Count != 0 && HighScoreHandler.GEtHighScores() != null)
        {
            for (int i = 0; i < 10; i++)
            {
                var hsElement = Instantiate(highscoreElement, highscoreHolder);
                highScoreElements.Add(hsElement);
                var hsElementIndex = hsElement.transform.GetChildWithName("index").GetComponentInChildren<TextMeshProUGUI>(true);
                var hsElementScore = hsElement.transform.GetChildWithName("score").GetComponentInChildren<TextMeshProUGUI>(true);

                hsElementIndex.text = "";
                hsElementScore.text = "";
            }
        }
    }

    private void OnLeaderBoardButton()
    {
        List<int> highscores = HighScoreHandler.GEtHighScores();
        if(highscores.Count == 0 || highscores == null) 
        {
            noScoresText.gameObject.SetActive(true);
            leaderboardPanel.SetActive(true);
            Debug.Log("We are in the no scores region");
            return;
        }
        Debug.Log("Button clicked, we have reached here");
        noScoresText.gameObject.SetActive(false);
        leaderboardPanel.SetActive(true);
        for(int i = 0; i < highscores.Count; i++)
        {
            var hsElement = highScoreElements[i];
            var hsElementIndex = hsElement.transform.GetChildWithName("index").GetComponentInChildren<TextMeshProUGUI>(true);
            var hsElementScore = hsElement.transform.GetChildWithName("score").GetComponentInChildren<TextMeshProUGUI>(true);

            hsElementIndex.text = $"{i+1}.";
            hsElementScore.text = $"{highscores[i]}";
            //hsElemenText.text = $"{i+1}. {highscores[i]}";
        }

    }
    private void OnDisable()
    {
        leaderboardButton.onClick.RemoveAllListeners();
        leaderboardCloseButton.onClick.RemoveAllListeners();
        resetScoreButton.onClick.RemoveAllListeners();
    }

}
