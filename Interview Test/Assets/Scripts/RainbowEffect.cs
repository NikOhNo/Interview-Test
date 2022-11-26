using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowEffect : MonoBehaviour
{
    [SerializeField]
    Color[] colors;

    [SerializeField]
    float timeToChange = 1f;

    int m_CurrentColor = 0;
    int m_NextColor;
    float m_TimeElapsed = 0f;
    MeshRenderer m_MeshRenderer;  

    void Start()
    {
        m_NextColor = m_CurrentColor + 1;

        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        m_TimeElapsed += Time.deltaTime;
        if (m_TimeElapsed >= timeToChange)
        {
            m_CurrentColor = (m_CurrentColor + 1) % colors.Length;
            m_NextColor = (m_CurrentColor + 1) % colors.Length;

            m_TimeElapsed = 0f;
        }

        Color newColor = Color.Lerp(colors[m_CurrentColor], colors[m_NextColor], m_TimeElapsed / timeToChange);
        m_MeshRenderer.material.color = newColor;
    }
}
