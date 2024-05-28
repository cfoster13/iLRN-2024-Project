using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioSource backgroundMusic;
    public AudioClip backgroundMusicClip;

    public AudioSource buttonClickSound;
    public AudioClip helpButtonClickClip;
    public AudioClip resetButtonClickClip;
    public AudioClip submitButtonClickClip;
    public AudioClip incorrectSound;

    public AudioSource correctAnswerSound;
    public AudioClip correctAnswerClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize background music
        backgroundMusic.clip = backgroundMusicClip;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Start playing background music if not already playing
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void PlayHelpButtonClick()
    {
        buttonClickSound.PlayOneShot(helpButtonClickClip);
    }

    public void PlayResetButtonClick()
    {
        buttonClickSound.PlayOneShot(resetButtonClickClip);
    }

    public void PlaySubmitButtonClick()
    {
        buttonClickSound.PlayOneShot(submitButtonClickClip);
    }

    public void PlayCorrectAnswerSound()
    {
        correctAnswerSound.PlayOneShot(correctAnswerClip);
    }

    public void PlayIncorrectAnswerSound()
    {
        correctAnswerSound.PlayOneShot(incorrectSound);
    }
}
