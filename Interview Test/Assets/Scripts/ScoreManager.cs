using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    TMP_Text scoreText;

    private int m_GameScore;

    private void Start()
    {
        UpdateScore();
    }

    public int GetScore()
    {
        return m_GameScore;
    }    

    private void UpdateScore()
    {
        scoreText.text = "Score: " + m_GameScore;
    }

    public void AddScore(int amount)
    {
        m_GameScore += amount;

        UpdateScore();
    }

    public void SubtractScore(int amount)
    {
        m_GameScore -= amount;

        UpdateScore();
    }

    public void ResetScore(int amount)
    {
        m_GameScore = 0;
    }
}
