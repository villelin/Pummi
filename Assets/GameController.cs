using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private Player player;
    private Dictionary<GameObject, GameItem> item_map;
    private Dictionary<GameObject, int> item_type;
    private GameObject pate;
    private GameObject speech;
    private GameObject speech_target;
    private float time;

    private GameObject parkbg;
    private GameObject stationbg;
	private GameObject metro;
	private GameObject lissu;
	private GameObject andrei;
	private GameObject bush;

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

        player = new Player();
        item_map = new Dictionary<GameObject, GameItem>();

		item_map.Add(GameObject.Find("Andrei"), new GameItem("Andrei"));
		item_map.Add(GameObject.Find("Lissu"), new GameItem("Lissu"));
		item_map.Add(GameObject.Find("Bush"), new GameItem("Bush"));
        item_map.Add(GameObject.Find("Pullo1"), new GameItem("Pullo1"));
        item_map.Add(GameObject.Find("Pullo2"), new GameItem("Pullo2"));
        item_map.Add(GameObject.Find("Pullo3"), new GameItem("Pullo3"));
        item_map.Add(GameObject.Find("Pullo4"), new GameItem("Pullo4"));
        item_map.Add(GameObject.Find("Pullo5"), new GameItem("Pullo5"));
        item_map.Add(GameObject.Find("Pullo6"), new GameItem("Pullo6"));
        item_map.Add(GameObject.Find("Pullo7"), new GameItem("Pullo7"));

        item_type = new Dictionary<GameObject, int>();
        item_type.Add(GameObject.Find("Pullo1"), 1);
        item_type.Add(GameObject.Find("Pullo2"), 1);
        item_type.Add(GameObject.Find("Pullo3"), 1);
        item_type.Add(GameObject.Find("Pullo4"), 1);
        item_type.Add(GameObject.Find("Pullo5"), 1);
        item_type.Add(GameObject.Find("Pullo6"), 1);
        item_type.Add(GameObject.Find("Pullo7"), 1);
        item_type.Add(GameObject.Find("Andrei"), 2);
        item_type.Add(GameObject.Find("Lissu"), 3);
        item_type.Add(GameObject.Find("Bush"), 4);

        pate = GameObject.Find("Pate");

        Debug.Log(item_map);

        Text cashtext = GameObject.Find("Cash").GetComponent<Text>();
        cashtext.text = "€0";


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
		metro = GameObject.Find("Metro");
		lissu = GameObject.Find("Lissu");
		andrei = GameObject.Find("Andrei");
		bush = GameObject.Find("Bush");

        ChangeLocation(0);

        LoadGlobalState(); 
    }

	// Change current location, hides/shows objects based on location
	// 0 = station, 1 = park
	void ChangeLocation(int location)
	{
        current_location = location;

		switch (location)
		{
			case 0:			// station
				{
					parkbg.SetActive(false);		// hide park background
					stationbg.SetActive(true);      // show station background
					metro.SetActive(true);          // show metro

                    foreach (KeyValuePair<GameObject, int> obj in item_type)
                    {
                        switch (obj.Value)
                        {
                            case 1:     // pullo
                                obj.Key.SetActive(false);
                                break;
                            case 2:     // Andrei
                                obj.Key.SetActive(true);
                                break;
                            case 3:     // Lissu
                                obj.Key.SetActive(true);
                                break;
                            case 4:     // Bush
                                obj.Key.SetActive(false);
                                break;

                            default:
                                obj.Key.SetActive(false);
                                break;
                        }
                    }

					break;
				}
			case 1:			// park
				{
					parkbg.SetActive(true);			// show park background
					stationbg.SetActive(false);     // hide station background
					metro.SetActive(false);         // hide metro

                    foreach (KeyValuePair<GameObject, int> obj in item_type)
                    {
                        switch (obj.Value)
                        {
                            case 1:     // pullo
                                if (Random.Range(0.0f, 100.0f) < 20.0f)
                                    obj.Key.SetActive(true);
                                else
                                    obj.Key.SetActive(false);
                                break;
                            case 2:     // Andrei
                                obj.Key.SetActive(false);
                                break;
                            case 3:     // Lissu
                                obj.Key.SetActive(false);
                                break;
                            case 4:     // Bush
                                obj.Key.SetActive(true);
                                break;

                            default:
                                obj.Key.SetActive(false);
                                break;
                        }
                    }
                    break;
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
                    float x = Input.mousePosition.x;
                    float y = Input.mousePosition.y;
                    Vector3 tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pate.SendMessage("SetTarget", new Vector2(tp.x, tp.y));
                }
            }

            Vector3 patepos = pate.transform.position;

            speech.SetActive(false);
            speech_target = null;

            if (current_location == 0 && patepos.x > 680.0f)
            {
                StartTransition(1);
                //ChangeLocation(1);
            }
            else if (current_location == 1 && patepos.x < 40.0f)
            {
                //ChangeLocation(0);
                StartTransition(0);
            }

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
                        speech.transform.position = Camera.main.WorldToScreenPoint(go.transform.position - new Vector3(0, -80, 0));
                        speech.SetActive(true);

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
            cashtext.text = "€" + player.GetCash();

            // talk
            SaveGlobalState();
            SceneManager.LoadScene("conversation");
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
    }

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, fade_level);
        GUI.depth = -1000;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    }

    void LoadGlobalState()
    {
        Vector2 pate_pos = Persistence.instance.pate_position;
        Debug.Log("load pos " + pate_pos);
        pate.SendMessage("SetPosition", pate_pos);

        ChangeLocation(Persistence.instance.location);
    }

    void SaveGlobalState()
    {
        Vector2 pate_pos = pate.transform.position;
        Persistence.instance.pate_position = pate_pos;
        Persistence.instance.location = current_location;
    }
}
