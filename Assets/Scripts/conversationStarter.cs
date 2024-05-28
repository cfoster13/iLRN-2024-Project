using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class conversationStarter : MonoBehaviour
{
    public NPCConversation conversation;
    public Button convoButton;
    // Start is called before the first frame update
    void Start()
    {
        convoButton.onClick.AddListener(convoStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void convoStart()
    {
        convoButton.gameObject.SetActive(false);
        ConversationManager.Instance.StartConversation(conversation);
        
    }
}
