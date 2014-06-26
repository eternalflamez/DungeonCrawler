using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	void OnGUI()
	{

		if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 4, Screen.width / 5, Screen.height / 10), "New Game"))
		{
			Application.LoadLevel(1);
		}
		if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 2.65f, Screen.width / 5, Screen.height / 10), "Tutorial"))
		{
			Application.LoadLevel(2);
		}
		if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height / 2, Screen.width / 5, Screen.height / 10), "Exit Game"))
		{
			Application.Quit();
		}

	}
}
