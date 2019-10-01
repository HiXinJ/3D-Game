using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge :MonoBehaviour
{
    UserGUI userGUI;
    public void Start(){
        userGUI = GetComponent<UserGUI>() ;
    }
     
    public void Update(){
            userGUI.gameState = SceneDirector.GetInstance().CSController.Defeat();
        
        
    }
}
