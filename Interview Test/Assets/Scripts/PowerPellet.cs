using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(m_AudioSource.clip, transform.position);

            PacMan player = other.gameObject.GetComponent<PacMan>();
            player.BeginEating();

            // Update Score
            Destroy(gameObject);
        }
    }
}
