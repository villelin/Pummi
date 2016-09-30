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
    public NPC conversation_target;
    
    public static Persistence instance;

    void Awake()
    {
        if (!instance)
        {
            NewGame();

            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        player = new Player();
        station = new Location();
        park = new Location();

        iobjects = new Dictionary<string, IInteractiveObject>();
        iobjects.Add("Andrei", new Andrei());
        iobjects.Add("Lissu", new Lissu());
        iobjects.Add("Bush", new Bush());
        iobjects.Add("Pullo1", new Bottle());
        iobjects.Add("Pullo2", new Bottle());
        iobjects.Add("Pullo3", new Bottle());
        iobjects.Add("Pullo4", new Bottle());
        iobjects.Add("Pullo5", new Bottle());
        iobjects.Add("Pullo6", new Bottle());
        iobjects.Add("Pullo7", new Bottle());
        iobjects.Add("Metro", new Metro());
        iobjects.Add("ES", new ES());

        station.AddObject(iobjects["Andrei"]);
        station.AddObject(iobjects["Lissu"]);
        station.AddObject(iobjects["Metro"]);
        station.AddObject(iobjects["ES"]);

        park.AddObject(iobjects["Bush"]);
        park.AddObject(iobjects["Pullo1"]);
        park.AddObject(iobjects["Pullo2"]);
        park.AddObject(iobjects["Pullo3"]);
        park.AddObject(iobjects["Pullo4"]);
        park.AddObject(iobjects["Pullo5"]);
        park.AddObject(iobjects["Pullo6"]);
        park.AddObject(iobjects["Pullo7"]);
        park.AddObject(iobjects["ES"]);

        conversation_target = null;
    }
}
