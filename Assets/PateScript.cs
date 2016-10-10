using UnityEngine;
using System.Collections;

public class PateScript : MonoBehaviour
{
    SpriteRenderer renderer;
    Vector2 target_pos;
    GameObject game_controller;
    Rigidbody2D pate_physics;
    float angle;
    const float speed = 100.0f;
    bool walking;

	// Use this for initialization
	void Start ()
    {
        renderer = GameObject.Find("PateSprite").GetComponent<SpriteRenderer>();
        game_controller = GameObject.Find("GameController");
        angle = 0.0f;

        pate_physics = GameObject.Find("Pate").GetComponent<Rigidbody2D>();

        walking = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float angrad = angle * 180.0f / Mathf.PI;

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
            walk_offset = Mathf.Abs(Mathf.Sin(angrad)) * 7.0f;
            walk_rot = 0.0f - Mathf.Sin(angrad) * 20.0f;
        }

        renderer.transform.localRotation = Quaternion.Euler(0, 0, walk_rot);
        renderer.transform.position = transform.position - new Vector3(0, walk_offset, 0);
    }

    /// <summary>
    /// Gives Pate the command to start moving to the given position 
    /// </summary>
    /// <param name="pos">Target position</param>
    void SetTarget(Vector2 pos)
    {
        Debug.Log("SET TARGET " + pos);
        target_pos = pos;

        Vector2 direction = target_pos - pate_physics.position;
        direction.Normalize();

        pate_physics.velocity = direction * speed;

        if (Persistence.instance.player.HasESBuff())
            pate_physics.velocity *= 3.0f;

		if (direction.x < 0.0f)
			renderer.flipX = false;
		else
			renderer.flipX = true;

        walking = true;
	}

    /// <summary>
    /// Instantly sets a new position for Pate.
    /// </summary>
    /// <param name="pos">New position</param>
    void SetPosition(Vector2 pos)
    {
        Debug.Log("SET POSITION " + pos);
        target_pos = pos;

        pate_physics.position = pos;
        pate_physics.velocity = new Vector2(0, 0);
        walking = false;
    }

    /// <summary>
    /// Stops Pate's movement
    /// </summary>
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
        if (collider.name == "LeftLocationTrigger")
        {
            game_controller.SendMessage("LeftExitTrigger");
        }
        else if (collider.name == "RightLocationTrigger")
        {
            game_controller.SendMessage("RightExitTrigger");
        }
    }
}
