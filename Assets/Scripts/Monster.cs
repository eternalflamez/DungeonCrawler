using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	public GameObject player;
	public float health;
	public float damage;
	public float speed;

	// Use this for initialization
	void Start () {
		health = 10;
		damage = 6;
		speed = 3;
	}
	
	// Update is called once per frame
	void Update () {
		MoveToPlayer();
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

	}
}
