using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

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
    Location current_location;

    bool transition_active;
    Location transition_target;
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
        item_map.Add(GameObject.Find("Martta"), Persistence.instance.iobjects["Martta"]);
        item_map.Add(GameObject.Find("Bush"), Persistence.instance.iobjects["Bush"]);
        item_map.Add(GameObject.Find("Can1"), Persistence.instance.iobjects["Can1"]);
        item_map.Add(GameObject.Find("Can2"), Persistence.instance.iobjects["Can2"]);
        item_map.Add(GameObject.Find("Can3"), Persistence.instance.iobjects["Can3"]);
        item_map.Add(GameObject.Find("Can4"), Persistence.instance.iobjects["Can4"]);
        item_map.Add(GameObject.Find("Can5"), Persistence.instance.iobjects["Can5"]);
        item_map.Add(GameObject.Find("Can6"), Persistence.instance.iobjects["Can6"]);
        item_map.Add(GameObject.Find("Can7"), Persistence.instance.iobjects["Can7"]);
        item_map.Add(GameObject.Find("Metro"), Persistence.instance.iobjects["Metro"]);
        item_map.Add(GameObject.Find("ES"), Persistence.instance.iobjects["ES"]);
        item_map.Add(GameObject.Find("Bottle1"), Persistence.instance.iobjects["Bottle1"]);
        item_map.Add(GameObject.Find("Bottle2"), Persistence.instance.iobjects["Bottle2"]);
        item_map.Add(GameObject.Find("Bottle3"), Persistence.instance.iobjects["Bottle3"]);
        item_map.Add(GameObject.Find("Bottle4"), Persistence.instance.iobjects["Bottle4"]);
        item_map.Add(GameObject.Find("Bag1"), Persistence.instance.iobjects["Bag1"]);
        item_map.Add(GameObject.Find("Bag2"), Persistence.instance.iobjects["Bag2"]);
        item_map.Add(GameObject.Find("Bag3"), Persistence.instance.iobjects["Bag3"]);
        item_map.Add(GameObject.Find("Bag4"), Persistence.instance.iobjects["Bag4"]);
        item_map.Add(GameObject.Find("Bag5"), Persistence.instance.iobjects["Bag5"]);
        item_map.Add(GameObject.Find("Bag6"), Persistence.instance.iobjects["Bag6"]);
        item_map.Add(GameObject.Find("Bag7"), Persistence.instance.iobjects["Bag7"]);

        Debug.Log("item_map = " + item_map);

        pate = GameObject.Find("Pate");
        
        background = GameObject.Find("StationBG").GetComponent<SpriteRenderer>();

        UpdateCash();

        Text speechtext = GameObject.Find("SpeechText").GetComponent<Text>();
        speechtext.text = "STEAL!";

        Button sb = GameObject.Find("Speech").GetComponent<Button>();
		sb.onClick.AddListener(()=> { SpeechButtonClicked(); });

		Button quit = GameObject.Find("Quit").GetComponent<Button>();
		quit.onClick.AddListener(()=> { QuitButtonClicked(); });

        speech = GameObject.Find("Speech");
        speech.SetActive(false);

        speech_target = null;

        time = 0.0f;

        parkbg = GameObject.Find("ParkBG");
        stationbg = GameObject.Find("StationBG");

        ChangeLocation(Persistence.instance.station);

        LoadGlobalState(); 
    }



	// Change current location, hides/shows objects based on location
	// 0 = station, 1 = park
	void ChangeLocation(Location new_location)
	{
        if (current_location != new_location)
        {
            if (new_location.GetBackground() == 0)
            {
                GameObject.Find("StationMusic").GetComponent<AudioSource>().Play();
                GameObject.Find("ParkMusic").GetComponent<AudioSource>().Stop();
            }
            else
            {
                GameObject.Find("StationMusic").GetComponent<AudioSource>().Stop();
                GameObject.Find("ParkMusic").GetComponent<AudioSource>().Play();
            }
        }

        current_location = new_location;

        if (current_location.GetBackground() == 0)
        {
            parkbg.SetActive(false);
            stationbg.SetActive(true);
        }
        else
        {
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
        List<IInteractiveObject> objlist = current_location.GetObjects();
        foreach (IInteractiveObject obj in objlist)
        {
            foreach (KeyValuePair<GameObject, IInteractiveObject> item_pair in item_map)
            {
                if (item_pair.Value == obj)
                {
                    GameObject go = item_pair.Key;

                    if (item_pair.Value.GetType() == typeof(Bottle))
                    {
                        // bottle types can be visible or invisible
                        go.SetActive(((Bottle)item_pair.Value).IsVisible());
                    }
                    else
                    {
                        go.SetActive(true);
                    }
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
                // don't move if we're actually clicking an UI item
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    pate.SendMessage("SetTarget", new Vector2(tp.x, tp.y));
                }
            }

            Vector3 patepos = pate.transform.localPosition;

            speech.SetActive(false);
            speech_target = null;

            // go through all interactable objects and check if we're close to them
            foreach (KeyValuePair<GameObject, IInteractiveObject> obj in item_map)
            {
                GameObject go = obj.Key;
                IInteractiveObject iobj = obj.Value;
                // only handle objects that are active
                if (go.activeSelf)
                {
                    Vector3 gopos = go.transform.localPosition;

                    Vector3 diff = gopos - patepos;
                    InteractType type = iobj.GetInteractType();

                    // loot if we're close enough
                    if (iobj.GetInteractType() != InteractType.None && diff.magnitude < 50.0f)
                    {
                        speech.transform.position = Camera.main.WorldToScreenPoint(go.transform.position) - new Vector3(0, -60, 0) ;
                        speech.SetActive(true);

                        Text speech_text = GameObject.Find("SpeechText").GetComponent<Text>();

                        switch (type)
                        {
                            case InteractType.Lootable:
                                speech_text.text = "Ota";
                                break;
                            case InteractType.NPC:
                                speech_text.text = "Puhu";
                                break;
                            case InteractType.Metro:
                                speech_text.text = "Mene metroon";
                                break;
                            case InteractType.Bush:
                                speech_text.text = "Kähmi puskaa";
                                break;
                            case InteractType.ES:
                                speech_text.text = "Tutki ES-tölkkiä";
                                break;
                        }
                        speech_target = go;

                        break;
                    }
                }
            }
        }
	}

    void UpdateCash()
    {
        Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
        cashtext.text = string.Format("Rahat: {0:0.00} €", Persistence.instance.player.GetCash());
    }

    // interact with a gameobject
    void Interact(GameObject obj)
    {
        if (obj != null)
        {
            // find item for this object
            IInteractiveObject item = item_map[obj];

            InteractType type = item.GetInteractType();

            item.Interact(0);

            switch (type)
            {
                case InteractType.Lootable:
                    {
                        // hide this object after it's looted
                        obj.SetActive(false);
                        ((Bottle)item).SetVisible(false);

                        double cash = ((Bottle)item).GetCash();   

                        Persistence.instance.player.AddCash(cash);

                        UpdateCash();
                        break;
                    }

                case InteractType.NPC:
                    {
                        Persistence.instance.conversation_target = (NPC)item;

                        // talk
                        SaveGlobalState();
                        SceneManager.LoadScene("conversation");
                        break;
                    }

                case InteractType.Metro:
                    {
                        int inspector_rng = Random.Range(0, 100);

                        // win the game if we have at least 5 euros or 10% random chance for no inspector
                        if (Persistence.instance.player.GetCash() >= 5.0 || inspector_rng < 10)
                        {
                            SceneManager.LoadScene("winscreen");
                        }
			if (Persistence.instance.player.HasESBuff () && Persistence.instance.player.GetCash() >= 5.0 || inspector_rng < 20) {
			SceneManager.LoadScene("winscreen");
					}
                        else
                        {
                            // 90% chance for inspector
                            Persistence.instance.conversation_target = (NPC)Persistence.instance.iobjects["Inspector"];

                            // talk to inspector
                            SaveGlobalState();
                            SceneManager.LoadScene("conversation");
                        }
                        break;
                    }

                case InteractType.Bush:
                    {
                        ((Bottle)Persistence.instance.iobjects["Bottle1"]).SetVisible(true);
                        ((Bottle)Persistence.instance.iobjects["Bottle2"]).SetVisible(true);
                        ((Bottle)Persistence.instance.iobjects["Bottle3"]).SetVisible(true);
                        ((Bottle)Persistence.instance.iobjects["Bottle4"]).SetVisible(true);
                        ChangeLocation(current_location);
                        break;
                    }

                case InteractType.ES:
                    {
                        SaveGlobalState();
                        SceneManager.LoadScene("kallioquest");
                        break;
                    }
            }
        }
	}

	void QuitButtonClicked()
	{
		Application.Quit ();
	}

    void SpeechButtonClicked()
    {
        if (speech_target != null)
        {
            Interact(speech_target);
        }
    }

    void StartTransition(Location location)
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

    void LeftExitTrigger()
    {
        StartTransition(current_location.GetLeftExit());
    }

    void RightExitTrigger()
    {
        StartTransition(current_location.GetRightExit());
    }
}
