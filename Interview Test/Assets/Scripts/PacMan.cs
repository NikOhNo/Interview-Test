using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    [SerializeField]
    float eatTime = 4f;
    [SerializeField]
    AudioSource eatingNoise;
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
        Debug.Log("Begin Eating");

        m_IsInvincible = true;
        m_CanEat = true;

        // Play Music


        // Ghosts edible
        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        foreach (Ghost g in ghosts)
        {
            g.BecomeEdible();
        }

        yield return new WaitForSeconds(eatTime);

        Debug.Log("Stop eating");

        // Ghosts not edible
        foreach (Ghost g in ghosts)
        {
            if (g != null)      // have to check here since ghost might be eaten and destroyed
            {
                g.StopEdible();
            }
        }

        // Stop Music


        m_IsInvincible = false;
        m_CanEat = false;
    }

    public void EatGhost(GameObject ghost)
    {
        Destroy(ghost.gameObject);

        // Update Score
        
    }

    public bool GetIsInvincible()
    {
        return m_IsInvincible;
    }
}
