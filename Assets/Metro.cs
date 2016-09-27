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

    public bool CanTalk()
    {
        return true;
    }

    public bool CanLoot()
    {
        return false;
    }
}
