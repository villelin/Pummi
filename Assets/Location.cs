using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location
{
    private List<IInteractiveObject> iobjects;

    public Location()
    {
        iobjects = new List<IInteractiveObject>();
    }

    public void AddObject(IInteractiveObject new_object)
    {
        iobjects.Add(new_object);
    }

    public List<IInteractiveObject> GetObjects()
    {
        return iobjects;
    }
}
