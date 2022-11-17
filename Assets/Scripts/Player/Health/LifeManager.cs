using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int startingLife;
    private int currentLife;
    private bool OverHealth;

    // Start is called before the first frame update
    void Awake()
    {
        currentLife = startingLife;
    }

    // Update is called once per frame

    public void LostLife()
    {
            currentLife -= 1;
    }

    public int getLifeCounter()
    {
        return currentLife;
    }
}
