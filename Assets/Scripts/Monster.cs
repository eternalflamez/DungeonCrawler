using UnityEngine;
using System.Collections;
using System;

public class Monster : MonoBehaviour {
	public GameObject player;
	public float health;
	public float damage;
	public float speed;
	public float attackSpeed;
	public float lastAttacked;
	public float attackRadius;

	// Use this for initialization
	void Start () {
		health = 10;
		damage = 6;
		speed = 3;
		attackSpeed = 1;
		attackRadius = 5;
		lastAttacked = 0;
	}
	
	// Update is called once per frame
	void Update () {
		lastAttacked += Time.deltaTime;

		MoveToPlayer();
		AimAtPlayer();
	}

	void MoveToPlayer()
	{
		float pX = player.transform.position.x;
		float pZ = player.transform.position.z;

		float x = transform.position.x;
		float z = transform.position.z;

		if (pX - speed > x)
		{
			x += speed * Time.deltaTime;
		}
		else if (pX + speed < x)
		{
			x -= speed * Time.deltaTime;
		}

		if (pZ - speed > z)
		{
			z += speed * Time.deltaTime;
		}
		else if (pZ + speed < z)
		{
			z -= speed * Time.deltaTime;
		}

		transform.position = new Vector3(x, transform.position.y, z);
	}

	void AimAtPlayer()
	{
		float pX = player.transform.position.x;
		float pZ = player.transform.position.z;
		
		float x = transform.position.x;
		float z = transform.position.z;

		if(lastAttacked > attackSpeed)
		{
			if (pX > x - attackRadius && pX < x + attackRadius && pZ > z - attackRadius && pZ < z + attackRadius)
			{
				// ATTACK! 
				lastAttacked = 0;

				player.SendMessage("AdjustcurHealth", damage * -1);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "MainCamera") {
			Debug.Log ("Trigger enter");
			// Destroy(col.gameObject.CubePlac);
		}
		else if(col.tag == "Spell")
		{
			health -= float.Parse(col.name);
			
			Debug.Log(col.name);
			if(health <= 0)
			{
				Destroy(this.gameObject);
			}

			col.tag = "UsedSpell";
		}
	}
	
	//basic collision
	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("Collision enter");
		if (col.gameObject.tag == "MainCamera")
		{
			Debug.Log ("Collision C");
		}
	}

}
