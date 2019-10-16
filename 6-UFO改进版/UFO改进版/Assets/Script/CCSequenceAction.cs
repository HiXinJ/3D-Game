using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CSequenceAtion : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;    //被组合动作对象
    public int repeat = -1;            //-1 无线循环
    public int start = 0;              

    public static CSequenceAtion GetSSAcition(int repeat, int start, List<SSAction> sequence)
    {
        CSequenceAtion action = ScriptableObject.CreateInstance<CSequenceAtion>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update()
    {
        // Debug.Log("CCSequenceAction Update");
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();     //一个组合中的一个动作执行完后会调用接口,所以这里看似没有start++实则是在回调接口函数中实现
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        Debug.Log("ss event");
        source.destroy = false;          //先保留这个动作，如果是无限循环动作组合之后还需要使用
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                source.gameobject.GetComponent<Rigidbody>().isKinematic=true;
                source.gameobject.GetComponent<Rigidbody>().isKinematic=false;
                this.destroy = true;               //整个组合动作就删除
                this.callback.SSActionEvent(this); //告诉组合动作的管理对象组合做完了
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameobject = this.gameobject;
            action.transform = this.transform;
            action.callback = this;                //组合动作的每个小的动作的回调是这个组合动作
            action.Start();
        }
    }

    void OnDestroy()
    {
        //如果组合动作做完第一个动作突然不要它继续做了，那么后面的具体的动作需要被释放
    }
}