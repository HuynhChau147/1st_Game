using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private AudioSource audioSrc;

    // MainMenu screen func
    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        LevelManager.Instance.LoadScence(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
