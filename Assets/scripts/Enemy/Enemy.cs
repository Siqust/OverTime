using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform zone;
    public EnemyZone reach;
    public float damage;
    public float delay;
    public Transform raycast;
    public float speed;
    public Transform player;
    public float health;
    private bool ready_to_attack;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        ready_to_attack = true;
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            //�������� ������
            Invoke("Death", 2f);
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) < zone.localScale.x && reach.player==null && ready_to_attack)
            {
                var side = 1f;
                if (transform.position.x < player.transform.position.x) {
                    transform.eulerAngles = new Vector2(0, 180);
                    rb.velocity = new Vector2(side * speed,rb.velocity.y);
                } else {
                    transform.eulerAngles = new Vector2(0, 0);
                    rb.velocity = new Vector2(-side * speed,rb.velocity.y);
                }
            }
            if (reach.player != null && ready_to_attack==true)
            {
                //�������� �����
                ready_to_attack = false;
                Invoke("Attack", delay);
            }
        }
    }
    void Attack()
    {
        if (reach.player != null)
        {
            reach.player.GetComponent<Health>().health -= damage;
        }
        ready_to_attack = true;
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
