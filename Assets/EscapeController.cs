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

    float jump_cooldown_timer;
    float jump_cooldown_duration;
    float inspector_jump_cooldown_timer;
    float inspector_animation_timer;
    float inspector_animation_duration;

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

        jump_cooldown_timer = 0;
        jump_cooldown_duration = 5.0f;

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

            jump_cooldown_duration /= 1.4f;
        }

        if (gameover_timer > 0.0f)
        {
            gameover_timer -= Time.deltaTime;
            if (gameover_timer <= 0.0f)
            {
                SceneManager.LoadScene("gameover");
            }
        }

        if (spin)
        {
            spin_timer += Time.deltaTime;
            pate_sprite.transform.rotation = Quaternion.Euler(0, 0, spin_timer * 360.0f);
        }

        timer_text.text = string.Format("Aika pakoon: {0:0.00}s", 60.0f - total_timer);

        float color_fade = Mathf.Abs(Mathf.Cos((total_timer / 10.0f) * 180.0f / Mathf.PI));
        timer_text.color = new Color(1.0f, color_fade, 0.0f);

        if (total_timer >= 60.0f && !gameover)
        {
            SceneManager.LoadScene("winscreen");
        }
        
        if (jump_cooldown_timer > 0.0f)
        {
            jump_cooldown_timer -= Time.deltaTime;

            if (jump_cooldown_timer <= 0.0f)
            {
                jump_button.image.color = new Color32(255, 64, 64, 255);
            }
        }

        if (inspector_jump_cooldown_timer > 0.0f)
        {
            inspector_jump_cooldown_timer -= Time.deltaTime;
        }

        if (lamp1.position.x < -400.0f)
        {
            lamp1.position = new Vector2(460.0f, 204.0f);
        }
        if (metro1.position.x < -450.0f)
            metro1.position = new Vector2(750.0f, 58.0f);
        if (metro2.position.x < -450.0f)
            metro2.position = new Vector2(750.0f, 58.0f);

        if (inspector_jump_cooldown_timer <= 0.0f)
        {
            if (metro1.position.x < -200.0f ||
                metro2.position.x < -200.0f)
            {
                inspector_jump_cooldown_timer = inspector_animation_duration;
                inspector_animation_timer = inspector_animation_duration;
            }
        }

        float inspector_jump_offset = 0.0f;
        if (inspector_animation_timer > 0.0f)
        {
            inspector_animation_timer -= Time.deltaTime;

            float angle = (inspector_animation_timer / inspector_animation_duration) * 180.0f;
            inspector_jump_offset = Mathf.Sin(angle * Mathf.Deg2Rad) * 100.0f;
        }

        inspector.transform.position = inspector_base_position + new Vector2(0, inspector_jump_offset);
	}

    void JumpButtonClicked()
    {
        if (jump_cooldown_timer <= 0.0f)
        {
            pate_physics.AddForce(new Vector2(0, 200.0f), ForceMode2D.Impulse);
            jump_cooldown_timer = jump_cooldown_duration;

            jump_button.image.color = new Color32(160, 160, 160, 255);
        }
    }

    void HitTrigger(string collider)
    {
        if (collider == "FloorCollider")
        {
            Vector3 blood_pos = blood.transform.position;
            Vector3 pate_pos = pate.transform.position;
            blood.transform.position = new Vector3(pate_pos.x, blood_pos.y, blood_pos.z);

            blood.SetActive(true);
            gameover_timer = 1.5f;
            gameover = true;
        }
        else
        {
            spin = true;
            gameover_timer = 1.5f;
            gameover = true;
        }
    }
}
