using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : IInteractiveObject
{
    private bool talked_to;
    protected string name;

    protected DialogPage current_dialog;
    protected Dictionary<string, DialogPage> pages;

    /// <summary>
    /// Constructor for the NPC class
    /// </summary>
    /// <param name="name">Name of this NPC</param>
    public NPC(string name)
    {
        this.talked_to = false;

        current_dialog = null;
        this.name = name;

        pages = new Dictionary<string, DialogPage>();
    }

    /// <summary>
    /// Return the current dialog page
    /// </summary>
    /// <returns></returns>
    public DialogPage GetCurrentDialog()
    {
        return current_dialog;
    }

    /// <summary>
    /// Advances the dialog to this dialog page
    /// </summary>
    /// <param name="target">Next dialog page to show</param>
    public void AdvanceDialog(DialogPage target)
    {
        current_dialog = target;
    }


    // IInteractiveObject interface methods
    public virtual int Interact(int param)
    {
        Debug.Log("INTERACT!");
        talked_to = true;

        return 0;
    }

    public virtual InteractType GetInteractType()
    {
        if (talked_to)
            return InteractType.None;
        else
            return InteractType.NPC;
    }

    /// <summary>
    /// Returns the name of this NPC
    /// </summary>
    /// <returns>Name of the NPC</returns>
    public string GetName()
    {
        return name;
    }
}
