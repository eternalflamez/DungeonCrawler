using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public GUIStyle customButton;

	void OnGUI()
	{
        if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 4, Screen.width / 5, Screen.height / 10), "New Game", customButton))
		{
			Application.LoadLevel("Level1");
		}
		if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 2.65f, Screen.width / 5, Screen.height / 10), "Tutorial", customButton))
		{
			Application.LoadLevel("Tutorial");
		}
		if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 2, Screen.width / 5, Screen.height / 10), "Exit Game", customButton))
		{
			Application.Quit();
		}
	}
}
