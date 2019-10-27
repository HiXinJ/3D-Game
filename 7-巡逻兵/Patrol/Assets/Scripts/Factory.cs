using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private Vector3[] pos0 = new Vector3[9];            //巡逻兵开始巡逻的坐标
    private List<GameObject> patrolsList = new List<GameObject>();        //正在被使用的巡逻兵                     
    private GameObject patrol = null;   
    
    public List<GameObject> GetPatrols()
    {
        int[] initPositionX = { 12, 2, -6 };
        int[] initPositionY = { 6, -4, -14 };
        for(int i=0;i < 9;i++)
        {
            pos0[i] = new Vector3(initPositionX[i/3], 0, initPositionY[i%3]);
        }
        for(int i=0; i < 9; i++)
        {
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
            patrol.AddComponent<PatrolInfo>();
            patrol.gameObject.GetComponent<Collider>().enabled=true;
            patrol.transform.position = pos0[i];

            patrol.GetComponent<PatrolInfo>().area = i + 1;
            patrol.GetComponent<PatrolInfo>().start_position = pos0[i];
            patrolsList.Add(patrol);
        }   
        return patrolsList;
    }
}
