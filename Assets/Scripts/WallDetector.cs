using UnityEngine;
using System.Collections;

public class WallDetector : MonoBehaviour {
    float speed;
    Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit wallHit;
        Ray wallDetector = new Ray(this.transform.position, speed * direction * Time.deltaTime);

        Debug.DrawRay(this.transform.position, speed * direction * Time.deltaTime * 100);

        if (Physics.Raycast(wallDetector, out wallHit, 1f))
        {
            if (wallHit.collider.gameObject.CompareTag("Untagged"))
            {
                this.tag = "UsedSpell";
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
}
