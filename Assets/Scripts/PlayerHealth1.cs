using UnityEngine;
using System.Collections;

public class PlayerHealth1 : MonoBehaviour {
	
	public int maxHealth = 100;
	public int curHealth = 100;
	

	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {   
		 
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurrentHealth(0);
	}
	

	
	public void AddjustCurrentHealth(int adj){
		
		curHealth += adj;
		
		if(curHealth <0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth <1)
			maxHealth = 1;

		if (curHealth == 0){
			//destroy player
		}
		
		healthBarLength =(Screen.width /2) * (curHealth / (float)maxHealth);
	}
}