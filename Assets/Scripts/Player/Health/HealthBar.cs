using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health player_Health;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start() {
        totalHealthbar.fillAmount = player_Health.currentHealth / 10;
    }

    private void Update() {
        currentHealthbar.fillAmount = player_Health.currentHealth / 10;
    }
}
