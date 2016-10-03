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

    GameObject andrei_image;
    GameObject andrei_angry_image;
    GameObject lissu_image;
    GameObject inspector_image;
    GameObject martta_image;

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

        lissu_image = GameObject.Find("LissuImage");
        andrei_image = GameObject.Find("AndreiImage");
        andrei_angry_image = GameObject.Find("AndreiAngryImage");
        inspector_image = GameObject.Find("InspectorImage");
        martta_image = GameObject.Find("MarttaImage");

        conversation_target = Persistence.instance.conversation_target;

        if (conversation_target != null)
        {        
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

        // show or hide reply buttons based on the dialogue
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

        // hide all images by default
        lissu_image.SetActive(false);
        andrei_image.SetActive(false);
        andrei_angry_image.SetActive(false);
        inspector_image.SetActive(false);
        martta_image.SetActive(false);

        // show image for this page
        DialogPageImage image = conversation_target.GetCurrentDialog().GetImage();
        switch (image)
        {
            case DialogPageImage.Andrei:
                andrei_image.SetActive(true);
                break;
            case DialogPageImage.AndreiAngry:
                andrei_angry_image.SetActive(true);
                break;
            case DialogPageImage.Lissu:
                lissu_image.SetActive(true);
                break;
            case DialogPageImage.Inspector:
                inspector_image.SetActive(true);
                break;
            case DialogPageImage.Martta:
                martta_image.SetActive(true);
                break;
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
