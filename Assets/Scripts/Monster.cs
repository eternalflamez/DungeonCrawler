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
	public bool dead;
	private bool playedAnimation = false;

	// Use this for initialization
	void Start () {
		health = 20;
		damage = 6;
		speed = 3;
		attackSpeed = 1;
		attackRadius = 5;
		lastAttacked = 0;
		detectRadius = 25;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead)
		{
			lastAttacked += Time.deltaTime;
			MoveToPlayer();
			AimAtPlayer();
		}
		else if(!playedAnimation)
		{
			playedAnimation = true;
			animation.Play("die");
			animation.wrapMode = WrapMode.Once;
			animation.PlayQueued("dead");
		}
	}

	void MoveToPlayer()
	{
		float xSpeed = 0;
		float zSpeed = 0;

		float pX = player.transform.position.x;
		float pZ = player.transform.position.z;

		float x = transform.position.x;
		float z = transform.position.z;

		bool testXMoved = false;

		if (pX - speed > x && pX - x < detectRadius)
		{
			xSpeed = speed * Time.deltaTime;
			testXMoved = true;
		}
		else if (pX + speed < x && x - pX < detectRadius)
		{
			xSpeed = -1 * speed * Time.deltaTime;
			testXMoved = true;
		}

		if(testXMoved)
		{
			if (pZ - speed > z && pZ - z < detectRadius)
			{
				zSpeed = speed * Time.deltaTime;
			}
			else if (pZ + speed < z && z - pZ < detectRadius)
			{
				zSpeed = -1 * speed * Time.deltaTime;
			}
		}

		Vector3 moveX = new Vector3(xSpeed, 0, 0);
		Vector3 moveZ = new Vector3(0, 0, zSpeed);

		RaycastHit hit;

		Vector3 faceRayStartPos = new Vector3(
				transform.position.x - xSpeed, 
				transform.position.y + 3, 
				transform.position.z - zSpeed);

		Ray xRay = new Ray(faceRayStartPos, new Vector3(moveX.x * 2, 3, 0));
		Ray zRay = new Ray(faceRayStartPos, new Vector3(0, 3, moveZ.z * 2));

		Vector3 debugRay = new Vector3(
				faceRayStartPos.x - xSpeed * 30, 
				faceRayStartPos.y, 
				faceRayStartPos.z - zSpeed * 30);

		Debug.DrawRay(debugRay, new Vector3(moveX.x * 31, 0, 0), Color.black);
		Debug.DrawRay(debugRay, new Vector3(0, 0, moveZ.z * 31), Color.red);

		if(xSpeed != 0)
		{
			if(!Physics.Raycast(xRay, out hit, Math.Abs(xSpeed)))
			{
				transform.position += moveX;
			}
		}

		if(zSpeed != 0)
		{
			if(!Physics.Raycast(zRay, out hit, Math.Abs(zSpeed)))
			{
				transform.position += moveZ;
			}
		}

		if(xSpeed != 0 || zSpeed != 0)
		{
			animation.Play("walk");
		}
		else
		{
			animation.PlayQueued("idle");
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

				System.Random r = new System.Random();

				animation.Play("hit" + r.Next(1, 3), PlayMode.StopAll);

				player.SendMessage("AdjustcurHealth", damage * -1);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "MainCamera") {
			// Destroy(col.gameObject.CubePlac);
		}
		else if(col.tag == "Spell")
		{
			health -= float.Parse(col.name);

			if(health <= 0)
			{
				dead = true;
			}

			col.tag = "UsedSpell";
		}
	}
}
