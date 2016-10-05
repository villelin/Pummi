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

        GameObject.Find("StoryAudio").GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void StartButtonClicked()
    {
        GameObject.Find("StoryAudio").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("basescene");
    }
}
