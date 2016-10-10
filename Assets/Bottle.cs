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

    /// <summary>
    /// Sets the visibility of this bottle
    /// </summary>
    /// <param name="status">True if visible, otherwise false.</param>
    public void SetVisible(bool status)
    {
        visible = status;
    }

    /// <summary>
    /// Returns the visibility of this bottle
    /// </summary>
    /// <returns>True if visible</returns>
    public bool IsVisible()
    {
        return visible;
    }

    /// <summary>
    /// Returns the cash value of this bottle
    /// </summary>
    /// <returns>Cash value of the bottle as double</returns>
    public double GetCash()
    {
        return cash;
    }
}
