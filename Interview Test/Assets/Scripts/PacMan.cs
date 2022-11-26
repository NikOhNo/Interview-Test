using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    [SerializeField]
    float eatTime = 4f;
    [SerializeField]
    AudioSource eatSong;
    [SerializeField]
    AudioSource eatNoise;

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
        StartCoroutine(Eating());
    }

    public IEnumerator Eating()
    {
        m_IsInvincible = true;
        m_CanEat = true;

        // Play Music
        eatSong.Play();

        // Ghosts edible
        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        foreach (Ghost g in ghosts)
        {
            g.BecomeEdible();
        }

        yield return new WaitForSeconds(eatTime);

        // Ghosts not edible
        foreach (Ghost g in ghosts)
        {
            if (g != null)      // have to check here since ghost might be eaten and destroyed
            {
                g.StopEdible();
            }
        }

        // Stop Music
        eatSong.Stop();

        m_IsInvincible = false;
        m_CanEat = false;
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
}
