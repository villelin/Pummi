using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location
{
    private List<IInteractiveObject> iobjects;
    private Location left_exit;
    private Location right_exit;
    private int background;

    public Location(int background)
    {
        iobjects = new List<IInteractiveObject>();

        this.left_exit = null;
        this.right_exit = null;

        this.background = background;
    }

    public void AddObject(IInteractiveObject new_object)
    {
        iobjects.Add(new_object);
    }

    public List<IInteractiveObject> GetObjects()
    {
        return iobjects;
    }

    public void SetRightExit(Location exit)
    {
        right_exit = exit;
    }

    public void SetLeftExit(Location exit)
    {
        left_exit = exit;
    }

    public Location GetRightExit()
    {
        return right_exit;
    }

    public Location GetLeftExit()
    {
        return left_exit;
    }

    public int GetBackground()
    {
        return background;
    }
}
