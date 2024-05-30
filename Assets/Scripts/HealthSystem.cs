using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public string gameOverSceneName;
    public AudioClip gameOverMusicClip;

    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        UpdateHearts();
    }

    public void LoseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void GameOver()
    {
        audioSource.clip = gameOverMusicClip;
        audioSource.Play();

        

        // Load game over screen after a short delay to let the game over music play
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay as needed
        SceneManager.LoadScene("GameOver");
    }
}
