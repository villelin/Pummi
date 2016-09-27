using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : IInteractiveObject
{
    private bool talked_to;
    protected int reward_cash;

    protected DialogPage current_dialog;

    public NPC()
    {
        this.talked_to = false;

        current_dialog = null;
    }


    public DialogPage GetCurrentDialog()
    {
        return current_dialog;
    }

    public void AdvanceDialog(DialogPage target)
    {
        current_dialog = target;
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
