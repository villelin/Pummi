using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		print ("hit "+ col.gameObject.name);

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "LeftOffscreen" ||
            col.name == "RightOffscreen")
        {
            SceneManager.LoadScene("basescene");
        }
        else if (col.name == "GameoverTrigger")
        {
            SceneManager.LoadScene("gameover");
        }
        else if (col.name == "es")
        {
            Persistence.instance.player.SetESBuff();
        }
    }
}