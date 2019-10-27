using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAction : SSAction
{
    private float speed = 1.7f;            //跟随玩家的速度
    private GameObject player;           //玩家
    private PatrolInfo patrolInfo;       //侦查兵数据

    public static TrackAction GetAction(GameObject player)
    {
        TrackAction action = CreateInstance<TrackAction>();
        action.player = player;
        return action;
    }

    public override void Start()
    {
        patrolInfo = this.gameobject.GetComponent<PatrolInfo>();
    }
    public override void Update()
    {
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
         
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);  
        if (!patrolInfo.track 
        || SSDirector.GetInstance().CurrentScenceController.GetPlayerArea() != patrolInfo.area)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,1,this.gameobject);
        }
    }
}
