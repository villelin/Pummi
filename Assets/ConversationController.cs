using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ConversationController : MonoBehaviour
{
    NPC conversation_target;
    GameObject done_button;
    GameObject[] reply_button = new GameObject[4];
    Text[] reply_text = new Text[4];

	// Use this for initialization
	void Start ()
    {
        reply_button[0] = GameObject.Find("ReplyButton1");
        reply_button[0].GetComponent<Button>().onClick.AddListener(() => { ReplyButton1Clicked(); });
        reply_text[0] = GameObject.Find("ReplyText1").GetComponent<Text>();

        reply_button[1] = GameObject.Find("ReplyButton2");
        reply_button[1].GetComponent<Button>().onClick.AddListener(() => { ReplyButton2Clicked(); });
        reply_text[1] = GameObject.Find("ReplyText2").GetComponent<Text>();

        reply_button[2] = GameObject.Find("ReplyButton3");
        reply_button[2].GetComponent<Button>().onClick.AddListener(() => { ReplyButton3Clicked(); });
        reply_text[2] = GameObject.Find("ReplyText3").GetComponent<Text>();

        reply_button[3] = GameObject.Find("ReplyButton4");
        reply_button[3].GetComponent<Button>().onClick.AddListener(() => { ReplyButton4Clicked(); });
        reply_text[3] = GameObject.Find("ReplyText4").GetComponent<Text>();

        done_button = GameObject.Find("DoneButton");
        done_button.GetComponent<Button>().onClick.AddListener(() => { DoneButtonClicked(); });

        conversation_target = Persistence.instance.conversation_target;

        if (conversation_target != null)
        {

            GameObject lissu_image = GameObject.Find("LissuImage");
            lissu_image.SetActive(conversation_target.GetType() == typeof(Lissu));
            GameObject andrei_image = GameObject.Find("AndreiImage");
            andrei_image.SetActive(conversation_target.GetType() == typeof(Andrei));
            GameObject inspector_image = GameObject.Find("InspectorImage");
            inspector_image.SetActive(conversation_target.GetType() == typeof(Inspector));

        
            SetDialog(conversation_target.GetCurrentDialog());

            Text char_name = GameObject.Find("CharacterName").GetComponent<Text>();
            char_name.text = conversation_target.GetName();
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

        for (int i=0; i < 4; i++)
        {
            DialogPage target = page.GetReplyTarget(i);
            if (target != null)
            {
                reply_button[i].SetActive(true);
                reply_text[i].text = page.GetReplyText(i);
            }
            else
            {
                reply_button[i].SetActive(false);
                reply_text[i].text = "";
            }
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
        if (conversation_target.GetCurrentDialog().IsGameOver())
        {
            SceneManager.LoadScene("losescreen");
        }
        else
        {
            // go back to main scene
            SceneManager.LoadScene("basescene");
        }
    }
}
