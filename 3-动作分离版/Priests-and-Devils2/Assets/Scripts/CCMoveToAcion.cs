using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public Vector3 target;        //移动到的目的地
    public float speed;           //移动的速度

    private CCMoveToAction() { }

    // 调用CreateInstance创建动作类，确保内存正确回收
    public static CCMoveToAction GetSSAction(Vector3 target, float speed)
    {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        // Debug.Log("CCMoveToAction Update");
        // if (System.Math.Abs( this.transform.position.x - target.x) <0.8)
        if(this.transform.position == target)
        {
            this.destroy = true;
            //通知动作组合对象
            this.callback.SSActionEvent(this);      
        }
    }

    public override void Start(){
        // Debug.Log("MoveToAction Start");
        // Debug.Log("target"+target);
    }
}

