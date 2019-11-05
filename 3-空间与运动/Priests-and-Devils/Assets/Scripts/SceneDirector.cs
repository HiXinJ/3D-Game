using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : System.Object
{
    private static SceneDirector _instance;             //导演类的实例
    public ISceneController CSController { get; set; }
    public static SceneDirector GetInstance()           
    {

        if (_instance == null)
        {
            _instance = new SceneDirector();
        }
        return _instance;
    }
}
