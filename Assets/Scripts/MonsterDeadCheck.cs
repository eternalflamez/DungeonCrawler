using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterDeadCheck : MonoBehaviour {
    public List<GameObject> monsters;
    float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            bool dead = true;

            foreach (GameObject monster in monsters)
            {
                if (monster.animation.name != "dead")
                {
                    dead = false; // This monster ain't dead yet, yo!
                    break;
                }
            }

            if (dead)
            {
                Application.LoadLevel("VictoryScreen");
            }

            timer = 0;
        }
	}
}
