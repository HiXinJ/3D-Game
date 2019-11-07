using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particles;
    public float t = 0;
    public float a = 1;
    public float speed = 10;
    public float dt = 3;
    void Start()
    {     
    }
    void Update()
    {
        dt += Time.deltaTime;
        if (dt > 3 && dt <8)
        {
            a = 25;
        }
        else if (dt >8 && dt < 11)
        { 
            a = 8; 
        }
        else if (dt >11)
        {
            dt = 3;
        }
        t = Time.time * speed;
        float x = a * Mathf.Pow(Mathf.Cos(t), 3);
        float y = a * Mathf.Pow(Mathf.Sin(t), 3);
        particles.transform.position = new Vector3(y, x, 0);
    }
}
