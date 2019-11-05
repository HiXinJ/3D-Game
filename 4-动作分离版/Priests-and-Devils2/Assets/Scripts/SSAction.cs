using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject      
{
    public bool enable = true;                      //是否正在进行此动作
    public bool destroy = false;                    //是否需要被销毁

    public GameObject gameobject;                   //动作对象
    public Transform transform;                     
    public ISSActionCallback callback;              

    protected SSAction() { }                        //防止用户自己new对象

    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}
