using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class infoButton : MonoBehaviour
{
    public Button helpButton;
    public Image helpInfo;
    public GameObject rowPanel;
    public GameObject colPanel;
    

    bool helpInfoDisplayed = false;

    void Start()
    {
        helpButton.onClick.AddListener(onHelpClick);
    }

    
    void Update()
    {
        
    }

    void onHelpClick()
    {
        if (helpInfoDisplayed == false)
        {
            
            helpInfo.gameObject.SetActive(true);
            rowPanel.SetActive(true);
            colPanel.SetActive(true);
            helpInfoDisplayed = true;
        }
        else
        {
            helpInfo.gameObject.SetActive(false);
            rowPanel.SetActive(false);
            colPanel.SetActive(false);
            helpInfoDisplayed = false;
        }

        // Play the help button click sound effect
        GameManager.Instance.PlayHelpButtonClick();

    }
}
