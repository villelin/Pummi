using UnityEngine;
using System.Collections;

public class Metro : IInteractiveObject
{
    public Metro()
    {

    }

    public int Interact(int param)
    {
        return 0;
    }

    public InteractType GetInteractType()
    {
        return InteractType.Metro;
    }
}
