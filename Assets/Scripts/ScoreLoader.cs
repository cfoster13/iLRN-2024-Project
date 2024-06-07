using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour
{
    public Text scoreText;
    public GameObject scoreReceiver;

    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    public void Update()
    {
        scoreText.text = scoreReceiver.GetComponent<ScoreSaver>().points.ToString();
    }
}
