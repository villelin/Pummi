using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location
{
    private List<IInteractiveObject> iobjects;
    private Location left_exit;
    private Location right_exit;
    private int background;

    /// <summary>
    /// Constructor for Location
    /// </summary>
    /// <param name="background">Index of the image to use as background</param>
    public Location(int background)
    {
        iobjects = new List<IInteractiveObject>();

        this.left_exit = null;
        this.right_exit = null;

        this.background = background;
    }

    /// <summary>
    /// Adds a IInteractiveObject to this Location
    /// </summary>
    /// <param name="new_object">Object to add to this Location</param>
    public void AddObject(IInteractiveObject new_object)
    {
        iobjects.Add(new_object);
    }

    /// <summary>
    /// Returns a List of objects in this Location
    /// </summary>
    /// <returns></returns>
    public List<IInteractiveObject> GetObjects()
    {
        return iobjects;
    }

    /// <summary>
    /// Sets the Location that is connected to the right side of this location
    /// </summary>
    /// <param name="exit">Location on the right side</param>
    public void SetRightExit(Location exit)
    {
        right_exit = exit;
    }

    /// <summary>
    /// Sets the Location that is connected to the left side of the location
    /// </summary>
    /// <param name="exit">Location on the left side</param>
    public void SetLeftExit(Location exit)
    {
        left_exit = exit;
    }

    /// <summary>
    /// Returns the Location that is connected to the right side of this Location
    /// </summary>
    /// <returns>Location on the right side</returns>
    public Location GetRightExit()
    {
        return right_exit;
    }

    /// <summary>
    /// Returns the Location that is connected to the left side of this location
    /// </summary>
    /// <returns>Location on the left side</returns>
    public Location GetLeftExit()
    {
        return left_exit;
    }

    /// <summary>
    /// Returns the index of image used as background
    /// </summary>
    /// <returns>Index of the background image</returns>
    public int GetBackground()
    {
        return background;
    }
}
