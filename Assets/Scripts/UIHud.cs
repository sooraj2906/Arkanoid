using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHud : MonoBehaviour
{
    public TMP_Text score_text;
    public TMP_Text lives_text;
    public GameObject gameOverScreen;
    public GameObject gameScore;
    public GameObject gameLives;
    public TMP_Text gameOverScore;

    private void Start()
    {
        //Registering the created events
        GameManager.pInstance.OnScoreUpdated += UpdateScore;
        GameManager.pInstance.OnLivesUpdated += UpdateLives;
        GameManager.pInstance.OnGameEnd += GameEnd; 
        GameManager.pInstance.OnStart += GameStart; 
    }

    /// <summary>
    /// Updates the score in the UI
    /// </summary>
    /// <param name="score">score received from GameManager</param>
    private void UpdateScore(int score)
    {
        score_text.text = score.ToString();
    }

    private void UpdateLives(int lives)
    {
        lives_text.text = lives.ToString();
    }
    /// <summary>
    /// Shows the game over screen and assign the score to the score text in the game over screen
    /// </summary>
    /// <param name="score">score received from GameManager</param>
    private void GameEnd(int score)
    {
        gameScore.SetActive(false);
        gameLives.SetActive(false);
        score_text.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    /// <summary>
    /// Shows the score text in UI
    /// </summary>
    private void GameStart()
    {
        gameScore.SetActive(true);
        gameLives.SetActive(true);
        score_text.gameObject.SetActive(true);
        gameOverScore.text = "0";
        gameOverScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        //Deregistering the created events
        if (GameManager.pInstance != null)
        {
            GameManager.pInstance.OnScoreUpdated -= UpdateScore;
            GameManager.pInstance.OnLivesUpdated -= UpdateLives;
            GameManager.pInstance.OnGameEnd -= GameEnd;
            GameManager.pInstance.OnStart -= GameStart;
        }
    }
}
