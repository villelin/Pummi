﻿using UnityEngine;
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

    public InteractType GetInteractType()
    {
        return InteractType.Lootable;
    }
}
