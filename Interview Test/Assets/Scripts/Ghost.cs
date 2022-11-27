using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static int scoreValue = 2000;

    [SerializeField]
    SkinnedMeshRenderer meshRenderer;

    Color m_OriginalColor;

    private void Start()
    {
        m_OriginalColor = meshRenderer.material.color;
    }

    public void BecomeEdible()
    {
        meshRenderer.material.color = Color.blue;
    }
    public void StopEdible()
    {
        meshRenderer.material.color = m_OriginalColor;
    }
}
