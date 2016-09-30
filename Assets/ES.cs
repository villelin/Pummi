using UnityEngine;
using System.Collections;

public class ES : IInteractiveObject
{
    public ES()
    {

    }

    public int Interact(int param)
    {
        return 0;
    }

    public InteractType GetInteractType()
    {
        return InteractType.ES;
    }
}
