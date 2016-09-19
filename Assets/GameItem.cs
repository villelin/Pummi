using UnityEngine;
using System.Collections;

public class GameItem
{
    private string name;
    private int cash;

    public GameItem(string name)
    {
        this.name = name;
        cash = Random.Range(1, 50);
    }

    public string GetName() { return name; }
    public int GetCash() { return cash; }
}
