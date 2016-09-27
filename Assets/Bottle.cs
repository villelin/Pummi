using UnityEngine;
using System.Collections;

public class Bottle : IInteractiveObject
{
    public Bottle()
    {

    }

    public int Interact(int param)
    {
        return 0;
    }

    public bool CanTalk()
    {
        return false;
    }

    public bool CanLoot()
    {
        return true;
    }
}
