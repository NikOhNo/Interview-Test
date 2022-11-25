using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PacMan player = other.gameObject.GetComponent<PacMan>();
            player.BeginEating();

            // Update Score
            Destroy(gameObject);
        }
    }
}
