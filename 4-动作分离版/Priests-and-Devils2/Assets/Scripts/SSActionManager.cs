using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback                      //action管理器
{

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //将执行的动作的字典集合,int为key，SSAction为value
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待去执行的动作列表
    private List<int> waitingDelete = new List<int>();                              //等待删除的动作的key                

    public void Update()
    {
        // Debug.Log("Add"+waitingAdd.Count);
        // Debug.Log("Delete"+waitingDelete.Count);
        // Debug.Log("SSAction Manager called RunAction, "+actions.Count);
        // Debug.Log(this.GetInstanceID());
        foreach (SSAction ac in waitingAdd)
        {
            // Debug.Log("Add action");
            actions.Add(ac.GetInstanceID(),ac);                                      //获取动作实例的ID作为key

        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                // Debug.Log("Destory");
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                // Debug.Log("Enable");
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Object.Destroy(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
        // Debug.Log("SSAction Manager called RunAction, "+waitingAdd.Count);

    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        //牧师与魔鬼的游戏对象移动完成后就没有下一个要做的动作了，所以回调函数为空
    }
}
