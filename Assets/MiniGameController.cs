using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MiniGameController : MonoBehaviour {

	PointerController up;
	PointerController down;
	PointerController left;
	PointerController right;
	//    RawImage box;
	GameObject ball;


    private float timer;

	// Use this for initialization, GetComponent<PointerController>
	void Start () {
		up = GameObject.Find ("Up").GetComponent<PointerController> ();
		down = GameObject.Find ("Down").GetComponent<PointerController> ();
		left = GameObject.Find ("Left").GetComponent<PointerController> ();
		right = GameObject.Find ("Right").GetComponent<PointerController> ();
		ball = GameObject.Find ("Ball");

        timer = 15.0f;

	}

	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

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
}
