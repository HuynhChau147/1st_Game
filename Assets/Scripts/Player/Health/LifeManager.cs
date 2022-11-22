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
        if(PlayerPrefs.GetInt("currentLife") == 0)
        {
            currentLife = startingLife;
            return;
        }
        currentLife = PlayerPrefs.GetInt("currentLife");
    }

    // Update is called once per frame
    private void Update() {
        PlayerPrefs.SetInt("currentLife",currentLife);
    }

    public void LostLife()
    {
            currentLife -= 1;
    }

    public int getLifeCounter()
    {
        return currentLife;
    }

    public void LifeReset()
    {
        PlayerPrefs.DeleteKey("currentLife");
        currentLife = 0;
    }
}
