using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : SSAction
{
    private enum Dirction { EAST, NORTH, WEST, SOUTH };

    int edge=0;                         //下次移动的边
    private Vector3 distination;        //多边形的定点坐标，作为每次移动的目的地坐标
    private float edgeLength=5;         //多边形的边长
    private float speed = 1.2f;        //移动速度
    private PatrolInfo patrolInfo; 
    
    public static PatrolAction GetAction(Vector3 location)
    {
        PatrolAction action = CreateInstance<PatrolAction>();
        action.distination = location;

        action.speed=Random.Range(0.8f, 1.8f);
        return action;
    }

    public override void Start()
    {
        this.gameobject.GetComponent<Animator>().SetBool("run", true);
        patrolInfo  = this.gameobject.GetComponent<PatrolInfo>();
    }
    
    public override void Update()
    {

        //防止碰撞发生后的旋转
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }            
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        this.transform.LookAt(distination);
        float distance = Vector3.Distance(transform.position, distination);
        transform.position = Vector3.MoveTowards(this.transform.position, distination, speed * Time.deltaTime);
        if (distance < 0.5)
        {
            edge = (edge+1)%4;
            switch(edge)
            {
                case 0:
                    distination.x+=edgeLength;
                    break;
                case 1:
                    distination.z+=edgeLength;
                    break;
                case 2:
                    distination.x-=edgeLength;
                    break;
                case 3:
                    distination.z-=edgeLength;
                    break;
            }
        }
        
        //如果侦察兵需要跟随玩家并且玩家就在侦察兵所在的区域，侦查动作结束
        if (patrolInfo.track 
        &&  SSDirector.GetInstance().CurrentScenceController.GetPlayerArea()== patrolInfo.area
        )
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,0,this.gameobject);
        }
    }
}
