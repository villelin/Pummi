using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// global singleton class for holding info across scenes
public class Persistence : MonoBehaviour
{
    public Player player;
    public Dictionary<string, IInteractiveObject> iobjects;
    public Location park;
    public Location station;
    
    public static Persistence instance;

    void Awake()
    {
        if (!instance)
        {
            player = new Player();
            station = new Location();
            park = new Location();

            iobjects = new Dictionary<string, IInteractiveObject>();
            iobjects.Add("Andrei",  new NPC());
            iobjects.Add("Lissu", new NPC());
            iobjects.Add("Bush", new NPC());
            iobjects.Add("Pullo1", new NPC());
            iobjects.Add("Pullo2", new NPC());
            iobjects.Add("Pullo3", new NPC());
            iobjects.Add("Pullo4", new NPC());
            iobjects.Add("Pullo5", new NPC());
            iobjects.Add("Pullo6", new NPC());
            iobjects.Add("Pullo7", new NPC());
            iobjects.Add("Metro", new NPC());

            station.AddObject(iobjects["Andrei"]);
            station.AddObject(iobjects["Lissu"]);
            station.AddObject(iobjects["Metro"]);

            park.AddObject(iobjects["Bush"]);
            park.AddObject(iobjects["Pullo1"]);
            park.AddObject(iobjects["Pullo2"]);
            park.AddObject(iobjects["Pullo3"]);
            park.AddObject(iobjects["Pullo4"]);
            park.AddObject(iobjects["Pullo5"]);
            park.AddObject(iobjects["Pullo6"]);
            park.AddObject(iobjects["Pullo7"]);

            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
