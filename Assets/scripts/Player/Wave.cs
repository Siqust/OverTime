using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed=5f;
    public float delete_time=3f;
    public float damage;
    void Start()
    {
        Invoke("Destr", delete_time);
    }
    void Update()
    {
        transform.position += new Vector3(1f,0f,0f) * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy>().health -= damage;
            Destroy(gameObject);
        }
    }
    void Destr()
    {
        Destroy(gameObject);
    }
}
