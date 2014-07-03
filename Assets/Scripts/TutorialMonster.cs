using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;

public class TutorialMonster : Monster {
    public GameObject fireballText;

    public override void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Spell")
        {
            float f = float.Parse(col.name.Split('/')[0]);
            health -= f;

            String sElement = col.name.Split('/')[1];

            Element e = (Element)Enum.Parse(typeof(Element), sElement);

            GameObject instance = null;

            switch (e)
            {
                case Element.Fire:
                    instance = (GameObject)Instantiate(fireWall, transform.position + transform.up * 1.3f, new Quaternion());
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

            if (health <= 0)
            {
                if (instance != null)
                {
                    Destroy(instance, .5f);
                }

                dead = true;
                agent.stoppingDistance = 0;
                agent.Stop();

                SphereCollider sc = (SphereCollider)this.GetComponent("SphereCollider");
                sc.enabled = false;
                fireballText.guiText.text = "                      Jump in the hole to continue.";
            }

            col.tag = "UsedSpell";
        }
    }
}
