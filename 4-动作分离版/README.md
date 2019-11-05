## 编程实践

牧师与魔鬼 动作分离版  
【2019新要求】：设计一个裁判类，当游戏达到结束条件时，通知场景控制器游戏结束  

动作分离版是将角色的一个动作拆分为多个动作，例如角色上船时向右运动，再向下运动，这两个动作通过组合模式组合在一起，这种分离符合松耦合的设计原则。

### 1、动作基类Action  
 
```cs
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
```

### 2、简单动作实现 
这是一个被组合对象，实现某一个动作。

```cs
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
```

### 3、组合动作对象  
有一个List\<SSAction\>用于组合动作  
这是标准的组合设计模式。被组合的对象和组合对象属于同一种类型。通过组合模式， 我们能实现几乎满足所有越位需要、非常复杂的动作管理。
```cs
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
        source.destroy = false;          //先保留这个动作，如果是无限循环动作组合之后还需要使用
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
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
```

### 4、动作事件接口定义 

```cs
public enum SSActionEventType : int { Started, Competeted }

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}
```

### 5、动作管理基类

```cs
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

```


### 6、实战动作管理   

- 该类的职责
    - 接收场景控制的命令 - 管理动作的自动执行
- 场景控制器与动作管理器的关系
    - 建议场景控制器在 start 中用 GetComponent\<T\> () 将
它作为场景管理的一员
    - 后面的代码主要是演示动作管理。加入场景过程比较奇 葩!
    - 动作管理器不应该有模型的知识，游戏对象信息必须由 场景控制器提供

```cs
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
```

### 7、裁判类  

检测游戏状态，将状态发送给userGUI。如果gameState为defeat，则显示You Lost, 游戏结束。
```cs
public class Judge :MonoBehaviour
{
    UserGUI userGUI;
    public void Start(){
        userGUI = GetComponent<UserGUI>() ;
    }
     
    public void Update(){
            userGUI.gameState = SceneDirector.GetInstance().CSController.Defeat();

    }
}
```

详细代码在[Priests-and-Devils2/Assets/Scripts](https://github.com/HiXinJ/3D-Game/tree/master/3-动作分离版/Priests-and-Devils2/Assets/Scripts)