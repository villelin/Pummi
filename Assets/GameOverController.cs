using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button button;
        button = GameObject.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() => { ButtonClicked(); });
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void ButtonClicked()
    {
        SceneManager.LoadScene("intro");
    }
}
