using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MinigamePate : MonoBehaviour {

    GameObject blood;
    GameObject pate;

    void Start()
    {
        blood = GameObject.Find("Blood");
        blood.SetActive(false);

        pate = GameObject.Find("Ball");
    }

    void Update()
    {

    }

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
            // go to gameover screen after 1.5 seconds
            pate.SendMessage("Crash");

            // show the blood in the position where Pate fell down
            blood.SetActive(true);

            Vector3 blood_pos = blood.transform.position;
            Vector3 pate_pos = pate.transform.position;
            blood.transform.position = new Vector3(pate_pos.x, blood_pos.y, blood_pos.z);
        }
        else if (col.name == "es")
        {
            Persistence.instance.player.SetESBuff();

            SceneManager.LoadScene("esscreen");
        }
    }
}