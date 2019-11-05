using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Devil : Role
{
    public Devil(Vector3 position){
        rightBankPos = position;
        leftBankPos = new Vector3(-rightBankPos.x, 3.5f, 0);
        path = "Prefabs/Devil";
        state = State.right;
        model = Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;   
        Click click =  model.AddComponent(typeof(Click)) as Click;
        isClicked= false;
        click.SetRole(this);
    }
}
