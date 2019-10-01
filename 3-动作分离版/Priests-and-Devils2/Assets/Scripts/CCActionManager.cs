using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager
{

    public FirstController sceneController;

    public void Awake(){

        
    }
    protected  void Start()
    {
        Debug.Log(this.GetInstanceID());
        sceneController = (FirstController)SceneDirector.GetInstance().CSController;
        sceneController.actionManager = this;
       
    }

    protected new void Update()
    {
        base.Update();
    }
    public void moveBoat(GameObject boat, Vector3 target, float speed)
    {

        CCMoveToAction moveBoatAction1 = CCMoveToAction.GetSSAction(target, speed);
        //可能有action2,3...
        
        CSequenceAtion actions = CSequenceAtion.GetSSAcition(1, 0, new List<SSAction>{moveBoatAction1});
        this.RunAction(boat, actions, this);
    }

    public void moveRole(GameObject role, Vector3 target, float speed)
    {

        CCMoveToAction moveRoleAction1 = CCMoveToAction.GetSSAction(target,speed);
        //可能有action2,3...

        CSequenceAtion actions = CSequenceAtion.GetSSAcition(1,0, new List<SSAction>{moveRoleAction1});
        this.RunAction(role, actions, this);
    }



}