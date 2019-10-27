using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollide : MonoBehaviour
{

    public int area=-1;
    FirstSceneController sceneController;
    private void Start()
    {
        sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
    }
    void OnTriggerEnter(Collider collider)
    {
        //标记玩家进入自己的区域
        if (collider.gameObject.tag == "Player")
        {
            if(sceneController==null)
            {        
                sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
            }
            sceneController.playerArea = area;
        }
    }
}
