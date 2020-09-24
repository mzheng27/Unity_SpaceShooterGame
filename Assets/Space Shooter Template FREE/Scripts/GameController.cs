using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public Text scoreText;
    public GameObject winGraphic;

    public Player player;
    public LevelController levelController;

    private int _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        resetUI();
        ActivateGame(false);
        _currentScore = 0;
        startCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        resetUI();
        ActivateGame(true);
    }

    public void GameOver()
    {
        resetUI();
        ActivateGame(false);
        checkAndDisplayWin();
        gameOverCanvas.gameObject.SetActive(true);
        
    }

    public void Replay()
    {
        string mainSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(mainSceneName);
    }

    private void resetUI()
    { 
        startCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void ActivateGame(bool activate)
    {
        player.gameObject.SetActive(activate);
        levelController.gameObject.SetActive(activate);
        gameCanvas.gameObject.SetActive(activate);
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    public void IncrementScore(int point)
    {
        _currentScore += point;
        scoreText.text = _currentScore.ToString();
    }

    private void checkAndDisplayWin()
    {
        if (HighScoreController.Instance.AddHighScore(_currentScore))
        {
            winGraphic.SetActive(true);
        } 
        else
        {
            winGraphic.SetActive(false);
        }
    }
}
