using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LifeCounterText;
    [SerializeField] private LifeManager life;

    void Update()
    {
        LifeCounterText.text = "X " + life.getLifeCounter().ToString();
    }
}
