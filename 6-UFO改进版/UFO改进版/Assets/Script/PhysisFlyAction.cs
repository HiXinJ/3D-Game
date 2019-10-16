using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PhysisFlyAction : SSAction {
    Vector3 speed;
    Vector3 gravity;
    Vector3 impluseForce;
    public static PhysisFlyAction GetSSAction(GameObject UFO)
    {
        PhysisFlyAction action = ScriptableObject.CreateInstance<PhysisFlyAction>();
        action.speed = UFO.GetComponent<UFOData>().speed;
        action.gravity=UFO.GetComponent<UFOData>().gravity;
        action.impluseForce = UFO.GetComponent<UFOData>().impulseForce;
        return action;
    }
	public override void Start () {
        gameobject.GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Force);
        gameobject.GetComponent<Rigidbody>().AddForce(impluseForce, ForceMode.Impulse);


    }
 
    // Update is called once per frame
    public override void Update () {
        if (gameobject.activeSelf)
        {

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
