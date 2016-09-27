using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private Dictionary<GameObject, IInteractiveObject> item_map;
    private GameObject pate;
    private GameObject speech;
    private GameObject speech_target;
    private float time;

    private SpriteRenderer background;
    private GameObject parkbg;
    private GameObject stationbg;

    int sw = 0;
    int current_location;

    bool transition_active;
    int transition_target;
    float transition_in_timer;
    float transition_out_timer;
    float fade_level;


	// Use this for initialization
	void Start ()
    {
        transition_active = false;
        transition_in_timer = 0;
        transition_out_timer = 0;
        fade_level = 0.0f;

        item_map = new Dictionary<GameObject, IInteractiveObject>();

        item_map.Add(GameObject.Find("Andrei"), Persistence.instance.iobjects["Andrei"]);
        item_map.Add(GameObject.Find("Lissu"), Persistence.instance.iobjects["Lissu"]);
        item_map.Add(GameObject.Find("Bush"), Persistence.instance.iobjects["Bush"]);
        item_map.Add(GameObject.Find("Pullo1"), Persistence.instance.iobjects["Pullo1"]);
        item_map.Add(GameObject.Find("Pullo2"), Persistence.instance.iobjects["Pullo2"]);
        item_map.Add(GameObject.Find("Pullo3"), Persistence.instance.iobjects["Pullo3"]);
        item_map.Add(GameObject.Find("Pullo4"), Persistence.instance.iobjects["Pullo4"]);
        item_map.Add(GameObject.Find("Pullo5"), Persistence.instance.iobjects["Pullo5"]);
        item_map.Add(GameObject.Find("Pullo6"), Persistence.instance.iobjects["Pullo6"]);
        item_map.Add(GameObject.Find("Pullo7"), Persistence.instance.iobjects["Pullo7"]);
        item_map.Add(GameObject.Find("Metro"), Persistence.instance.iobjects["Metro"]);

        Debug.Log("item_map = " + item_map);

        pate = GameObject.Find("Pate");
        
        background = GameObject.Find("StationBG").GetComponent<SpriteRenderer>();

        Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
        cashtext.text = "€" + Persistence.instance.player.GetCash();

        Text speechtext = GameObject.Find("SpeechText").GetComponent<Text>();
        speechtext.text = "STEAL!";

        Button sb = GameObject.Find("Speech").GetComponent<Button>();
        sb.onClick.AddListener(()=> { SpeechButtonClicked(); });

        speech = GameObject.Find("Speech");
        speech.SetActive(false);

        speech_target = null;

        time = 0.0f;

        parkbg = GameObject.Find("ParkBG");
        stationbg = GameObject.Find("StationBG");

        ChangeLocation(0);

        LoadGlobalState(); 
    }

	// Change current location, hides/shows objects based on location
	// 0 = station, 1 = park
	void ChangeLocation(int new_location)
	{
        current_location = new_location;

        Location location;
        if (new_location == 0)
        {
            location = Persistence.instance.station;
            parkbg.SetActive(false);
            stationbg.SetActive(true);
        }
        else
        {
            location = Persistence.instance.park;
            parkbg.SetActive(true);
            stationbg.SetActive(false);
        }

        // hide everything by default
        foreach (KeyValuePair<GameObject, IInteractiveObject> obj in item_map)
        {
            GameObject go = obj.Key;
            go.SetActive(false);
        }

        // show objects based on location
        List<IInteractiveObject> objlist = location.GetObjects();

        foreach (IInteractiveObject obj in objlist)
        {
            foreach (KeyValuePair<GameObject, IInteractiveObject> item_pair in item_map)
            {
                if (item_pair.Value == obj)
                {
                    GameObject go = item_pair.Key;
                    go.SetActive(true);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        if (transition_in_timer > 0)
        {
            transition_in_timer -= Time.deltaTime;

            if (transition_in_timer <= 0.0f)
            {
                ChangeLocation(transition_target);
                transition_out_timer = 1.0f;

                // put Pate in the center
                pate.SendMessage("SetPosition", (Vector2)background.bounds.center);
            }

            fade_level = Mathf.Max(1.0f - transition_in_timer, 0.0f);
        }
        if (transition_out_timer > 0)
        {
            transition_out_timer -= Time.deltaTime;
            if (transition_out_timer <= 0.0f)
                transition_active = false;

            fade_level = Mathf.Max(transition_out_timer, 0.0f);
            fade_level = Mathf.Min(transition_out_timer, 1.0f);
        }

        if (!transition_active)
        {
            // if mouse was clicked, set a target position for Pate
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("clicked");

                // don't move if we're actually clicking an UI item
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pate.SendMessage("SetTarget", new Vector2(tp.x, tp.y));
                }
            }

            Vector3 patepos = pate.transform.position;

            speech.SetActive(false);
            speech_target = null;

            // change location if we walk to edge of the screen
            if (patepos.x > (background.bounds.max.x - 40.0f) ||
                patepos.x < (background.bounds.min.x + 40.0f))
            {
                if (current_location == 0)
                    StartTransition(1);
                else
                    StartTransition(0);
            }

            // go through all interactable objects and check if we're close to them
            foreach (KeyValuePair<GameObject, IInteractiveObject> obj in item_map)
            {
                GameObject go = obj.Key;
                IInteractiveObject iobj = obj.Value;
                // only handle objects that are active
                if (go.activeSelf)
                {
                    Vector3 gopos = go.transform.position;

                    Vector3 diff = gopos - patepos;
                    // loot if we're close enough
                    if (diff.magnitude < 50.0f)
                    {
                        //Loot(go);
                        if (iobj.CanTalk())
                        {
                            speech.transform.position = Camera.main.WorldToScreenPoint(go.transform.position - new Vector3(0, -80, 0));
                            speech.SetActive(true);

                            Text speech_text = GameObject.Find("SpeechText").GetComponent<Text>();
                            speech_text.text = "Talk";
                        }

                        speech_target = go;
                        break;
                    }
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
            IInteractiveObject item = item_map[obj];
            if (item != null)
            {
            }

            // hide this object after it's looted
            if (item.CanLoot())
            {
                obj.SetActive(false);
            }

            // update cash
            Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
            cashtext.text = "€" + Persistence.instance.player.GetCash();

            if (item.CanTalk())
            {
                item.Interact(0);

                Persistence.instance.conversation_target = (NPC)item;

                // talk
                SaveGlobalState();
                SceneManager.LoadScene("conversation");
            }
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

    void StartTransition(int location)
    {
        transition_target = location;
        transition_in_timer = 1.0f;
        transition_active = true;

        // stop Pate
        pate.SendMessage("StopMoving");
    }

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, fade_level);
        GUI.depth = -1000;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    }

    void LoadGlobalState()
    {
        Vector2 pate_pos = Persistence.instance.player.GetPosition();
        Debug.Log("load pos " + pate_pos);
        pate.SendMessage("SetPosition", pate_pos);

        ChangeLocation(Persistence.instance.player.GetLocation());
    }

    void SaveGlobalState()
    {
        Vector2 pate_pos = pate.transform.position;
        Persistence.instance.player.SetPosition(pate_pos);
        Persistence.instance.player.SetLocation(current_location);
    }
}
