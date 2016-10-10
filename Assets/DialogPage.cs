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
    private bool is_escape;
    private DialogPageImage image;
    private string audio;

    /// <summary>
    /// DialogPage constructor
    /// </summary>
    /// <param name="image">Image to use on this page.</param>
    /// <param name="audio">Audio file name to play on this page.</param>
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
        is_escape = false;
        reward = 0;
        this.image = image;
        this.audio = audio;
    }

    /// <summary>
    /// Returns the dialog text on this page
    /// </summary>
    /// <returns>Dialog text</returns>
    public string GetText()
    {
        return this.text;
    }

    /// <summary>
    /// Sets the dialog text on this page
    /// </summary>
    /// <param name="text">Dialog text</param>
    public void SetText(string text)
    {
        this.text = text;
    }

    /// <summary>
    /// Returns the reply text of the given index
    /// </summary>
    /// <param name="index">Index of the reply in range 0...3</param>
    /// <returns></returns>
    public string GetReplyText(int index)
    {
        return reply[index];
    }

    /// <summary>
    /// Returns the done button text in this page
    /// </summary>
    /// <returns></returns>
    public string GetDoneText()
    {
        return done_text;
    }

    /// <summary>
    /// Returns the dialog page of the given reply index
    /// </summary>
    /// <param name="index">Reply index in range 0...3</param>
    /// <returns>DialogPage of the indexed reply</returns>
    public DialogPage GetReplyTarget(int index)
    {
        return reply_target[index];
    }

    /// <summary>
    /// Sets the reply on the given index
    /// </summary>
    /// <param name="index">Index of the reply to set</param>
    /// <param name="text">Reply text</param>
    /// <param name="target">DialogPage that this index targets</param>
    public void SetReply(int index, string text, DialogPage target)
    {
        reply[index] = text;
        reply_target[index] = target;
    }

    /// <summary>
    /// Returns true if this the last page of the conversation
    /// </summary>
    /// <returns>True if this is the last page</returns>
    public bool IsLast()
    {
        return is_last;
    }

    /// <summary>
    /// Sets this page as the last page of the conversation
    /// </summary>
    /// <param name="text">Text to show on the done button</param>
    public void SetLast(string text)
    {
        is_last = true;
        done_text = text;
    }

    /// <summary>
    /// Sets this page to give a cash reward to the player. Can be negative to reduce player cash.
    /// </summary>
    /// <param name="amount">Amount of cash to add to the player</param>
    public void SetReward(double amount)
    {
        reward = amount;
    }

    /// <summary>
    /// Returns the amount of cash this page gives
    /// </summary>
    /// <returns>Amount of cash</returns>
    public double GetReward()
    {
        return reward;
    }

    /// <summary>
    /// Returns true if this page leads to game over.
    /// </summary>
    /// <returns>True if this page leads to game over</returns>
    public bool IsGameOver()
    {
        return is_gameover;
    }

    /// <summary>
    /// Sets this page to lead to game over
    /// </summary>
    public void SetGameOver()
    {
        is_gameover = true;
    }

    /// <summary>
    /// True if this page leads to the "Escape" mini game
    /// </summary>
    /// <returns></returns>
    public bool IsEscapeMiniGame()
    {
        return is_escape;
    }

    /// <summary>
    /// Sets this page to lead to the "Escape" mini game
    /// </summary>
    public void SetEscapeMiniGame()
    {
        is_escape = true;
    }

    /// <summary>
    /// Returns the image used on this page
    /// </summary>
    /// <returns>Image used on this page</returns>
    public DialogPageImage GetImage()
    {
        return image;
    }

    /// <summary>
    /// Returns the audio file name played on this page. Can be null if there's no audio.
    /// </summary>
    /// <returns>Audio file name</returns>
    public string GetAudio()
    {
        return audio;
    }
}
