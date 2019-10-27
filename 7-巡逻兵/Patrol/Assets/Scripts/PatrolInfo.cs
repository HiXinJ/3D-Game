using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolInfo : MonoBehaviour
{
    public int area;                      //标志巡逻兵在哪一块区域
    public bool track = false;    //是否跟随玩家

    public GameObject player;             //玩家游戏对象
    public Vector3 start_position;        //当前巡逻兵初始位置     
}
