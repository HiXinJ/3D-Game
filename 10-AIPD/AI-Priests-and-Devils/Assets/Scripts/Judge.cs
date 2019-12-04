using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge :MonoBehaviour
{
    UserGUI userGUI;
    int frame = 0;
    public void Start(){
        userGUI = GetComponent<UserGUI>() ;
    }
     
    public void Update(){
        frame++;
        if(frame % 100 ==0)
            userGUI.gameState = SceneDirector.GetInstance().CSController.Defeat();

    }
}
