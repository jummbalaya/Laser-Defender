using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeeper scoreKeeeper;

    private void Awake()
    {
        scoreKeeeper = FindObjectOfType<ScoreKeeeper>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeeper.GetScore().ToString("00000000");
    }

}
