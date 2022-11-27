using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField]
    int scoreValue = 350;

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

            FindObjectOfType<ScoreManager>().AddScore(scoreValue);

            Destroy(gameObject);
        }
    }
}
