using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour
{
    public Text timerText;
    public GameObject gameManager;

    public Text bonusPointsText;

    float timer = 0.0f;
    float displayTimer = 0.0f;

    public int bonusPoints = 5000;
    // Update is called once per frame

    private void Start()
    {
        bonusPointsText.text = "Bonus Points: " + bonusPoints;
    }
    void Update()
    {
        if (gameManager.GetComponent<GameAndLevelManager>().inALevel)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                displayTimer += 1.0f;
                bonusPoints -= 50;
                bonusPointsText.text = "Bonus Points: " + bonusPoints;
                if (displayTimer < 10.0f)
                {
                    timerText.text = "TIMER: 0" + displayTimer.ToString();
                } else
                {
                    timerText.text = "TIMER: " + displayTimer.ToString();
                }
                
                timer = 0.0f;
            }
        }
    }

    public void ResetTimer()
    {
        timer = 0.0f;
        displayTimer = 0.0f;
        bonusPoints = 5000;
        bonusPointsText.text = "Bonus Points: " + bonusPoints;
        timerText.text = "TIMER: 00";
    }
}
