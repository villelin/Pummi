using UnityEngine;
using System.Collections;

public class Bottle : IInteractiveObject
{
    private bool visible;
    private double cash;

    public Bottle(double cash)
    {
        visible = true;
        this.cash = cash;
    }

    public int Interact(int param)
    {
        return 0;
    }

    public InteractType GetInteractType()
    {
        return InteractType.Lootable;
    }

    public void SetVisible(bool status)
    {
        visible = status;
    }

    public bool IsVisible()
    {
        return visible;
    }

    public double GetCash()
    {
        return cash;
    }
}
