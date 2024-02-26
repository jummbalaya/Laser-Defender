using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeeper scoreKeeeper;

    void Awake()
    {
        scoreKeeeper = FindObjectOfType<ScoreKeeeper>();
    }

    void Start()
    {
        scoreText.text = "Your score was:\n" + scoreKeeeper.GetScore().ToString();
    }
}
