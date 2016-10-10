using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour {

	PointerController up;
	PointerController down;
	PointerController left;
	PointerController right;
	//    RawImage box;
	GameObject ball;

    Text timer_text;
    float gameover_timer;
    bool gameover;

    private float timer;

	// Use this for initialization, GetComponent<PointerController>
	void Start () {
		up = GameObject.Find ("Up").GetComponent<PointerController> ();
		down = GameObject.Find ("Down").GetComponent<PointerController> ();
		left = GameObject.Find ("Left").GetComponent<PointerController> ();
		right = GameObject.Find ("Right").GetComponent<PointerController> ();
		ball = GameObject.Find ("Ball");

        timer_text = GameObject.Find("TimerText").GetComponent<Text>();

        timer = 15.0f;
        gameover_timer = 0.0f;
        gameover = false;

        GameObject.Find("Music").GetComponent<AudioSource>().Play();
	}

	// Update is called once per frame
	void Update () {

        if (!gameover)
        {
            timer -= Time.deltaTime;
        } 
        else
        {
            gameover_timer -= Time.deltaTime;
            if (gameover_timer <= 0.0f)
            {
                SceneManager.LoadScene("gameover");
            }
        }

        timer_text.text = string.Format("Aikaa jäljellä: {0:0.00s}", Mathf.Max(timer, 0.0f));

        if (timer <= 0.0f)
        {
            SceneManager.LoadScene("basescene");
        }

		if (up.getPressed()) {
			ball.transform.Translate (0,0.1f,0);    
			Debug.Log ("YLÖS painettu"); //TESTAA
		}
		if (down.getPressed()) {
			ball.transform.Translate (0,-0.1f,0);    
		}
		if (left.getPressed()) {
			ball.transform.Translate (-0.1f,0,0);    
		}
		if (right.getPressed()) {
			ball.transform.Translate (0.1f,0,0);    
		}
	}

    void Crash()
    {
        gameover = true;
        gameover_timer = 1.5f;
    }
}
