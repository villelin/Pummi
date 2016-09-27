using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{
    NPC conversation_target;
    GameObject reply1_button;
    GameObject reply2_button;
    GameObject reply3_button;
    GameObject done_button;

	// Use this for initialization
	void Start ()
    {
        reply1_button = GameObject.Find("ReplyButton1");
        reply1_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton1Clicked(); });
        reply1_button.SetActive(true);

        reply2_button = GameObject.Find("ReplyButton2");
        reply2_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton2Clicked(); });
        reply2_button.SetActive(true);

        reply3_button = GameObject.Find("ReplyButton3");
        reply3_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton3Clicked(); });
        reply3_button.SetActive(true);

        done_button = GameObject.Find("DoneButton");
        done_button.GetComponent<Button>().onClick.AddListener(() => { DoneButtonClicked(); });
        done_button.SetActive(false);

        conversation_target = Persistence.instance.conversation_target;

        GameObject lissu_image = GameObject.Find("LissuImage");
        lissu_image.SetActive(conversation_target.GetType() == typeof(Lissu));
        GameObject andrei_image = GameObject.Find("AndreiImage");
        andrei_image.SetActive(conversation_target.GetType() == typeof(Andrei));

            if (conversation_target != null)
        {
            Text convtext = GameObject.Find("ConversationText").GetComponent<Text>();

            string intro = Persistence.instance.conversation_target.GetIntroConversation();

            convtext.text = intro;
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    private void Answer(int answer)
    {
        if (conversation_target.GetCorrectAnswer() == answer)
            Persistence.instance.player.AddCash(conversation_target.GetRewardCash());

        Text convtext = GameObject.Find("ConversationText").GetComponent<Text>();
        string answertext = Persistence.instance.conversation_target.GetAnswer(answer);

        convtext.text = answertext;

        // hide replies, show done button
        reply1_button.SetActive(false);
        reply2_button.SetActive(false);
        reply3_button.SetActive(false);
        done_button.SetActive(true);
    }

    void ReplyButton1Clicked()
    {
        Answer(0);
    }

    void ReplyButton2Clicked()
    {
        Answer(1);
    }

    void ReplyButton3Clicked()
    {
        Answer(2);
    }

    void DoneButtonClicked()
    {
        // end this scene
        SceneManager.LoadScene("basescene");
    }
}
