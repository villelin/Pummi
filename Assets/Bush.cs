using UnityEngine;
using System.Collections;

public class Bush : IInteractiveObject
{
    private bool looted;

    public Bush()
    {
        looted = false;
    }

    public int Interact(int param)
    {
        looted = true;

        return 0;
    }

    public InteractType GetInteractType()
    {
        if (looted)
            return InteractType.None;
        else
            return InteractType.Bush;
    }
}
