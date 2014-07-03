using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;

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
    private GameObject fireWall;
    private ArrayList particleSystems;
    private float slowTimerTotal = 2f;
    private float slowTimer = 0;
    private bool slowed = false;
    private int slowSpeedReduction;

	// Use this for initialization
	void Start () {
        
        agent = (NavMeshAgent) GetComponent("NavMeshAgent");

        particleSystems = new ArrayList();
        fireWall = Resources.Load<GameObject>("FireWall");

        if (health == 0)
        {
            health = 20;
        }

        if (damage == 0)
        {
            damage = 12;
        }

        if (speed == 0)
        {
            speed = 5;
        }

        if (attackSpeed == 0)
        {
            attackSpeed = 1;
        }

        if (attackRadius == 0)
        {
            attackRadius = 2;
        }

        if (lastAttacked == 0)
        {
            lastAttacked = 0;
        }

        if (detectRadius == 0)
        {
            detectRadius = 25;
        }
        slowSpeedReduction = (int)speed;

		dead = false;
        lastFramePosition = transform.position;
        agent.speed = this.speed;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!dead)
		{
            if (slowed)
            {
                slowTimer += Time.deltaTime;

                if (slowTimer >= slowTimerTotal)
                {
                    slowed = false;
                    slowTimer = 0;
                    agent.speed += slowSpeedReduction;
                }
            }

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

        ArrayList toRemove = new ArrayList();

        foreach (GameObject item in particleSystems)
        {
            if (item != null)
            {
                item.transform.position = currentFrame + transform.up * 1.3f;
            }
            else
            {
                toRemove.Add(item);
            }
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            particleSystems.Remove(toRemove[i]);
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
		if(col.tag == "Spell")
		{
            float f = float.Parse(col.name.Split('/')[0]);
            health -= f;

            String sElement = col.name.Split('/')[1];

            Element e = (Element)Enum.Parse(typeof(Element), sElement);

            GameObject instance = null;

            switch (e)
            {
                case Element.Fire:
                    instance = (GameObject) Instantiate(fireWall, transform.position + transform.up * 1.3f, new Quaternion());
                    particleSystems.Add(instance);
                    Destroy(instance, 2);
                    break;
                case Element.Ice:
                    if (!slowed)
                    {
                        slowed = true;
                        agent.speed -= slowSpeedReduction;
                    }
                    break;
                case Element.None:
                    break;
            }

			if(health <= 0)
			{
                if (instance != null)
                {
                    Destroy(instance, .5f);
                }

				dead = true;
                agent.stoppingDistance = 0;
                agent.Stop();

                SphereCollider sc = (SphereCollider) this.GetComponent("SphereCollider");
                sc.enabled = false;
			}

			col.tag = "UsedSpell";
		}
	}
}
