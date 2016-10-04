using UnityEngine;
using System.Collections;

public enum DialogPageImage { Andrei, AndreiAngry, Lissu, Inspector, Martta }; 

public class DialogPage
{
    private string text;
    private string[] reply = new string[4];
    private DialogPage[] reply_target = new DialogPage[4];
    private bool is_last;
    private string done_text;
    private double reward;
    private bool is_gameover;
    private DialogPageImage image;
    private string audio;

    public DialogPage(DialogPageImage image, string audio)
    {
        for (int i=0; i < 4; i++)
        {
            reply[i] = "REPLY";
            reply_target[i] = null;
        }

        text = "Placeholder dialog";
        done_text = "DONE";
        is_last = false;
        is_gameover = false;
        reward = 0;
        this.image = image;
        this.audio = audio;
    }

    public string GetText()
    {
        return this.text;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public string GetReplyText(int index)
    {
        return reply[index];
    }

    public string GetDoneText()
    {
        return done_text;
    }

    public DialogPage GetReplyTarget(int index)
    {
        return reply_target[index];
    }

    public void SetReply(int index, string text, DialogPage target)
    {
        reply[index] = text;
        reply_target[index] = target;
    }

    public bool IsLast()
    {
        return is_last;
    }

    public void SetLast(string text)
    {
        is_last = true;
        done_text = text;
    }

    public void SetReward(double amount)
    {
        reward = amount;
    }

    public double GetReward()
    {
        return reward;
    }

    public bool IsGameOver()
    {
        return is_gameover;
    }

    public void SetGameOver()
    {
        is_gameover = true;
    }

    public DialogPageImage GetImage()
    {
        return image;
    }

    public string GetAudio()
    {
        return audio;
    }
}
