using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    public float airres;
    public bool dynamic_jump;
    public float jump_force;
    public CheckGround checkGround;
    public float coef;
    private float delay;
    private Animator animator;
    private int side;
    [Header("attack settings")]
    private float attack_timer;
    public float attack_delay;
    public AttackZone atkzone;
    [Header("Dash settings")]
    public float dash_delay;
    public float dash_force = 0f;
    public bool dash_ready;
    [Header("Wave settings")]
    public GameObject wave;
    public bool wave_ready;
    public float wave_reload;
    public Transform wavespawn;

    void Awake()
    {
        dash_ready = true;
        wave_ready = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var xmove = Input.GetAxis("Horizontal");
        if (rb.velocity.y > 0.01)
        {
            animator.SetInteger("State", 2);
        }
        else if (rb.velocity.y < -0.01)
        {
            animator.SetInteger("State", 3);
        }
        else if (Mathf.Abs(xmove) > 0 && checkGround.grounded)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        if (xmove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            side = -1;
        }
        else if (xmove>0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            side = 1;
        }


        if (!checkGround.grounded)
        {
            xmove /= airres;
        }

        attack_timer -= Time.deltaTime;

        if (!(Mathf.Abs(xmove * speed) < Mathf.Abs(rb.velocity.x) && ((xmove * speed>=0 && rb.velocity.x>=0) || (xmove * speed  <= 0 && rb.velocity.x <= 0))))
            rb.velocity = new Vector2(xmove * speed, rb.velocity.y);

    }
    void ReloadDash()
    {
        dash_ready = true;
    }
    void ReloadWave()
    {
        wave_ready = true;
    }
    private void Update()
    {
        if (dash_ready && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(new Vector2(dash_force*side, 0));
            dash_ready = false;
            Invoke("ReloadDash", dash_delay);
        }
        delay -= Time.deltaTime;
        if (checkGround.grounded && Input.GetKey("space") && delay <= 0)
        {
            delay = 0.5f;
            rb.velocity = new Vector2(rb.velocity.x, jump_force);
        }
        if (dynamic_jump)
        {
            if (Input.GetKeyUp("space") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / coef);
            }
        }
        if (Input.GetMouseButtonDown(0) && attack_timer <= 0)
        {
            attack_timer = attack_delay;
            animator.SetTrigger("Attack");
            foreach (var enemy in atkzone.enemies)
            {
                enemy.GetComponent<Enemy>().health -= 1f;
                enemy.GetComponent<Animator>().SetTrigger("Hit");
            }
        }

        if (Input.GetKeyDown("r") && wave_ready)
        {
            var g = Instantiate(wave);
            g.transform.position = wavespawn.transform.position;
            g.transform.rotation = wavespawn.transform.rotation;
            //katana_animator.SetTrigger("Hit");
            Invoke("ReloadWave", wave_reload);
        }
    }
}