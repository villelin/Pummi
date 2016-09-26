using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
    private int cash;
    private int location;
    private Vector2 position;

    public Player()
    {
        cash = 0;
        position = new Vector2(400, 100);
    }

    public void AddCash(int amount)
    {
        cash += amount;
    }

    public int GetCash()
    {
        return cash;
    }

    public void SetLocation(int new_location)
    {
        location = new_location;
    }

    public int GetLocation()
    {
        return location;
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }

    public Vector2 GetPosition()
    {
        return position;
    }
}
