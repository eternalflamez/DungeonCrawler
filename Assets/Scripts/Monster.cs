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
	public float detectRadius;

	// Use this for initialization
	void Start () {
		health = 10;
		damage = 6;
		speed = 3;
		attackSpeed = 1;
		attackRadius = 5;
		lastAttacked = 0;
		detectRadius = 100;
	}
	
	// Update is called once per frame
	void Update () {
		lastAttacked += Time.deltaTime;

		MoveToPlayer();
		AimAtPlayer();
	}

	void MoveToPlayer()
	{
		float xMoved = 0;
		float zMoved = 0;

		float pX = player.transform.position.x;
		float pZ = player.transform.position.z;

		float x = transform.position.x;
		float z = transform.position.z;

		if (pX - speed > x && pX - x < detectRadius)
		{
			xMoved = speed * Time.deltaTime;
		}
		else if (pX + speed < x && x - pX < detectRadius)
		{
			xMoved = -1 * speed * Time.deltaTime;
		}

		if (pZ - speed > z && pZ - z < detectRadius)
		{
			zMoved = speed * Time.deltaTime;
		}
		else if (pZ + speed < z && z - pZ < detectRadius)
		{
			zMoved = -1 * speed * Time.deltaTime;
		}

		Vector3 moveX = new Vector3(xMoved, 0, 0);
		Vector3 moveZ = new Vector3(0, 0, zMoved);

		RaycastHit hit;
		Ray xRay = new Ray(this.transform.position, moveX);
		Ray zRay = new Ray(this.transform.position, moveZ);

		if(xMoved != 0)
		{
			if(!Physics.Raycast(xRay, out hit, Math.Abs(xMoved) + transform.localScale.x / 2))
			{
				transform.position += moveX;
			}
		}

		if(zMoved != 0)
		{
			if(!Physics.Raycast(zRay, out hit, Math.Abs(zMoved) + transform.localScale.z / 2))
			{
				transform.position += moveZ;
			}
		}
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
		Debug.Log ("Trigger enter");
		if (col.tag == "MainCamera") {
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
