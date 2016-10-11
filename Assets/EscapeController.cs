using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeController : MonoBehaviour
{
    GameObject inspector;
    GameObject pate;
    GameObject pate_sprite;
    Rigidbody2D pate_physics;
    Button jump_button;

    Text timer_text;

    float timer;

    const float pate_xleft = -500.0f;
    const float pate_xright = 600.0f;

    float inspector_jump_cooldown_timer;
    float inspector_animation_timer;
    float inspector_animation_duration;
    const float inspector_jump_height = 80.0f;

    Rigidbody2D lamp1;
    Rigidbody2D metro1;
    Rigidbody2D metro2;

    GameObject blood;

    Vector2 inspector_base_position = new Vector2(4, 181);

    float minigame_timer;
    float total_timer;
    float gameover_timer;
    bool gameover;

    bool spin;
    float spin_timer;

	// Use this for initialization
	void Start ()
    {
        inspector = GameObject.Find("Inspector");

        GameObject.Find("Music").GetComponent<AudioSource>().Play();
        pate = GameObject.Find("Pate");

        pate_physics = GameObject.Find("Pate").GetComponent<Rigidbody2D>();
        pate_sprite = GameObject.Find("PateSprite");

        lamp1 = GameObject.Find("Lamp1").GetComponent<Rigidbody2D>();
        lamp1.velocity = new Vector2(-180.0f, 0.0f);

        metro1 = GameObject.Find("Metro1").GetComponent<Rigidbody2D>();
        metro1.velocity = new Vector2(-50.0f, 0.0f);

        metro2 = GameObject.Find("Metro2").GetComponent<Rigidbody2D>();
        metro2.velocity = new Vector2(-50.0f, 0.0f);

        jump_button = GameObject.Find("JumpButton").GetComponent<Button>();
        jump_button.onClick.AddListener(() => { JumpButtonClicked(); });

        jump_button.image.color = new Color32(255, 64, 64, 255);

        timer_text = GameObject.Find("TimerText").GetComponent<Text>();

        blood = GameObject.Find("Blood");
        blood.SetActive(false);

        inspector_jump_cooldown_timer = 0;
        inspector_animation_timer = 0;
        inspector_animation_duration = 5.0f;

        minigame_timer = 0;
        total_timer = 0;
        gameover_timer = 0.0f;

        spin = false;
        spin_timer = 0.0f;

        gameover = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        minigame_timer += Time.deltaTime;

        if (!gameover)
            total_timer += Time.deltaTime;

        // speed increases by 40% every 15 seconds
        if (minigame_timer >= 15.0f)
        {
            minigame_timer = 0.0f;

            // increase speed by 40%
            lamp1.velocity *= 1.4f;
            metro1.velocity *= 1.4f;
            metro2.velocity *= 1.4f;

            inspector_animation_duration /= 1.4f;
        }

        // count down the gameover timer and go to gameover if expired
        if (gameover_timer > 0.0f)
        {
            gameover_timer -= Time.deltaTime;
            if (gameover_timer <= 0.0f)
            {
                SceneManager.LoadScene("gameover");
            }
        }

        // spin Pate if we're spinning
        if (spin)
        {
            spin_timer += Time.deltaTime;
            pate_sprite.transform.rotation = Quaternion.Euler(0, 0, spin_timer * 360.0f);
        }

        // format the escape timer text
        timer_text.text = string.Format("Aika pakoon: {0:0.00}s", 60.0f - total_timer);

        float color_fade = Mathf.Abs(Mathf.Cos((total_timer / 10.0f) * 180.0f / Mathf.PI));
        timer_text.color = new Color(1.0f, color_fade, 0.0f);
        
        // we win if 60 seconds have passed
        if (total_timer >= 60.0f && !gameover)
        {
            SceneManager.LoadScene("winscreen");
        }

        // reduce inspector jump cooldown
        if (inspector_jump_cooldown_timer > 0.0f)
        {
            inspector_jump_cooldown_timer -= Time.deltaTime;
        }

        // move lamp and metro cars back to start after they are off the screen
        if (lamp1.position.x < -400.0f)
        {
            lamp1.position = new Vector2(460.0f, 204.0f);
        }
        if (metro1.position.x < -450.0f)
            metro1.position = new Vector2(750.0f, 58.0f);
        if (metro2.position.x < -450.0f)
            metro2.position = new Vector2(750.0f, 58.0f);

        // inspector AI
        if (inspector_jump_cooldown_timer <= 0.0f && !gameover)
        {
            BoxCollider2D insbox = inspector.GetComponent<BoxCollider2D>();
            BoxCollider2D metro1_box = metro1.GetComponent<BoxCollider2D>();
            BoxCollider2D metro2_box = metro2.GetComponent<BoxCollider2D>();

            if (Mathf.Abs(metro1_box.bounds.max.x - insbox.bounds.max.x) < 5.0f ||
                Mathf.Abs(metro2_box.bounds.max.x - insbox.bounds.max.x) < 5.0f)
            {
                inspector_jump_cooldown_timer = inspector_animation_duration;
                inspector_animation_timer = inspector_animation_duration;
            }
        }

        // calculate sway animation for Pate
        float sway_anim_angle = (total_timer / 1.4f) * 180.0f / Mathf.PI;
        if (Mathf.Abs(pate_physics.velocity.y) < 0.5f)
        {
            float pate_angle = sway_anim_angle;
            if (gameover)
                pate_angle = 0;
            pate_sprite.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Cos(pate_angle) * 20.0f));
        }

        // update inspector position if we're currently animating the jump
        float inspector_jump_offset = 0.0f;
        if (inspector_animation_timer > 0.0f)
        {
            inspector_animation_timer -= Time.deltaTime;

            float angle = (inspector_animation_timer / inspector_animation_duration) * 180.0f;
            inspector_jump_offset = Mathf.Sin(angle * Mathf.Deg2Rad) * inspector_jump_height;
        }

        inspector.transform.position = inspector_base_position + new Vector2(0, inspector_jump_offset);

        // calculate sway animation for inspector if he's not jumping (and not game over)
        if (inspector_animation_timer <= 0.0f && !gameover)
        {
            inspector.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Cos(sway_anim_angle) * 10.0f);
        }
        else
        {
            inspector.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        /*
        if (Mathf.Abs(pate_physics.velocity.y) < 0.5f)
        {
            Vector3 pate_pos = pate_physics.transform.position;
            pate_sprite.transform.position = pate_pos + new Vector3(0, Mathf.Abs(Mathf.Sin((total_timer / 6.0f) * 180.0f / Mathf.PI)) * 20.0f, 0);
        }*/

	}

    /// <summary>
    /// Called when Jump button is clicked
    /// </summary>
    void JumpButtonClicked()
    {
        float pate_velocity = Mathf.Abs(pate_physics.velocity.y);

        if (pate_velocity < 0.5f)
        {
            pate_physics.AddForce(new Vector2(0, 200.0f), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Called when Pate hits of the trigger colliders
    /// </summary>
    /// <param name="collider">Name of the collider</param>
    void HitTrigger(string collider)
    {
        if (collider == "FloorCollider")
        {
            // spawn blood if we hit the floor

            Vector3 blood_pos = blood.transform.position;
            Vector3 pate_pos = pate.transform.position;
            blood.transform.position = new Vector3(pate_pos.x, blood_pos.y, blood_pos.z);

            blood.SetActive(true);
            gameover_timer = 1.5f;
            gameover = true;

            StopGame();
        }
        else
        {
            // start spinning if we hit any other trigger (= inspector)
            spin = true;
            gameover_timer = 1.5f;
            gameover = true;

            StopGame();
        }
    }

    /// <summary>
    /// Stops the movement of all objects
    /// </summary>
    void StopGame()
    {
        metro1.velocity = new Vector3();
        metro2.velocity = new Vector3();
        lamp1.velocity = new Vector3();
    }
}
