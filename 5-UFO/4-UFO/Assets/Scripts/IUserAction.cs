using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction                              
{

    void StartGame();
    void Hit(Vector3 pos);
    //获得分数
    int GetScore();
    int GetLife();
    //游戏结束
    void GameOver();
    //游戏重新开始
    void ReStart();


}