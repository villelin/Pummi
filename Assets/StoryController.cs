using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{

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
        SceneManager.LoadScene("basescene");
    }
}
