using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject highscoreElement;
    [SerializeField] private Transform highscoreHolder;

    [Header("Buttons")]
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button leaderboardCloseButton;
    [SerializeField] private Button resetScoreButton;
  
    
    [Header("UI Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI titleHsText;
    public TextMeshProUGUI goScore;
    [SerializeField] private TextMeshProUGUI noScoresText;

    [Header("UI Panels")]
    [SerializeField] private GameObject leaderboardPanel;
    public GameObject gameOverPanel;
    public GameObject uiPanel;
    public GameObject menuPanel;
    public GameObject hsPanel;
    public GameObject screenButtonPanel;

    [Header("Audio")]
    public AudioSource uiSound;

    private GameManager gameManager;
    private List<GameObject> highScoreElements = new();

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreText.text = gameManager.Score.ToString();
        List<int> highscores = HighScoreHandler.DisplayHighScore();
        highScoreText.text = titleHsText.text = $"HighScore: {(highscores.Count > 0 ? highscores[0] : 0)}";
        gameOverPanel.SetActive(false);
        uiPanel.SetActive(false);
        menuPanel.SetActive(true);
        screenButtonPanel.SetActive(true);
        hsPanel.SetActive(false);
    }
    void OnEnable()
    {
        noScoresText.gameObject.SetActive(false);
        leaderboardPanel.SetActive(false);
        leaderboardButton.onClick.AddListener(OnLeaderBoardButton);
        leaderboardCloseButton.onClick.AddListener(OnCloseButtonClicked);
        resetScoreButton.onClick.AddListener(ResetScore);
        Debug.Log($"Count : {HighScoreHandler.DisplayHighScore().Count}");
        if(HighScoreHandler.DisplayHighScore().Count != 0 && HighScoreHandler.DisplayHighScore() != null)
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

    private void OnDisable()
    {
        leaderboardButton.onClick.RemoveAllListeners();
        leaderboardCloseButton.onClick.RemoveAllListeners();
        resetScoreButton.onClick.RemoveAllListeners();
    }

    private void OnLeaderBoardButton()
    {
        List<int> highscores = HighScoreHandler.DisplayHighScore();
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

    public void OnCloseButtonClicked()
    {
        leaderboardPanel.SetActive(false);
        Debug.Log("Elements destroyed!");
        foreach (Transform child in highscoreHolder)
        {
            Destroy(child.gameObject);
        }
    }
    public void ResetScore()
    {
        //PlayerPrefs.DeleteKey("HighScore");
        HighScoreHandler.DeleteScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        uiSound.Play();
    }
}
