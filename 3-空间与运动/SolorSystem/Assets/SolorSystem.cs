using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolorSystem : MonoBehaviour
{
    Vector3 sun;
    // Start is called before the first frame update
    void Start()
    {
        sun = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Mercury").transform.RotateAround(sun, new Vector3(0, 1, 0.2f), 30 * Time.deltaTime);
        GameObject.Find("Mercury").transform.Rotate(Vector3.up * Time.deltaTime * 1080);

        GameObject.Find("Venus").transform.RotateAround(sun, new Vector3(0, 1, 0.1f), 25 * Time.deltaTime);
        GameObject.Find("Venus").transform.Rotate(Vector3.up * Time.deltaTime * 300);

        GameObject.Find("Earth").transform.RotateAround(sun, new Vector3(0, 1, 0), 35 * Time.deltaTime);
        GameObject.Find("Earth").transform.Rotate(Vector3.up * Time.deltaTime * 360);
        GameObject.Find("Moon").transform.Rotate(Vector3.up*500*Time.deltaTime);


        GameObject.Find("Mars").transform.RotateAround(sun, new Vector3(0, 1, 0.5f), 50 * Time.deltaTime);
        GameObject.Find("Mars").transform.Rotate(Vector3.up * Time.deltaTime * 360);

        GameObject.Find("Jupiter").transform.RotateAround(sun, new Vector3(0.5f, 1, 0), 35 * Time.deltaTime);
        GameObject.Find("Jupiter").transform.Rotate(Vector3.up * Time.deltaTime * 800);

        GameObject.Find("Saturn").transform.RotateAround(sun, new Vector3(0, 1, 2), 45* Time.deltaTime);
        GameObject.Find("Saturn").transform.Rotate(Vector3.up * Time.deltaTime * 810);

        GameObject.Find("Uranus").transform.RotateAround(sun, new Vector3(0, 1, 0.3f), 50 * Time.deltaTime);
        GameObject.Find("Uranus").transform.Rotate(Vector3.up * Time.deltaTime * 1000);

        GameObject.Find("Neptune").transform.RotateAround(sun, new Vector3(0.1f, 1, 0.2f), 60 * Time.deltaTime);
        GameObject.Find("Neptune").transform.Rotate(Vector3.up * Time.deltaTime * 1000);
    }



}
