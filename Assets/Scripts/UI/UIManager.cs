using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private TextMeshProUGUI ScoreCounter;
    [SerializeField] private Player_Score player_Score;
    [SerializeField] private LifeManager player_Life;

    // Active GameOver screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        audioSrc.PlayOneShot(gameOverSound);
    }

    private void Update() {
        ScoreCounter.text = "Your Score:" + player_Score.getScore().ToString();
    }

    // GameOver screen func
    public void Restart()
    {
        player_Life.LifeReset();
        player_Score.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        player_Life.LifeReset();
        player_Score.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        player_Life.LifeReset();
        player_Score.ResetScore();
        Application.Quit();
    }

}
