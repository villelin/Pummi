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
        iobjects.Add("Can1", new Bottle(0.1));
        iobjects.Add("Can2", new Bottle(0.1));
        iobjects.Add("Can3", new Bottle(0.1));
        iobjects.Add("Can4", new Bottle(0.1));
        iobjects.Add("Can5", new Bottle(0.1));
        iobjects.Add("Can6", new Bottle(0.1));
        iobjects.Add("Can7", new Bottle(0.1));
        iobjects.Add("Metro", new Metro());
        iobjects.Add("ES", new ES());
        iobjects.Add("Bottle1", new Bottle(0.2));
        iobjects.Add("Bottle2", new Bottle(0.2));
        iobjects.Add("Bottle3", new Bottle(0.2));
        iobjects.Add("Bottle4", new Bottle(0.2));
        iobjects.Add("Inspector", new Inspector());
        iobjects.Add("Bag1", new Bottle(5.0));
        iobjects.Add("Bag2", new Bottle(5.0));
        iobjects.Add("Bag3", new Bottle(5.0));
        iobjects.Add("Bag4", new Bottle(5.0));
        iobjects.Add("Bag5", new Bottle(5.0));
        iobjects.Add("Bag6", new Bottle(5.0));
        iobjects.Add("Bag7", new Bottle(5.0));

        // set spawnable bottles as invisible to begin with
        ((Bottle)iobjects["Bottle1"]).SetVisible(false);
        ((Bottle)iobjects["Bottle2"]).SetVisible(false);
        ((Bottle)iobjects["Bottle3"]).SetVisible(false);
        ((Bottle)iobjects["Bottle4"]).SetVisible(false);

        // randomly choose either can or bag, for a total of 7 items
        bool[] bottle_sizes = new bool[7];
        for (int i=0; i < 7; i++)
        {
            bottle_sizes[i] = Random.Range(0, 100) < 80 ? true : false;
        }
        ((Bottle)iobjects["Can1"]).SetVisible(bottle_sizes[0] == true);
        ((Bottle)iobjects["Can2"]).SetVisible(bottle_sizes[1] == true);
        ((Bottle)iobjects["Can3"]).SetVisible(bottle_sizes[2] == true);
        ((Bottle)iobjects["Can4"]).SetVisible(bottle_sizes[3] == true);
        ((Bottle)iobjects["Can5"]).SetVisible(bottle_sizes[4] == true);
        ((Bottle)iobjects["Can6"]).SetVisible(bottle_sizes[5] == true);
        ((Bottle)iobjects["Can7"]).SetVisible(bottle_sizes[6] == true);
        ((Bottle)iobjects["Bag1"]).SetVisible(bottle_sizes[0] != true);
        ((Bottle)iobjects["Bag2"]).SetVisible(bottle_sizes[1] != true);
        ((Bottle)iobjects["Bag3"]).SetVisible(bottle_sizes[2] != true);
        ((Bottle)iobjects["Bag4"]).SetVisible(bottle_sizes[3] != true);
        ((Bottle)iobjects["Bag5"]).SetVisible(bottle_sizes[4] != true);
        ((Bottle)iobjects["Bag6"]).SetVisible(bottle_sizes[5] != true);
        ((Bottle)iobjects["Bag7"]).SetVisible(bottle_sizes[6] != true);

        station.AddObject(iobjects["Andrei"]);
        station.AddObject(iobjects["Lissu"]);
        station.AddObject(iobjects["Metro"]);
        station.AddObject(iobjects["ES"]);

        park.AddObject(iobjects["Bush"]);
        park.AddObject(iobjects["Can1"]);
        park.AddObject(iobjects["Can2"]);
        park.AddObject(iobjects["Can3"]);
        park.AddObject(iobjects["Can4"]);
        park.AddObject(iobjects["Can5"]);
        park.AddObject(iobjects["Can6"]);
        park.AddObject(iobjects["Can7"]);
        park.AddObject(iobjects["Bottle1"]);
        park.AddObject(iobjects["Bottle2"]);
        park.AddObject(iobjects["Bottle3"]);
        park.AddObject(iobjects["Bottle4"]);
        park.AddObject(iobjects["Bag1"]);
        park.AddObject(iobjects["Bag2"]);
        park.AddObject(iobjects["Bag3"]);
        park.AddObject(iobjects["Bag4"]);
        park.AddObject(iobjects["Bag5"]);
        park.AddObject(iobjects["Bag6"]);
        park.AddObject(iobjects["Bag7"]);

        conversation_target = null;
    }
}
