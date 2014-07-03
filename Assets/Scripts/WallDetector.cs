using UnityEngine;
using System.Collections;

public class WallDetector : MonoBehaviour {
    float speed;
    Vector3 direction;
    GameObject particles = null;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit wallHit;
        Ray wallDetector = new Ray(this.transform.position, speed * direction * Time.deltaTime);

        if (Physics.Raycast(wallDetector, out wallHit, 1f))
        {
            if (wallHit.collider.gameObject.CompareTag("Environment"))
            {
                this.tag = "UsedSpell";
                if (particles != null)
                {
                    GameObject instance = (GameObject)Instantiate(particles, transform.position + speed * direction * Time.deltaTime, new Quaternion());
                    Destroy(instance, 2);
                }
            }
        }
	}

    void setSpeed(float speed)
    {
        this.speed = speed;
    }

    void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    void setParticles(string particlesName)
    {
        if (particlesName != "")
        {
            this.particles = Resources.Load<GameObject>(particlesName);
        }
    }
}
