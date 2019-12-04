using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Boat : MonoBehaviour
{
    public GameObject model;
    string path;
    public static Vector3 rightBoatPos = new Vector3(10.3f, 2f, 0f);
    public static Vector3 leftBoatPos = new Vector3(-10.3f, 2f, 0f);
    State state;
    public bool seatable1;
    public bool seatable2;
    int nSeats;
    public bool canMove;
    public Boat(Vector3 position){
        canMove = true;
        state = State.right;
        path = "prefabs/Boat";
        model = Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;
        seatable1 = true;
        seatable2 = true;
        nSeats = 2;
    }

    public void Move(Vector3 direction, int speed){
        model.transform.position+=direction*speed*Time.deltaTime;
    
    }

    public State GetState(){return state;}
    public void SetState(State s){state = s;}

    public Vector3 GetLeftSeatPos(){
        return this.model.transform.position-new Vector3(1, 0, 0);
    }
    public Vector3 GetRightSeatPos(){
        return this.model.transform.position+new Vector3(1, 0, 0);
    }

    public Vector3 GetSeat(){
        if( model.transform.position.x >= Boat.rightBoatPos.x)
        {
            if(seatable1)
                return GetLeftSeatPos();
            else if(seatable2)
                return GetRightSeatPos();
        }   
        else if( model.transform.position.x <= Boat.leftBoatPos.x)
        {
            if(seatable2)
                return GetRightSeatPos();
            else if(seatable1)
                return GetLeftSeatPos();
        }
        
        return new Vector3(0,0,0);
    }
    public void RemoveModel()
    {
        Destroy(model);
    }
}
