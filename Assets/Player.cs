using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
    private double cash;
    private Location location;
    private Vector2 position;
    private bool es_buff;

    public Player()
    {
        cash = 0;
        position = new Vector2(400, 100);
        es_buff = false;
    }

    /// <summary>
    /// Adds given amount of cash to the player. Player cash is set to zero if it would ever go negative.
    /// </summary>
    /// <param name="amount">Amount of cash to add</param>
    public void AddCash(double amount)
    {
        cash += amount;
        if (cash < 0.0)
            cash = 0.0;
    }

    /// <summary>
    /// Returns the amount of cash the player has
    /// </summary>
    /// <returns>Amount of cash</returns>
    public double GetCash()
    {
        return cash;
    }

    /// <summary>
    /// Sets a new location for the player
    /// </summary>
    /// <param name="new_location">New location</param>
    public void SetLocation(Location new_location)
    {
        location = new_location;
    }

    /// <summary>
    /// Returns the location of the player
    /// </summary>
    /// <returns></returns>
    public Location GetLocation()
    {
        return location;
    }

    /// <summary>
    /// Sets the position of the player
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }

    /// <summary>
    /// Returns the position of the player
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPosition()
    {
        return position;
    }

    /// <summary>
    /// Gives the player ES buff
    /// </summary>
    public void SetESBuff()
    {
        es_buff = true;
    }

    /// <summary>
    /// Return true if the player has ES buff
    /// </summary>
    /// <returns>True if player has ES buff</returns>
    public bool HasESBuff()
    {
        return es_buff;
    }
}
