using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button button = GameObject.Find("DoneButton").GetComponent<Button>();
        button.onClick.AddListener(() => { DoneButtonClicked(); });
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void DoneButtonClicked()
    {
        SceneManager.LoadScene("intro");
    }
}
