using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAppear : MonoBehaviour
{
    bool loadedButton;
    public GameObject continueButton;
    public GameObject timerBonusText;
    public GameObject wordBonusText;
    public GameObject timerPanel;
    public GameObject gameManager;

    GameAndLevelManager gameAndLevelManager;

    float wordBonusCounter = 0.0f;
    float timerBonusCounter = 0.0f;
    float pauseCounter = 0.0f;

    bool addedPointsTimer;

    bool addedPointsWord;

    // Start is called before the first frame update
    void Start()
    {
        gameAndLevelManager = gameManager.GetComponent<GameAndLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !loadedButton)
        {
            wordBonusCounter += Time.deltaTime;

            if (wordBonusCounter >= 1.0f)
            {
                wordBonusText.SetActive(true);

                if (!addedPointsWord)
                {
                    gameAndLevelManager.AddPoints(500);

                    addedPointsWord = true;
                }

                timerBonusCounter += Time.deltaTime;

                if (timerBonusCounter >= 0.7f)
                {
                    timerBonusText.SetActive(true);

                    if (!addedPointsTimer)
                    {
                        timerBonusText.GetComponent<Text>().text = "Timer Bonus: " + timerPanel.GetComponent<TimerBehaviour>().bonusPoints;

                        gameAndLevelManager.AddPoints(timerPanel.GetComponent<TimerBehaviour>().bonusPoints);

                        addedPointsTimer = true;
                    }

                    pauseCounter += Time.deltaTime;

                    if (pauseCounter >= 1.0f)
                    {
                        continueButton.SetActive(true);
                        pauseCounter = 0.0f;
                        timerBonusCounter = 0.0f;
                        wordBonusCounter = 0.0f;
                        loadedButton = true;
                        addedPointsTimer = false;
                        addedPointsWord = false;
                    }
                }
            }
            
        }
    }
}
