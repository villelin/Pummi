using UnityEngine;
using System.Collections;

public class Bottle : IInteractiveObject
{
    private bool visible;
    private double cash;

    public Bottle()
    {
        visible = true;
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

    public void SetCash(double cash)
    {
        this.cash = cash;
    }

    public double GetCash()
    {
        return cash;
    }
}
