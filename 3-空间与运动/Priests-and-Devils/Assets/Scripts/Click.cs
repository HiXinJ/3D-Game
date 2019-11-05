using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    Role role;

    public Role GetRole(){
        return role;
    }

    public void SetRole(Role value){
        role = value;
    }
    public void OnMouseDown()
    {
        if(Role.clickable)
        {
            Role.clickable = false;
            role.isClicked = true;
        }   
    }

    void Update(){
       if(role.isClicked)
            SceneDirector.GetInstance().CSController.Move(role);
    }
}
