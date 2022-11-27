using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PacMan : MonoBehaviour
{
    [SerializeField]
    float eatTime = 4f;
    [SerializeField]
    Slider eatTimeSlider;
    [SerializeField]
    Image eatFilter;
    [SerializeField]
    AudioSource eatSong;
    [SerializeField]
    AudioSource eatNoise;

    float m_EatTimeLeft = 0f;

    bool m_Eating = false;
    bool m_IsInvincible = false;
    bool m_CanEat = false; 

    private void OnCollisionEnter(Collision collision)
    {
        if (m_CanEat && collision.gameObject.tag == "Ghost")
        {
            EatGhost(collision.gameObject);
        }
    }

    public void BeginEating()
    {
        m_Eating = true;
        m_EatTimeLeft = eatTime;

        m_IsInvincible = true;
        m_CanEat = true;

        eatFilter.enabled = true;

        // Play Music
        eatSong.Play();

        // Ghosts edible
        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        foreach (Ghost g in ghosts)
        {
            g.BecomeEdible();
        }
    }

    private void StopEating()
    {
        m_Eating = false;

        m_IsInvincible = false;
        m_CanEat = false;

        eatFilter.enabled = false;

        // Stop Music
        eatSong.Stop();

        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        foreach (Ghost g in ghosts)
        {
            g.StopEdible();
        }
    }

    private void Update()
    {
        m_EatTimeLeft -= Time.deltaTime;
        eatTimeSlider.value = m_EatTimeLeft / eatTime;

        if (m_Eating && m_EatTimeLeft <= 0)
        {
            m_EatTimeLeft = 0f;
            StopEating();
        }
    }

    public void EatGhost(GameObject ghost)
    {
        Destroy(ghost.gameObject);

        eatNoise.PlayOneShot(eatNoise.clip);

        // Update Score
        FindObjectOfType<ScoreManager>().AddScore(Ghost.scoreValue);
    }

    public bool GetIsInvincible()
    {
        return m_IsInvincible;
    }

    public float GetEatTimeLeft()
    {
        return m_EatTimeLeft;
    }
}
