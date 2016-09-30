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
        iobjects.Add("Can1", new Bottle());
        iobjects.Add("Can2", new Bottle());
        iobjects.Add("Can3", new Bottle());
        iobjects.Add("Can4", new Bottle());
        iobjects.Add("Can5", new Bottle());
        iobjects.Add("Can6", new Bottle());
        iobjects.Add("Can7", new Bottle());
        iobjects.Add("Metro", new Metro());
        iobjects.Add("ES", new ES());
        iobjects.Add("Bottle1", new Bottle());
        iobjects.Add("Bottle2", new Bottle());
        iobjects.Add("Bottle3", new Bottle());
        iobjects.Add("Bottle4", new Bottle());
        iobjects.Add("Inspector", new Inspector());

        // set random cash values for bottle types
        foreach (KeyValuePair<string,IInteractiveObject> obj in iobjects)
        {
            if (obj.Value.GetType() == typeof(Bottle))
            {
                ((Bottle)obj.Value).SetCash(Random.Range(0.1f, 40.0f));
            }
        }

        // set spawnable bottles as invisible to begin with
        ((Bottle)iobjects["Bottle1"]).SetVisible(false);
        ((Bottle)iobjects["Bottle2"]).SetVisible(false);
        ((Bottle)iobjects["Bottle3"]).SetVisible(false);
        ((Bottle)iobjects["Bottle4"]).SetVisible(false);

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
        park.AddObject(iobjects["ES"]);
        park.AddObject(iobjects["Bottle1"]);
        park.AddObject(iobjects["Bottle2"]);
        park.AddObject(iobjects["Bottle3"]);
        park.AddObject(iobjects["Bottle4"]);

        conversation_target = null;
    }
}
