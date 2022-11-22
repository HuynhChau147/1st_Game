using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Score : MonoBehaviour
{
    private int currentScore = 0;
    private int currentGems = 0;

    private void Awake() {
        currentGems = PlayerPrefs.GetInt("currentGems");
        currentScore = PlayerPrefs.GetInt("currentScore");
    }

    private void Update() {
        PlayerPrefs.SetInt("currentGems", currentGems);
        PlayerPrefs.SetInt("currentScore", currentScore);
    }

    public void AddScore(int addvalue)
    {
        currentScore += addvalue;
    }

    public void TakeScore(int takevalue)
    {
        if(currentScore <= 0)
        {
            return;
        }
        
        currentScore -= takevalue;
    }

    public void AddGem(int GemCount)
    {
        currentGems += GemCount;
    }

    public int getScore()
    {
        return currentScore;
    }

    public int getGemCounter()
    {
        return currentGems;
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("currentGems");
        PlayerPrefs.DeleteKey("currentScore");
        currentGems = 0;
        currentScore = 0;
    }
}
