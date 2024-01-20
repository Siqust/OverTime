using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed=5f;
    public float delete_time=3f;
    void Start()
    {
        Invoke("Destr", delete_time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * -speed * Time.deltaTime;
    }
    void Destr()
    {
        Destroy(gameObject);
    }
}
