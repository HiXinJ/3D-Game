using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollide : MonoBehaviour
{
    //游戏结束事件代理
    public delegate void GameOverEvent();
    public static event GameOverEvent gameOverHandle;
    public void SendGameOverEvent()
    {
        if (gameOverHandle != null)
        {
            gameOverHandle();
        }
    }
    void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.tag == "Player")
        {
           SendGameOverEvent();
        }
    }
}
