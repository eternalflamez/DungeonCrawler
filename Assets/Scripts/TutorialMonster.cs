using UnityEngine;
using System.Collections;

public class TutorialMonster : Monster {
    public GameObject fireballText;


    public override void OnTriggerEnter(Collider col)
    {
        if (col.tag == "MainCamera")
        {
            // Destroy(col.gameObject.CubePlac);
        }
        else if (col.tag == "Spell")
        {
            health -= float.Parse(col.name);

            if (health <= 0)
            {
                dead = true;
                agent.SetDestination(transform.position);
                agent.speed = 0;

                SphereCollider sc = (SphereCollider)this.GetComponent("SphereCollider");
                sc.enabled = false;
				fireballText.guiText.text = "Jump in the hole to continue.";
            }

            col.tag = "UsedSpell";
        }
    }
}
