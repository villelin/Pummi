using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
    private List<GameItem> inventory;

    public Player()
    {
        inventory = new List<GameItem>();
    }

    public void Loot(GameItem item)
    {
        Debug.Log("Looted " + item.GetName());
        inventory.Add(item);
    }

    public string GetInventory()
    {
        string inv = "Inventory:\n";
        foreach (GameItem item in inventory)
        {
            inv += item.GetName() + "\n";
        }
        return inv;
    }

    public int GetCash()
    {
        int cash = 0;
        foreach (GameItem item in inventory)
        {
            cash += item.GetCash();
        }
        return cash;
    }
}
