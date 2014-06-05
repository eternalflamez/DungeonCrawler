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
		health = 30;
		damage = 6;
		speed = 3;
		attackSpeed = 1;
		attackRadius = 5;
		lastAttacked = 0;
		detectRadius = 100;
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

		Vector3 faceRay = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);

		Ray xRay = new Ray(faceRay, new Vector3(moveX.x, 3, 0));
		Ray zRay = new Ray(faceRay, new Vector3(0, 3, moveZ.z));

		Debug.DrawRay(faceRay, new Vector3(moveX.x + 2, 0, 0), Color.black);
		Debug.DrawRay(faceRay, new Vector3(0, 0, moveZ.z + 1), Color.red);

		if(xMoved != 0)
		{
			if(!Physics.Raycast(xRay, out hit, Math.Abs(xMoved)))
			{
				transform.position += moveX;
			}
		}

		if(zMoved != 0)
		{
			if(!Physics.Raycast(zRay, out hit, Math.Abs(zMoved)))
			{
				transform.position += moveZ;
			}
		}

		if(xMoved != 0 || zMoved != 0)
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
