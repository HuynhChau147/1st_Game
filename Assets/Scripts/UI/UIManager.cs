using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused;
    private bool isGameOver;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private EndPoint endPoint;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private TextMeshProUGUI ScoreCounter;
    [SerializeField] private Player_Score player_Score;
    [SerializeField] private LifeManager player_Life;
    [SerializeField] private PlayerMovement PlayerMovement;

    // Active GameOver screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        audioSrc.PlayOneShot(gameOverSound);
        isGameOver = true;
    }

    private void Update() {
        ScoreCounter.text = "Your Score:" + player_Score.getScore().ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }

        if(endPoint.getWinStatus())
        {
            Win();
        }
    }

    // GameOver screen func
    public void Restart()
    {
        if(isPaused == true)
        {
            pauseButton.GetComponent<PauseGame>().PausedGame();
        }
        player_Life.LifeReset();
        player_Score.ResetScore();
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        if(isPaused == true)
        {
            pauseButton.GetComponent<PauseGame>().PausedGame();
        }
        isGameOver = false;
        player_Life.LifeReset();
        player_Score.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        isGameOver = false;
        player_Life.LifeReset();
        player_Score.ResetScore();
        Application.Quit();
    }

    // Pause screen func
    public void Paused()
    {
        if(isGameOver == true)
        {
            return;
        }

        if(isPaused == false)
        {
            pauseButton.SetActive(false);
            pauseButton.GetComponent<PauseGame>().PausedGame();
            pauseScreen.SetActive(true);
            isPaused = true;
        }
        else if(isPaused == true)
        {
            pauseButton.SetActive(true);
            pauseButton.GetComponent<PauseGame>().PausedGame();
            pauseScreen.SetActive(false);
            isPaused = false;
        }
    }

    // Win screen func
    public void Win()
    {
        WinScreen.SetActive(true);
        PlayerMovement.enabled = false;
    }

    public void RestartFromTheBeginning()
    {
        if(isPaused == true)
        {
            pauseButton.GetComponent<PauseGame>().PausedGame();
        }
        player_Life.LifeReset();
        player_Score.ResetScore();
        isGameOver = false;
        LevelManager.Instance.LoadScence(1);
    }
}
