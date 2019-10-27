using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UFOData : MonoBehaviour {
 
    public Color color;
    public Vector3 speed;

    // 物理运动
    public Vector3 impulseForce;

    // 物理运动
    public Vector3 gravity;
    // 击落UFO的得分
    public int score;
}