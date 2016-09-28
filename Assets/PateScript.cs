using UnityEngine;
using System.Collections;

public class PateScript : MonoBehaviour
{
    SpriteRenderer renderer;
    Vector2 target_pos;
    GameObject target_object;
    GameObject game_controller;
    Rigidbody2D pate_physics;
    float angle;
    const float speed = 100.0f;
    bool walking;

	// Use this for initialization
	void Start ()
    {
        renderer = GameObject.Find("Pate").GetComponent<SpriteRenderer>();
        game_controller = GameObject.Find("GameController");
        angle = 0.0f;

        pate_physics = GameObject.Find("Pate").GetComponent<Rigidbody2D>();

        target_object = null;

        walking = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float angrad = angle * 180.0f / Mathf.PI;
        float x = Mathf.Cos(angrad);
        float y = Mathf.Sin(angrad);
        //transform.localPosition = new Vector2(x, y);
        //transform.localScale = new Vector3(0.1f + (Mathf.Abs(x) * 0.5f), 0.1f + (Mathf.Abs(x) * 0.5f), 1.0f);
        
        angle += 0.008f;

        float walk_offset = 0.0f;
        float walk_rot = 0.0f;

        Vector2 direction = target_pos - pate_physics.position;
        if (direction.magnitude < 5.0f)
        {
            // close enough to target, let's stop
            StopMoving();
        }

        //transform.position = new Vector3(current_pos.x, current_pos.y + walk_offset, 0);

        renderer.sortingOrder = 1000 - (int)Mathf.Floor(transform.position.y);

        // animate pate if walking
        if (walking)
        {
            walk_offset = Mathf.Abs(Mathf.Sin(angrad)) * 15.0f;
            walk_rot = 0.0f - Mathf.Sin(angrad) * 20.0f;
        }

        transform.localRotation = Quaternion.Euler(0, 0, walk_rot);
    }

    void SetTarget(Vector2 pos)
    {
        Debug.Log("SET TARGET " + pos);
        target_pos = pos;

        Vector2 direction = target_pos - pate_physics.position;
        direction.Normalize();

        pate_physics.velocity = direction * speed;

        
		if (direction.x < 0.0f)
			renderer.flipX = false;
		else
			renderer.flipX = true;

        walking = true;
	}

    void SetPosition(Vector2 pos)
    {
        target_pos = pos;

        pate_physics.position = pos;
        pate_physics.velocity = new Vector2(0, 0);
        walking = false;
    }

    void StopMoving()
    {
        target_pos = pate_physics.position;

        pate_physics.velocity = new Vector2(0, 0);
        walking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("törmättiin");

        StopMoving();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "LeftLocationTrigger" ||
            collider.name == "RightLocationTrigger")
        {
            Debug.Log("triggered");
            game_controller.SendMessage("LocationTrigger");
        }
    }
}
