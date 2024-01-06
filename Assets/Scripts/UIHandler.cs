using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject highscoreElement;
    [SerializeField] private Transform highscoreHolder;

    [Header("Buttons")]
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button leaderboardCloseButton;
  
    
    [Header("UI Texts")]
    //[SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI noScoresText;

    [Header("UI Panels")]
    [SerializeField] private GameObject leaderboardPanel;


    void Awake()
    {
        noScoresText.gameObject.SetActive(false);
        leaderboardPanel.SetActive(false);
        leaderboardButton.onClick.AddListener(OnLeaderBoardButton);
        leaderboardCloseButton.onClick.AddListener(() => 
        {
            leaderboardPanel.SetActive(false);
            Debug.Log("Elements destroyed!");
            foreach(Transform child in highscoreHolder)
            {
                Destroy(child.gameObject);
            }
        }); 

    }

    private void OnDisable()
    {
        leaderboardButton.onClick.RemoveAllListeners();
        leaderboardCloseButton.onClick.RemoveAllListeners();
    }

    private void OnLeaderBoardButton()
    {
        leaderboardPanel.SetActive(true);
        List<int> highscores = HighScoreHandler.DisplayHighScore();
        if(highscores.Count == 0 || highscores == null) 
        {
            noScoresText.gameObject.SetActive(true);
            return;
        }

        noScoresText.gameObject.SetActive(false);
        for(int i = 0; i < highscores.Count; i++)
        {
            var hsElement = Instantiate(highscoreElement, highscoreHolder);
            var hsElemenText = hsElement.GetComponentInChildren<TextMeshProUGUI>(true);
            hsElemenText.text = $"{i+1}. {highscores[i]}";
        }

    }
}
