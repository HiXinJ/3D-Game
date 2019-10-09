using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour
{
    public GameObject UFOPrefab;

    private List<UFOData> used = new List<UFOData>();
    private Queue<UFOData> free = new Queue<UFOData>();

    private void Awake()
    {
        UFOPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
        UFOPrefab.SetActive(false);
    }


    public GameObject GetUFO(int round)
    {
        GameObject newUFO = null;
        if (free.Count > 0)
        {
            newUFO = free.Dequeue().gameObject;
        }
        else
        {
            newUFO = GameObject.Instantiate<GameObject>(UFOPrefab, Vector3.zero, Quaternion.identity);
            newUFO.AddComponent<UFOData>();
        }

        Color UFOColor = Color.yellow;
        Vector3 UFOSpeed;

        if (round == 1)
        {
            UFOSpeed = new Vector3(10,-3f,0);
            UFOColor = Color.yellow;
        }
        else if (round == 2)
        {
            UFOSpeed = new Vector3(Random.Range(12f,18f), Random.Range(-3f,-13f),0);
            UFOColor = Random.Range(0, 10000) % 2 == 0 ? Color.yellow : Color.green;
        }
        else
        {
            int n = Random.Range(0, 10000) % 4;
            UFOSpeed = new Vector3(Random.Range(16f,25f), Random.Range(-3f,-20f), 0);
            switch (n)
            {
                case 0:
                    UFOColor = Color.yellow;
                    break;
                case 1:
                    UFOColor = Color.green;
                    break;
                case 2:
                    UFOColor = Color.red;
                    break;
                case 3:
                    UFOColor = Color.red;
                    break;
            }
        }
        newUFO.GetComponent<UFOData>().color = UFOColor;
        newUFO.GetComponent<Renderer>().material.color = UFOColor;
        newUFO.GetComponent<UFOData>().score = round;
        int X = Random.Range(0,100) % 2 == 0? -1 : 1;
        
        newUFO.GetComponent<UFOData>().speed.x = UFOSpeed.x;
        newUFO.GetComponent<UFOData>().speed.y = UFOSpeed.y;


        Vector3 position = new Vector3(19,Random.Range(5f,10f),0);
        int dire =(Random.Range(0,100)%2 == 0? 1:-1);
        position.x*=dire;
        newUFO.transform.position = position;
        newUFO.GetComponent<UFOData>().speed.x *= -dire;

        used.Add(newUFO.GetComponent<UFOData>());

        return newUFO;
    }

    public void FreeUFO(GameObject UFO)
    {
        foreach (UFOData i in used)
        {
            if (UFO.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                i.gameObject.SetActive(false);
                free.Enqueue(i);
                used.Remove(i);
                break;
            }
        }
    }
}