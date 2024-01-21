using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLerp : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Transform[] edges;
    public List<float> clampx;
    public List<float> clampy;
    public float xoffset;
    public float yoffset;
    private void Start()
    {
        clampx = new List<float>() {0f,0f};
        clampy = new List<float>() { 0f, 0f };
        foreach (var e in edges)
        {
            if (e.transform.position.x < clampx[0])
            {
                clampx[0] = e.transform.position.x;
            }
            if (e.transform.position.x > clampx[1])
            {
                clampx[1] = e.transform.position.x;
            }
            if (e.transform.position.y < clampy[0])
            {
                clampy[0] = e.transform.position.y;
            }
            if (e.transform.position.y > clampy[1])
            {
                clampy[1] = e.transform.position.y;
            }
        }
    }
    void Update()
    {
        var minx = clampx[0]+xoffset/2;
        var maxx = clampx[1]-xoffset/2;
        var miny = clampy[0]+yoffset/2;
        var maxy = clampy[1]-yoffset/2;
        var x = Mathf.Clamp(player.position.x,minx,maxx);
        var y = Mathf.Clamp(player.position.y, miny, maxy);
        if (player.position.x > clampx[1] || player.position.x < clampx[0] ||  player.position.y < clampy[0]) { player.GetComponent<Health>().health = 0f; }
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -10), speed);

    }
}
