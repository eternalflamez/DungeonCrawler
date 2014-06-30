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
    protected NavMeshAgent agent;
	private bool playedAnimation = false;
    private Vector3 lastFramePosition;

	// Use this for initialization
	void Start () {
        agent = (NavMeshAgent) GetComponent("NavMeshAgent");
		health = 20;
		damage = 12;
		speed = 5;
		attackSpeed = 1;
		attackRadius = 2;
		lastAttacked = 0;
        if (detectRadius == 0)
        {
            detectRadius = 25;
        }
		dead = false;
        lastFramePosition = transform.position;
        agent.speed = this.speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead)
		{
			lastAttacked += Time.deltaTime;
			MoveToPlayer();
		}
		else if(!playedAnimation)
		{
			playedAnimation = true;
			animation.Play("die");
			animation.wrapMode = WrapMode.Once;
			animation.PlayQueued("dead");
		}
	}

	protected void MoveToPlayer()
	{
        Vector3 currentFrame = transform.position;
        float distance = Vector3.Distance(currentFrame, lastFramePosition);

        lastFramePosition = currentFrame;
        float currentSpeed = Mathf.Abs(distance) / Time.deltaTime;

        if (Vector3.Distance(player.transform.position, transform.position) < detectRadius)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) < 4 && !AimAtPlayer())
            {
                agent.SetDestination(player.transform.position);

                if (currentSpeed > 0.1)
                {
                    animation.Play("walk");
                }
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }

        
        if (currentSpeed <= 0.1)
        {
            animation.PlayQueued("idle");
        }
	}

	protected bool AimAtPlayer()
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

        if (pX > x - attackRadius && pX < x + attackRadius && pZ > z - attackRadius && pZ < z + attackRadius)
        {
            return true;
        }
        return false;
	}

	public virtual void OnTriggerEnter(Collider col)
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
                agent.SetDestination(transform.position);
                agent.speed = 0;

                SphereCollider sc = (SphereCollider) this.GetComponent("SphereCollider");
                sc.enabled = false;
			}

			col.tag = "UsedSpell";
		}
	}
}
