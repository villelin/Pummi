using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button button = GameObject.Find("StartButton").GetComponent<Button>();
        button.onClick.AddListener(() => { StartButtonClicked(); });
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void StartButtonClicked()
    {
        Debug.Log("pers " + Persistence.instance);

        Persistence.instance.NewGame();

        SceneManager.LoadScene("basescene");
    }
}
