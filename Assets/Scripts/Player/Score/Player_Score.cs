using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Score : MonoBehaviour
{
    private long currentScore = 0;
    private int currentGems = 0;

    public void AddScore(int addvalue)
    {
        currentScore += addvalue;
    }

    public void TakeScore(int takevalue)
    {
        currentScore -= takevalue;
    }

    public void AddGem(int GemCount)
    {
        currentGems += GemCount;
    }

    public long getScore()
    {
        return currentScore;
    }

    public int getGemCounter()
    {
        return currentGems;
    }
}
