using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State{left, right, onBoat1, onBoat2, BoatMoving};

public class Role : MonoBehaviour
{
    public GameObject model;
    protected State state;
    public string path;
    public bool isClicked;
    public static bool clickable = true;
    public Vector3 leftBankPos;
    public Vector3 rightBankPos;
    public void Move(Vector3 direction, int speed){
        model.transform.position +=  speed * direction * Time.deltaTime;
        
    }
    public State GetState(){return state;}
    public void SetState(State s){state = s;}
    public void RemoveModel()
    {
        Destroy(model);
    }
}