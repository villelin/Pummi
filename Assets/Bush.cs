using UnityEngine;
using System.Collections;

public class Bush : IInteractiveObject
{
    public Bush()
    {

    }

    public int Interact(int param)
    {
        return 0;
    }

    public InteractType GetInteractType()
    {
        return InteractType.Bush;
    }
}
