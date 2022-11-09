using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSrc;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        audioSrc.PlayOneShot(gameOverSound);
    }
}