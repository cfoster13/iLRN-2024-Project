using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour
{
    public Text timerText;
    public GameObject gameManager;

    float timer = 0.0f;
    float displayTimer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameAndLevelManager>().inALevel)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                displayTimer += 1.0f;
                timerText.text = "TIME: " + displayTimer.ToString();
                timer = 0.0f;
            }
        }
    }
}
