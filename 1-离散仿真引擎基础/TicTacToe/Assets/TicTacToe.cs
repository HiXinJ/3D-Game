using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
	
	private int[,] board = new int[3, 3];
	private string[]result=new string[3]{"平局","X获胜","O获胜"};
	private int turns = 0;//odd:X, even:O
	private int winner = -1;	//-1:gaming  0:tie  1: x wins  2: O wins 
	private int width = 60;
	// Use this for initialization

	void Start ()
	{
		turns = 0;
		winner = -1;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				board [i, j] = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
		//Set the GUIStyle style to be label
        GUIStyle style = GUI.skin.GetStyle ("label");
        style.fontSize =32;
        

		winner = Judge ();
		if (winner != -1)
			GUI.Label (new Rect (160, 80,150, 50), result[winner]);
	
		
		DrawChessBoard ();
		if (winner == -1) {
			if (turns % 2 == 1)
				GUI.Label (new Rect (160, 80, 150, 50), "Player: X");
			else
				GUI.Label (new Rect (160, 80, 150, 50), "Player: 0");
		}
		if (GUI.Button (new Rect (0, 10, 150, 30), "重新开始"))
			Start ();
	}

	private bool DrawChessBoard ()
	{
		GUIStyle style = GUI.skin.GetStyle ("button");
        style.fontSize =30;
        
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++) {
				if (board [i, j] == 1) {
					GUI.Button (new Rect (120 + i * width, 150 + j * width, width, width), "X");
				} else if (board [i, j] == 2) {
					GUI.Button (new Rect (120 + i * width, 150 + j * width, width, width), "O");
				} else if (GUI.Button (new Rect (120 + i * width, 150 + j * width, width, width), "")) {
					if (winner != -1)
						continue;
					else if (turns % 2 == 1) {
						board [i, j] = 1;
						turns++;
					} else if (turns % 2 == 0) {
						board [i, j] = 2;
						turns++;
					}
				}
			}
		return true;
	}

	private int Judge ()
	{

		if (board[0,0] != 0 && board [0, 0] == board [1, 1] && board [2, 2] == board [1, 1])
			return board [0, 0];
		
		if (board[0,2] != 0 && board [0, 2] == board [1, 1] && board [2, 0] == board [1, 1])
			return board [0, 2];
		
		for (int i = 0; i < 3; i++)
			if (board[i,0] != 0 && board [i, 0] == board [i, 1] && board [i, 2] == board [i, 1])
				return board [i, 0];
		
		for (int i = 0; i < 3; i++)
			if (board[0,i] != 0 && board [0, i] == board [1, i] && board [2, i] == board [1, i])
				return board [0, i];
		
		if (turns == 9)
			return 0;
		return -1;
	}
}