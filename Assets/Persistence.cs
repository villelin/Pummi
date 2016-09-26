using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// global singleton class for holding info across scenes
public class Persistence : MonoBehaviour
{
    public Player player;
    public Dictionary<string, IInteractiveObject> iobjects;
    
    public static Persistence instance;

    void Awake()
    {
        if (!instance)
        {
            player = new Player();

            iobjects = new Dictionary<string, IInteractiveObject>();
            iobjects.Add("Andrei",  new NPC("Andrei"));
            iobjects.Add("Lissu", new NPC("Lissu"));
            iobjects.Add("Bush", new NPC("Bush"));
            iobjects.Add("Pullo1", new NPC("Pullo1"));
            iobjects.Add("Pullo2", new NPC("Pullo2"));
            iobjects.Add("Pullo3", new NPC("Pullo3"));
            iobjects.Add("Pullo4", new NPC("Pullo4"));
            iobjects.Add("Pullo5", new NPC("Pullo5"));
            iobjects.Add("Pullo6", new NPC("Pullo6"));
            iobjects.Add("Pullo7", new NPC("Pullo7"));


            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
