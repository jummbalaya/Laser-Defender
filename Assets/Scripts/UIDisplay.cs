using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health&Shield")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;
    [SerializeField] Slider dashSlider;
    [SerializeField] Slider shieldSlider;
    
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Misc")]
    [SerializeField] GameObject auxPanel;

    ScoreKeeeper scoreKeeeper;
    PlayerController playerController;
    

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        scoreKeeeper = FindObjectOfType<ScoreKeeeper>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        shieldSlider.maxValue = playerController.shieldMaxHp;
        dashSlider.maxValue = playerController.GetDashCooldown();
    }

    private void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeeper.GetScore().ToString("00000000");

        dashSlider.value = playerController.GetDashCooldownTimer();

        if (playerController.HasShieldUp == true)
        {
            auxPanel.SetActive(true);
            shieldSlider.value = playerController.shieldHP;
        }
        else if (playerController.HasShieldUp == false)
        {
            auxPanel.SetActive(false);
        }
        
    }
}
