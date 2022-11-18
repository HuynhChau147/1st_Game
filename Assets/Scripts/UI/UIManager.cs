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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
