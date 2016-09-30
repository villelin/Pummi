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
    GameObject reply4_button;
    GameObject done_button;

	// Use this for initialization
	void Start ()
    {
        reply1_button = GameObject.Find("ReplyButton1");
        reply1_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton1Clicked(); });

        reply2_button = GameObject.Find("ReplyButton2");
        reply2_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton2Clicked(); });

        reply3_button = GameObject.Find("ReplyButton3");
        reply3_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton3Clicked(); });

        reply4_button = GameObject.Find("ReplyButton4");
        reply4_button.GetComponent<Button>().onClick.AddListener(() => { ReplyButton4Clicked(); });

        done_button = GameObject.Find("DoneButton");
        done_button.GetComponent<Button>().onClick.AddListener(() => { DoneButtonClicked(); });

        conversation_target = Persistence.instance.conversation_target;

        GameObject lissu_image = GameObject.Find("LissuImage");
        lissu_image.SetActive(conversation_target.GetType() == typeof(Lissu));
        GameObject andrei_image = GameObject.Find("AndreiImage");
        andrei_image.SetActive(conversation_target.GetType() == typeof(Andrei));

        if (conversation_target != null)
        {
            SetDialog(conversation_target.GetCurrentDialog());
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    void SetDialog(DialogPage page)
    {
        Text convtext = GameObject.Find("ConversationText").GetComponent<Text>();

        string intro = Persistence.instance.conversation_target.GetCurrentDialog().GetText();
        convtext.text = page.GetText();

        reply1_button.SetActive(false);
        reply2_button.SetActive(false);
        reply3_button.SetActive(false);
        reply4_button.SetActive(false);

        if (page.GetReplyTarget(0) != null)
        {
            reply1_button.SetActive(true);
            GameObject.Find("ReplyText1").GetComponent<Text>().text = page.GetReplyText(0);
        }
        if (page.GetReplyTarget(1) != null)
        {
            reply2_button.SetActive(true);
            GameObject.Find("ReplyText2").GetComponent<Text>().text = page.GetReplyText(1);
        }
        if (page.GetReplyTarget(2) != null)
        {
            reply3_button.SetActive(true);
            GameObject.Find("ReplyText3").GetComponent<Text>().text = page.GetReplyText(2);
        }
        if (page.GetReplyTarget(3) != null)
        {
            reply4_button.SetActive(true);
            GameObject.Find("ReplyText4").GetComponent<Text>().text = page.GetReplyText(3);
        }

        done_button.SetActive(page.IsLast());

        if (page.GetReward() != 0)
        {
            // TODO: animation
            Persistence.instance.player.AddCash(page.GetReward());
        }
    }

    private void Answer(int answer)
    {
        DialogPage target = conversation_target.GetCurrentDialog().GetReplyTarget(answer);
        conversation_target.AdvanceDialog(target);

        SetDialog(target);
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

    void ReplyButton4Clicked()
    {
        Answer(3);
    }

    void DoneButtonClicked()
    {
        // end this scene
        SceneManager.LoadScene("basescene");
    }
}
