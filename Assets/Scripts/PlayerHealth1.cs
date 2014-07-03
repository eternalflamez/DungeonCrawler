using UnityEngine;
using System.Collections;

public class PlayerHealth1 : MonoBehaviour 
{
	
	public int maxHealth = 100;
	public int curHealth = 100;
	public float healthBarLength;
	public float healthBar;
	// Use this for initialization
	void Start () 
	{   
		healthBarLength = Screen.width / 2; 
		healthBar = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		AdjustcurHealth (0);
	}
	
	void OnGUI()
	{
		Texture2D t = new Texture2D(1,1);
		t.SetPixel(0, 0, Color.red);
		t.wrapMode = TextureWrapMode.Repeat;
		t.Apply();

		GUIStyle g = new GUIStyle();
		g.normal.background = t;

		//GUI.Box (new Rect (10, 10, healthBarLength, 20), curHealth, "/", maxHealth);
		GUI.Box(new Rect(10, 10, healthBarLength, 20), "", g);
		GUI.Box(new Rect(10, 10, healthBar, 20), curHealth + "/" + maxHealth);
	}
	
	public void AdjustcurHealth(int adj)
	{
		//curHealth += adj;

        if (curHealth < 0)
        {
            curHealth = 0;
            Application.LoadLevel("Dead");
        }
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = (Screen.width/2) * (curHealth / (float)maxHealth);
	}
}