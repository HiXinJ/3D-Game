﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    bool clickGo;
    bool arrived;
    GameState gameState;
    ISceneController action;
    // Start is called before the first frame update
    void Start()
    {
        clickGo = false;
        arrived = false;
        gameState = GameState.playing;
        action = SceneDirector.GetInstance().CSController=new SceneController();
        action.LoadResources();
    }

    // Update is called once per frame
    void Update()
    {
        if(clickGo)
            gameState = SceneDirector.GetInstance().CSController.MoveAll(out arrived);
        if(arrived)
            clickGo = false;
    }
    void OnGUI()
    {
        GUIStyle button_style;


        if(gameState == GameState.playing)
        {    
            if(GUI.Button (new Rect(Screen.width/2.1f, Screen.height/8, 150,70), "Go",button_style))
                clickGo=true;
        }
        else if(gameState == GameState.win)
        {
            if(GUI.Button (new Rect(Screen.width/2.4f, Screen.height/3.5f, 450,170), "You Won \nplay again",button_style))
            {
                clickGo = false;
                arrived = false;
                gameState = GameState.playing;
                action.DestoryResources();
                action.LoadResources();
            }
        }   
        else if(gameState == GameState.defeat)
        {
            if(GUI.Button (new Rect(Screen.width/2.4f, Screen.height/3.5f, 450,170), "You Lost\nplay again",button_style))
            {
                clickGo = false;
                arrived = false;
                gameState = GameState.playing;
                action.DestoryResources();
                action.LoadResources();
            }            
        }
    }
}
