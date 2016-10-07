using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : IInteractiveObject
{
    private bool talked_to;
    protected string name;

    protected DialogPage current_dialog;
    protected Dictionary<string, DialogPage> pages;

    public NPC(string name)
    {
        this.talked_to = false;

        current_dialog = null;
        this.name = name;

        pages = new Dictionary<string, DialogPage>();
    }


    public DialogPage GetCurrentDialog()
    {
        return current_dialog;
    }

    public void AdvanceDialog(DialogPage target)
    {
        current_dialog = target;
    }


    // IInteractiveObject interface methods
    public int Interact(int param)
    {
        Debug.Log("INTERACT!");
        talked_to = true;

        return 0;
    }

    public InteractType GetInteractType()
    {
        if (talked_to)
            return InteractType.None;
        else
            return InteractType.NPC;
    }

    public string GetName()
    {
        return name;
    }
}
