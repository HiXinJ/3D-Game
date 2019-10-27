using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
  //玩家逃脱巡逻兵追踪事件代理
    public delegate void EscapeEvent();
    public static event EscapeEvent escapeHandle;
    public void SendPlayerEscapeEvent()
    {
        if (escapeHandle != null)
        {
            escapeHandle();
        }
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            // Debug.Log("Trigger");
            this.gameObject.transform.parent.GetComponent<Animator>().SetBool("run",true);
            this.gameObject.transform.parent.GetComponent<PatrolInfo>().player=collider.gameObject;
            this.gameObject.transform.parent.GetComponent<PatrolInfo>().track=true;
            this.GetComponent<BoxCollider>().size=new Vector3(7,7,1);
        }
    }
    void OnTriggerExit(Collider collider)
    {
 
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("PatrollCollode: exit");
            this.gameObject.transform.parent. GetComponent<Animator>().SetBool("run",false);
            
            this.gameObject.transform.parent.GetComponent<PatrolInfo>().track = false;
            this.gameObject.transform.parent.GetComponent<PatrolInfo>().player = null;
            this.GetComponent<BoxCollider>().size=new Vector3(5,5,1);
            SendPlayerEscapeEvent();

        }
    }
}
