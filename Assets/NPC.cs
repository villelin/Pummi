using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : IInteractiveObject
{
    private bool talked_to;
    protected List<string> conversations;
    protected int correct_answer;

    public NPC()
    {
        this.talked_to = false;
        this.conversations = new List<string>();
        this.correct_answer = 0;

        conversations.Add("testi");
    }


    public List<string> GetConversations()
    {
        return this.conversations;
    }

    public int GetCorrectAnswer()
    {
        return correct_answer;
    }


    // IInteractiveObject interface methods
    public int Interact(int param)
    {
        Debug.Log("INTERACT!");
        talked_to = true;

        return 0;
    }

    public bool CanTalk()
    {
        return talked_to != true;
    }

    public bool CanLoot()
    {
        return false;
    }
}
