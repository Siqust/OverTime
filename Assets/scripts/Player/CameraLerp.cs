using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Transform[] edges;
    private List<float> clampx;
    private List<float> clampy;
    public float xoffset;
    public float yoffset;
    private void Start()
    {
        clampx = new List<float>() {0f,0f};
        clampy = new List<float>() { 0f, 0f };
        foreach (var e in edges)
        {
            if (e.transform.position.x > clampx[0])
            {
                clampx[0] = e.transform.position.x;
            }
            if (e.transform.position.x < clampx[1])
            {
                clampx[1] = e.transform.position.x;
            }
            if (e.transform.position.y > clampy[0])
            {
                clampy[0] = e.transform.position.y;
            }
            if (e.transform.position.y < clampy[1])
            {
                clampy[1] = e.transform.position.y;
            }
        }
        clampx[0] += xoffset/2;
        clampx[1] -= xoffset/2;
        clampy[0] += yoffset/2;
        clampy[1] -= yoffset/2;
    }
    void Update()
    {
        
        var x = Mathf.Clamp(player.position.x,clampx[0],clampx[1]);
        var y = Mathf.Clamp(player.position.x, clampy[0], clampy[1]);
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -10), speed);

    }
}
