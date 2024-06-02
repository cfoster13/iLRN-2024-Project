using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class GameAndLevelManager : MonoBehaviour
{
    public int level;

    [Space]

    public string matchingWord;

    char[,] letterMatrix = {
        {'A', 'B', 'C', 'D', 'E'},
        {'F', 'G', 'H', '#', 'K'},
        {'L', 'M', 'N', 'O', 'P'},
        {'Q', 'R', 'S', 'T', 'U'},
        {'V', 'W', 'X', 'Y', 'Z'} };

    List<char> letters = new List<char>();

    [Space]

    public GameObject normalBackground;
    GameObject backgroundImage;
    public List<GameObject> backgroundImages = new List<GameObject>();

    [Space]

    public GameObject submitButton;
    public GameObject Torches;
    public Sprite normalTorchImage;
    public Text messageText;
    public Text outcomeText;

    [Space]

    public GameObject hintBox;
    public Text hintText;

    [Space]

    public HealthSystem healthSystem;

    [Space]

    public Button iButton;
    public Button jButton;

    [Space]

    public GameObject levelComplete1;
    public GameObject levelComplete2;
    public GameObject levelComplete3;

    TorchBehaviour tb;

    List<GameObject> TorchesA = new List<GameObject>();
    List<GameObject> TorchesB = new List<GameObject>();

    int torchACount = 0;
    int torchBCount = 0;

    bool wordCompleted;
    bool backgroundIsShown;

    public bool inALevel;

    float pauseCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        tb = Torches.GetComponent<TorchBehaviour>();

        level = 1;

    }
    void Update()
    {
        Debug.Log(backgroundIsShown);

        //Set word
        if (level == 1)
        {
            matchingWord = "FIRE";
            backgroundImage = backgroundImages[0];
        }

        //Set word
        if (level == 2)
        {
            //WORD = SPARTA
            matchingWord = "SPARTA";
            backgroundImage = backgroundImages[1];
        }

        //Set word
        if (level == 3)
        {
            //WORD = OLYMPIA
            matchingWord = "OLYMPIA";
            backgroundImage = backgroundImages[2];
        }

        if (tb.torchesLoaded)
        {
            if (tb != null)
            {
                for (int i = 0; i <= 4; i++)
                {
                    TorchesA.Add(tb.torches[i]);
                }

                for (int i = 5; i <= 9; i++)
                {
                    TorchesB.Add(tb.torches[i]);
                }
            }
            tb.torchesLoaded = false;
        }

        if (wordCompleted)
        {
            inALevel = false;
            StartCoroutine(showPrometheus());
        } else
        {
            inALevel = true;
        }

        if (backgroundIsShown)
        {
            pauseCounter += Time.deltaTime;

            if (pauseCounter >= 2.0f)
            {
                if (level == 1)
                {
                    levelComplete1.SetActive(true);
                }

                if (level == 2)
                {
                    levelComplete2.SetActive(true);
                }

                if (level == 3)
                {
                    levelComplete3.SetActive(true);
                }

                pauseCounter = 0.0f;
                backgroundIsShown = false;
            }
        }
    }

    public void CountNumber()
    {
        int torchACount = 0;
        int torchBCount = 0;
        foreach (GameObject child in TorchesA)
        {
            if (child.GetComponent<TorchIsHit>().isLit)
            {
                torchACount++;
            }
        }

        foreach (GameObject child in TorchesB)
        {
            if (child.GetComponent<TorchIsHit>().isLit)
            {
                torchBCount++;
            }
        }

        if (torchBCount == 0 || torchACount == 0)
        {
            //TELL THEM TO LIGHT UP
            outcomeText.gameObject.SetActive(true);
            GameManager.Instance.PlayIncorrectAnswerSound();
            // Lose a life
            healthSystem.LoseHealth();
            Debug.Log("Light a torch!");
            return;
        }

        torchACount = (torchACount == 0) ? 0 : torchACount - 1;
        torchBCount = (torchBCount == 0) ? 0 : torchBCount - 1;

        DisplayLetter(torchACount, torchBCount);

        GameManager.Instance.PlaySubmitButtonClick();
    }

    void DisplayLetter(char letter)
    {
        iButton.gameObject.SetActive(false);
        jButton.gameObject.SetActive(false);

        letters.Add(letter);

        WriteLetterAndLookForWord();
    }

    void DisplayLetter(int countX, int countY)
    {
        char tempLetter = letterMatrix[countX, countY];

        //IF THE LETTER PICKED IS I OR J
        if (tempLetter.Equals('#'))
        {
            Debug.Log("PICK I OR J");
            //OPTION FOR I OR J
            PickLetterIorJ();
            return;
        }
        else
        {
            letters.Add(letterMatrix[countX, countY]);
            WriteLetterAndLookForWord();
        }
        ResetTorches();
    }
    void WriteLetterAndLookForWord()
    {
        string sentence = string.Empty;
        for (int i = 0; i < letters.Count; i++)
        {
            sentence += letters[i];
        }
        Debug.Log(sentence);
        messageText.text = "Input: " + sentence;

        //LOOK FOR THE WORD 'FIRE' FOR TUTORIAL
        if (sentence == matchingWord)
        {
            submitButton.SetActive(false);
            GameManager.Instance.PlayCorrectAnswerSound();
            backgroundImage.SetActive(true);
            wordCompleted = true;
        }

        ResetTorches();
    }

    void PickLetterIorJ()
    {
        iButton.gameObject.SetActive(true);
        jButton.gameObject.SetActive(true);
    }

    

    public void ResetTorches()
    {
        foreach (GameObject child in tb.torches)
        {
            child.GetComponent<SpriteRenderer>().sprite = normalTorchImage;
            child.GetComponent<TorchIsHit>().isLit = false;

            // Reset the fire particles
            Transform fireParticles = child.transform.Find("Flame Particle System(Clone)");
            if (fireParticles != null)
            {
                Destroy(fireParticles.gameObject);
            }
        }
        // Play reset button sound effect
        GameManager.Instance.PlayResetButtonClick();

        outcomeText.gameObject.SetActive(false);
    }

    public void PickI()
    {
        DisplayLetter('I');
    }

    public void PickJ()
    {
        DisplayLetter('J');
    }

    public void HintButtonPressed()
    {
        if (level == 1)
        {
            hintText.text = "I am not alive, but I grow.\r\nI don't have lungs, but I need air.\r\nI don't have a mouth, but water kills me.\r\nWhat am I?";
        }
        if (level == 2)
        {
            hintText.text = "In ancient Greece, a city strong;\r\nwhere warriors trained to fight lifelong;\r\nbrave and fierce, their name's well known;\r\nin battles, they have always shone.\r\nWhat is this city's name in part?";
        }
        if (level == 3)
        {
            hintText.text = "I am a place of gods on high;\r\nwhere Zeus and Hera rule the sky;\r\nmy peak is known, a mythic dome;\r\nin Ancient Greece, I was their home.\r\nWhat am I?";
        }

        Debug.Log(level);
        hintBox.SetActive(true);
    }

    public void ResetMessageText() // Reset the text when user clicks on button
    {
        messageText.text = string.Empty;
        letters.Clear();
        messageText.text = "Input: ";
        GameManager.Instance.PlayResetButtonClick();
        
    }

    public IEnumerator showPrometheus()
    {
        while (backgroundImage.GetComponent<SpriteRenderer>().color.a < 3)
        {
            var normalColour = normalBackground.GetComponent<SpriteRenderer>().color;
            normalColour.a -= 0.00001f;
            var prometheusColour = backgroundImage.GetComponent<SpriteRenderer>().color;
            prometheusColour.a += 0.00001f;

            normalBackground.GetComponent<SpriteRenderer>().color = normalColour;
            backgroundImage.GetComponent<SpriteRenderer>().color = prometheusColour;

            yield return new WaitForEndOfFrame();
        }

        backgroundIsShown = true;
    }

    //Continue to next level
    public void ContinueButtonPressed()
    {
        level++;

        backgroundIsShown = false;
        wordCompleted = false;
        inALevel = true;

        if (level == 2)
        {
            levelComplete1.SetActive(false);

        }

        if (level == 3)
        {
            levelComplete2.SetActive(false);

        }

        if (level == 4)
        {
            //FINISH THE GAME
            return;
        }

        //RESETTING BACKGROUND
        var resettingTransparencyNormal = normalBackground.GetComponent<SpriteRenderer>().color;
        resettingTransparencyNormal.a = 0.63921568627450980392156862745098f;

        normalBackground.GetComponent<SpriteRenderer>().color = resettingTransparencyNormal;

        var resettingTransparencyPrometheus = backgroundImage.GetComponent<SpriteRenderer>().color;
        resettingTransparencyPrometheus.a = 0;

        backgroundImage.GetComponent<SpriteRenderer>().color = resettingTransparencyPrometheus;

        submitButton.SetActive(true);

        ResetTorches();
        ResetMessageText();
    }

}
