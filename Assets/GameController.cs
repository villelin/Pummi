using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    private Player player;
    private Dictionary<GameObject, GameItem> item_map;
    private GameObject pate;
    private GameObject speech;
    private GameObject speech_target;
    private float time;


	// Use this for initialization
	void Start ()
    {
        player = new Player();
        item_map = new Dictionary<GameObject, GameItem>();

        item_map.Add(GameObject.Find("Collect1"), new GameItem("Apple 1"));
        item_map.Add(GameObject.Find("Collect2"), new GameItem("Apple 2"));
        item_map.Add(GameObject.Find("Collect3"), new GameItem("Apple 3"));
        item_map.Add(GameObject.Find("Collect4"), new GameItem("Apple 4"));

        pate = GameObject.Find("Pate");

        Debug.Log(item_map);

        Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
        cashtext.text = "$0";


        Text speechtext = GameObject.Find("SpeechText").GetComponent<Text>();
        speechtext.text = "STEAL!";

        Button sb = GameObject.Find("Speech").GetComponent<Button>();
        sb.onClick.AddListener(()=> { SpeechButtonClicked(); });

        speech = GameObject.Find("Speech");
        speech.SetActive(false);

        speech_target = null;

        time = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        // respawn thing every 5 seconds
        if (time >= 5.0f)
        {
            foreach (KeyValuePair<GameObject, GameItem> obj in item_map)
            {
                GameObject go = obj.Key;
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                    break;
                }
            }
            time = 0.0f;
        }

        // if mouse was clicked, set a target position for Pate
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("clicked");

            // don't move if we're actually clicking an UI item
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;
                pate.SendMessage("SetTarget", new Vector2(x, y));
            }
        }

        Vector3 patepos = pate.transform.position;

        speech.SetActive(false);
        speech_target = null;

        // go through all interactable objects and check if we're close to them
        foreach (KeyValuePair<GameObject, GameItem> obj in item_map)
        {
            GameObject go = obj.Key;
            // only handle objects that are active
            if (go.activeSelf)
            {
                Vector3 gopos = go.transform.position;

                Vector3 diff = gopos - patepos;
                // loot if we're close enough
                if (diff.magnitude < 50.0f)
                {
                    //Loot(go);
                    speech.transform.position = go.transform.position - new Vector3(0, -80, 0);
                    speech.SetActive(true);

                    speech_target = go;
                    break;
                }
            }
        }
	}

    // loot a gameobject
    void Loot(GameObject obj)
    {
        if (obj != null)
        {
            // find item for this object
            GameItem item = item_map[obj];
            if (item != null)
            {
                player.Loot(item);
            }

            // hide this object after it's looted
            obj.SetActive(false);

            Debug.Log("Current inv: " + player.GetInventory());

            // update inventory
       //     Text invtext = GameObject.Find("Text").GetComponent<Text>();
       //     invtext.text = player.GetInventory();

            // update cash
            Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
            cashtext.text = "$" + player.GetCash();
        }
    }

    void SpeechButtonClicked()
    {
        if (speech_target != null)
        {
            Loot(speech_target);

            // reset the respawn timer
            time = 0.0f;
        }
    }
}
