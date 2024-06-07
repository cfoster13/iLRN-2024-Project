using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    GameAndLevelManager gameManagerScript;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameAndLevelManager>();

        if (gameManagerScript == null)
        {
            return;
        }

        points = gameManagerScript.points;
    }
}
