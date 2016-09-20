using UnityEngine;
using System.Collections;


// global singleton class for holding info across scenes
public class Persistence : MonoBehaviour
{
    public static Persistence instance;
    public Vector2 pate_position;
    public int location;
    public int cash;

    void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            pate_position = new Vector2(400, 100);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
