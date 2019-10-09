using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CCFlyAction : SSAction {
    Vector3 speed;
 
    public static CCFlyAction GetSSAction(GameObject UFO)
    {
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        action.speed = UFO.GetComponent<UFOData>().speed;

        return action;
    }
	public override void Start () {
    }
 
    // Update is called once per frame
    public override void Update () {
        if (gameobject.activeSelf)
        {
            transform.position += speed * Time.deltaTime;
            if (Mathf.Abs(this.transform.position.x) > 20 ||Mathf.Abs( this.transform.position.y) > 13)
            {
                this.destroy = true;
                //通知动作组合对象
                this.callback.SSActionEvent(this);

            }
        }
        else{
            this.callback.SSActionEvent(this);
        }
	}
}