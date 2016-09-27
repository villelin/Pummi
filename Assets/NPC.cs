using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : IInteractiveObject
{
    private bool talked_to;
    protected string intro_conversation;
    protected List<string> answers;
    protected int correct_answer;
    protected int reward_cash;

    public NPC()
    {
        this.talked_to = false;
        this.answers = new List<string>();
        this.correct_answer = 0;

        this.intro_conversation = "INTRO CONVERSATION";
    }


    public string GetIntroConversation()
    {
        return this.intro_conversation;
    }

    public string GetAnswer(int number)
    {
        return this.answers[number];
    }

    public int GetCorrectAnswer()
    {
        return correct_answer;
    }

    public int GetRewardCash()
    {
        return reward_cash;
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
