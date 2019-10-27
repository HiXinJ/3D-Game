using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    //加载场景资源
    void LoadResources();
    int GetPlayerArea();
}

public interface IUserAction                          
{

    //得到分数
    int GetScore();


    //得到游戏结束标志
    GameState GetGameState();
    //重新开始
    void Restart();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,int intParam = 0,GameObject objectParam = null);
}

public interface IGameStatusOp
{
    void PlayerEscape();
    void PlayerGameover();
}
