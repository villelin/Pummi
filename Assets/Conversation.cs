using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Conversation : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Button sb = GameObject.Find("DoneButton").GetComponent<Button>();
        sb.onClick.AddListener(() => { DoneButtonClicked(); });
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    void DoneButtonClicked()
    {
        Persistence.instance.cash += 100;

        SceneManager.LoadScene("basescene");
    }
}
