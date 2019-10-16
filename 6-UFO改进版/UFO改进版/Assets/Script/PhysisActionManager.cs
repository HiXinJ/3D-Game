﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisActionManager : SSActionManager, IActionManager
{
    public FirstController sceneController;

    public void Awake(){
        
    }
    protected  void Start()
    {
        sceneController = (FirstController)SceneDirector.GetInstance().CSController;
        sceneController.actionManager = this;
    }

    protected new void Update()
    {
        base.Update();
    }

    public void SendUFO(GameObject UFO)
    {
        PhysisFlyAction flyAction = PhysisFlyAction.GetSSAction(UFO);
        CSequenceAtion actions = CSequenceAtion.GetSSAcition(1, 0, new List<SSAction>{flyAction});
        this.RunAction(UFO, actions , this);
    }
}
