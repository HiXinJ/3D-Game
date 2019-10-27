using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;

    //GUIstyle
    GUIStyle style1 = new GUIStyle();
    GUIStyle style2 = new GUIStyle(); 
    GUIStyle buttonStyle ;
    private bool gameStart = false;       //游戏开始

    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        style1.normal.textColor = Color.black;
        style1.fontSize = 31;
        style2.normal.textColor = Color.red;
        style2.fontSize = 31;
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize=29;
    }
	
	void OnGUI ()
    {
        GUI.Label(new Rect(Screen.width/2 - 74, 10, 150, 50), "得分:", style1);
        GUI.Label(new Rect(Screen.width/2 +20, 10, 220, 50), action.GetScore().ToString(), style2);
        //游戏结束
        if (action.GetGameState()==GameState.gameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 96, Screen.height / 2 - 250, 150, 100), "Game Over!", style2);
            GUI.Label(new Rect(Screen.width / 2 - 76, Screen.height / 2 - 200, 150, 50), "得分:", style1);
            GUI.Label(new Rect(Screen.width / 2 +15, Screen.height / 2 - 200, 50, 50), action.GetScore().ToString(), style1);
            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 - 150, 130, 66), "重新开始",buttonStyle))
            {
                action.Restart();
            }
        }
    }

}
