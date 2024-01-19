using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private float xmove;
    public float jump_force;
    public CheckGround checkGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xmove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xmove * speed * Time.deltaTime, 0);
    }
}