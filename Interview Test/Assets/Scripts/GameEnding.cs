using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField]
    int winningScore = 20000;

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;

    public CanvasGroup introCanvas;
    public CanvasGroup warningCanvas;
    public float messageDuration = 2f;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_PlayerWon;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    private void Start()
    {
        StartCoroutine(DisplayMessage(introCanvas, 0f, fadeDuration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            int currentScore = scoreManager.GetScore();

            if (currentScore >= winningScore)
            {
                m_PlayerWon = true;
            }
            else
            {
                StartCoroutine(DisplayMessage(warningCanvas, fadeDuration, fadeDuration));
            }
        }
    }

    public void CaughtPlayer()
    {
        bool playerIsInvincible = player.GetComponent<PacMan>().GetIsInvincible();
        if (!playerIsInvincible)
        {
            m_IsPlayerCaught = true;
        }
    }

    private void Update()
    {
        if (m_PlayerWon)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > (fadeDuration + displayImageDuration))
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();

            }
        }
    }

    IEnumerator DisplayMessage(CanvasGroup messageCanvasGroup, float fadeInTime, float fadeOutTime)
    {
        float timeElapsed = 0f;

        // Fade In
        while (timeElapsed < fadeInTime)
        {
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);

            messageCanvasGroup.alpha = timeElapsed / fadeDuration;

            yield return null;
        }
        messageCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(messageDuration);

        // Fade Out
        timeElapsed = 0f;
        while (timeElapsed < fadeOutTime)
        {
            timeElapsed += Time.deltaTime;

            messageCanvasGroup.alpha = 1 - (timeElapsed / fadeDuration);

            yield return null;
        }
    }
}
