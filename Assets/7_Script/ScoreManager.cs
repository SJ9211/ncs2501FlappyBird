using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
