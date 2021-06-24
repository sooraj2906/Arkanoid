using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

/// <summary>
/// Game Manager class manages the gameflow and hodls the public references to all the classes
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Score")]
    private int score;

    [SerializeField] private int playerLives = 3;
    private int remainingLives;
    private List<GameObject> bricks;


    [Header("Public References")]
    public BallController ball;
    public PaddleController paddle;
    public PowerUpController powerUpController;
    public TileGenerator tileGenerator;

    public System.Action<int> OnScoreUpdated;
    public System.Action<int> OnGameEnd;
    public System.Action<int> OnGameWin;
    public System.Action OnStart;
    public System.Action<GameObject> OnBrickDestruction;
    public System.Action<int> OnLivesUpdated;

    public static GameManager pInstance { get; private set; }
    public bool pAllowInputs { get; private set; }

    private void Awake()
    {
        if (pInstance == null)
            pInstance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        print(tileGenerator.GetTiles());
    }


    /// <summary>
    /// Check remaining lives when the ball is destroyed and end the game if no lives or reset the ball to the paddle
    /// </summary>
    public void OnBallDestroyed()
    {
        remainingLives--;
        OnLivesUpdated?.Invoke(remainingLives);
        if (remainingLives <= 0)
        {
            pAllowInputs = false;
            OnGameEnd?.Invoke(score);
        }
        else
        {
            ball.ResetBall();
        }
    }
    /// <summary>
    /// Resets the score and invoke OnStart in other classes
    /// </summary>
    public void StartGame()
    {
        remainingLives = playerLives;
        OnLivesUpdated?.Invoke(remainingLives);
        OnStart?.Invoke();
        UpdateScore(-score);
        pAllowInputs = true;
    }

    /// <summary>
    /// Updates the score in the UI
    /// </summary>
    /// <param name="value">the score value</param>
    void UpdateScore(int value)
    {
        score += value;
        OnScoreUpdated?.Invoke(score);
    }


    /// <summary>
    /// Calculate and add the score when a brick is destroyed
    /// </summary>
    /// <param name="brick"></param>
    public void OnBrickDestroyed(BrickController brick)
    {
        if (ball.timer < 0.1f)
        {
            if (ball.isFirstBrick)
            {
                if (brick.hits == brick.hitsToDestroy)
                {
                    UpdateScore(10);
                    ball.prevScore = 10;
                }
                ball.isFirstBrick = false;
            }
            else
            {
                //Bonus score
                UpdateScore((int)(ball.prevScore + (ball.prevScore * 0.1f)));
                ball.prevScore = ball.prevScore + (ball.prevScore * 0.1f);
            }

        }
        else
        {
            UpdateScore(10);
            ball.prevScore = 10;
        }

        ball.isBonusTimer = true;
        ball.timer = 0f;
        OnScoreUpdated?.Invoke(score);
        OnBrickDestruction?.Invoke(brick.gameObject);
        if (tileGenerator.GetTiles() <= 0)
        {
            OnGameWin?.Invoke(score);
        }
    }


    private void OnDestroy()
    {
        pInstance = null;
    }
}
