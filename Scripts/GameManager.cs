using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float  GAME_DURATION = 60f;

    /* ENCAPSULATION 
     *  - GameManager instance can be accesed by anyone, but no one else can set it
     *  - So does GameEnded
     *  - Score set prevents m_score from being negative
     */
    public static   GameManager Instance { get; private set; }
    [SerializeField]
    private Spawner             spawner;
    public  bool                GameEnded {  get; private set; }
    private int                 m_score;
    private float               remainingTime;

    // Properties
    public  int     Score
    {
        get { return m_score; }
        set { m_score = Math.Max(value, 0); }
    }

    [SerializeField]
    private GameObject  gameOverScreen;
    [SerializeField]
    private TMP_Text    scoreText;
    [SerializeField]
    private TMP_Text    timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance        = this;
        Score           = 0;
        remainingTime   = GAME_DURATION;
    }

    private void Update() {
        if(!GameEnded) {
            UpdateRemainingTime();
            UpdateHUD();
        }
    }

    private void UpdateRemainingTime() {
        remainingTime -= Time.deltaTime;

        if(remainingTime <= 0f) { 
            GameFinished();
        }
        else if(spawner != null)
        {
            if      (remainingTime <= 5)    spawner.SetSpawnInterval(0.2f);
            else if (remainingTime <= 15)   spawner.SetSpawnInterval(0.5f);
            else if (remainingTime <= 30)   spawner.SetSpawnInterval(0.7f);
            else if (remainingTime <= 45)   spawner.SetSpawnInterval(1f);
        }

    }

    /* ABSTRACTION
     * Update all HUD texts calling UpdateHUD. 
     * There is no need to know how many text has and what values will have.
     */
    public void UpdateHUD() {
        if(scoreText != null) {
            scoreText.text = "Points: " + Score;
        }
        if(timeText != null) { 
            timeText.text = "Time: " + Mathf.CeilToInt(remainingTime);
        }
    }

    public void IncreaseScore(int points) { 
        Score += points;

        UpdateHUD();
    }

    public void DecreaseScore(int points) {  
        Score -= points;

        UpdateHUD();
    }

    private void GameFinished() { 
        GameEnded = true;

        ShowGameOverScreen();
    }

    private void ShowGameOverScreen() {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
