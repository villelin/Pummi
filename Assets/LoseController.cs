using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoseController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button button;
        button = GameObject.Find("Button1").GetComponent<Button>();
        button.onClick.AddListener(() => { Button1Clicked(); });

        button = GameObject.Find("Button2").GetComponent<Button>();
        button.onClick.AddListener(() => { Button2Clicked(); });
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Button1Clicked()
    {
        SceneManager.LoadScene("intro");
    }

    void Button2Clicked()
    {
        SceneManager.LoadScene("escape");
    }
}
