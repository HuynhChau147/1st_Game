using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GemCounter;
    [SerializeField] private TextMeshProUGUI ScoresCounter;
    [SerializeField] private Player_Score p_Score;
    [SerializeField]

    private void Update() {
        GemCounter.text = p_Score.getGemCounter().ToString();
        ScoresCounter.text = "Score:" + p_Score.getScore().ToString();
    }
}
