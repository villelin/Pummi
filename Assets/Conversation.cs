using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Button sb = GameObject.Find("DoneButton").GetComponent<Button>();
        sb.onClick.AddListener(() => { DoneButtonClicked(); });

        if (Persistence.instance.conversation_target != null)
        {
            Text convtext = GameObject.Find("ConversationText").GetComponent<Text>();

            List<string> conversations = Persistence.instance.conversation_target.GetConversations();

            convtext.text = conversations[0];
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    void DoneButtonClicked()
    {
        Persistence.instance.player.AddCash(100);

        SceneManager.LoadScene("basescene");
    }
}
