using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubmitLetter : MonoBehaviour
{
    char[,] letterMatrix = {
        {'A', 'B', 'C', 'D', 'E'},
        {'F', 'G', 'H', '#', 'K'},
        {'L', 'M', 'N', 'O', 'P'},
        {'Q', 'R', 'S', 'T', 'U'},
        {'V', 'W', 'X', 'Y', 'Z'} };

    List<char> letters = new List<char>();

    public GameObject Torches;
    public Sprite normalTorchImage;
    public Text messageText;
    public Text outcomeText;
    TorchBehaviour tb;

    


    List<GameObject> TorchesA = new List<GameObject>();
    List<GameObject> TorchesB = new List<GameObject>();

    int torchACount = 0;
    int torchBCount = 0;

    public Button iButton;
    public Button jButton;

    // Start is called before the first frame update
    void Start()
    {
        tb = Torches.GetComponent<TorchBehaviour>();

    }
    void Update()
    {
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
        if (sentence == "FIRE")
        {
            outcomeText.text = "You have made the word FIRE!";
            outcomeText.gameObject.SetActive(true);
            GameManager.Instance.PlayCorrectAnswerSound();
            Debug.Log("You have made the word FIRE!");
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

    public void ResetMessageText() // Reset the text when user clicks on button
    {
        messageText.text = string.Empty;
        letters.Clear();
        messageText.text = "Input: ";
        GameManager.Instance.PlayResetButtonClick();
        
    }

}
