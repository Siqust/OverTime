using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam;
    public float speed = 0.5f;
    public float airres;
    public bool dynamic_jump;
    public float jump_force;
    public CheckGround checkGround;
    public float coef;
    private float delay;
    private Animator animator;
    private int side;
    [Header("Dash settings")]
    public float dash_delay;
    public float dash_force = 5f;
    public bool dash_ready;
    [Header("Bow settings")]
    public GameObject arrow_spawn;
    public float arrow_delay;
    private bool bow_ready;
    void Awake()
    {
        bow_ready = true;
        dynamic_jump = true;
        dash_ready = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var xmove = Input.GetAxis("Horizontal");
        if (rb.velocity.y > 1)
        {
            animator.SetInteger("State", 2);
        }
        else if (rb.velocity.y < -1)
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

        if (dash_ready && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(new Vector2(dash_force*side, 0));
            dash_ready = false;
            Invoke("ReloadDash", dash_delay);
        }
        if (!checkGround.grounded)
        {
            xmove /= airres;
        }

        if (!(Mathf.Abs(xmove * speed) < Mathf.Abs(rb.velocity.x) && ((xmove * speed>=0 && rb.velocity.x>=0) || (xmove * speed <= 0 && rb.velocity.x <= 0))))
            rb.velocity = new Vector2(xmove * speed, rb.velocity.y);
        if (!Input.anyKey)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetMouseButtonDown(0) && bow_ready)
        {
            bow_ready = false;
            RaycastHit2D hit = Physics2D.Raycast(arrow_spawn.transform.position, transform.forward);
            if (hit)
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().health -= 1;
                    hit.transform.GetComponent<Animator>().SetTrigger("Hit");
                }
                Debug.Log(hit.transform.tag);
            }
            Invoke("Reload_bow", arrow_delay);
        }





       delay -= Time.deltaTime;
        if (checkGround.grounded && Input.GetKey("space") && delay<=0)
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
    }
    void ReloadDash()
    {
        dash_ready = true;
    }
    void Reload_bow()
    {
        bow_ready = true;
    }
}