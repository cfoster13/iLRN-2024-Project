using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintButtonBehaviour : MonoBehaviour
{
    public GameObject hintMessage;
    public void HintClicked()
    {
        hintMessage.SetActive(true);
    }
}
