using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bank : MonoBehaviour
{
    public static Vector3 rightBankPos = new Vector3(17.5f, 1, 0);
    public static Vector3 leftBankPos = new Vector3(-17.5f, 1, 0);
    GameObject model;
    string path;

    public Bank(Vector3 position)
    {
        
        path = "prefabs/Bank";
        model = Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;
    }
    public void RemoveModel()
    {
        Destroy(model);
        
    }
}
