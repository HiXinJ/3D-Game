using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//River
public class River : MonoBehaviour
{
    public static Vector3 riverPos = new Vector3(0,0,0);
    GameObject model;
    string path;
    public River(Vector3 position){
        path = "prefabs/River";
        model = Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;
    }
    public void RemoveModel()
    {
        Destroy(model);
    }
}
