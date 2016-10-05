using UnityEngine;
using System.Collections;

public class EscapePate : MonoBehaviour
{
    GameObject escape_controller;

	// Use this for initialization
	void Start ()
    {
        escape_controller = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        escape_controller.SendMessage("HitTrigger");
    }
}
