using UnityEngine;
using System.Collections;
using Leap;
using System;
using AssemblyCSharp;

public class LeapController : MonoBehaviour {
	Controller controller;
    ObjectController spells;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
        this.gameObject.AddComponent("ObjectController");
        spells = (ObjectController) this.gameObject.GetComponent("ObjectController");
	}
	
	// Update is called once per frame
	void Update () {
		spells.updateTimer(Time.deltaTime);

		if(Input.GetKey("f"))
		{
			spells.Cast("Fireball");
		}

		if(Input.GetKey ("g"))
		{
			//spells.Cast("Frost Ray");
		}

		spells.moveAllObjects();

		foreach (Spell item in spells.getFinishedObjects()) 
		{
			Destroy(item.getObject());
		}

		Frame frame = controller.Frame();

		if (!frame.Hands.IsEmpty) {
			// Get the first hand
			Hand hand = frame.Hands [0];
			
			// Check if the hand has any fingers
			FingerList fingers = hand.Fingers;
			if (!fingers.IsEmpty) {
				// Calculate the hand's average finger tip position
				Vector avgPos = Vector.Zero;
				foreach (Finger finger in fingers) {
					avgPos += finger.TipPosition;
				}
				avgPos /= fingers.Count;
			//	Debug.Log ("Hand has " + fingers.Count
			//	               + " fingers, average finger tip position: " + avgPos);
			}
			
			// Get the hand's sphere radius and palm position
			//Debug.Log ("Hand sphere radius: " + hand.SphereRadius.ToString ("n2")
			//               + " mm, palm position: " + hand.PalmPosition);
			
			// Get the hand's normal vector and direction
			//Vector normal = hand.PalmNormal;
			//Vector direction = hand.Direction;
			
			// Calculate the hand's pitch, roll, and yaw angles
			//Debug.Log ("Hand pitch: " + direction.Pitch * 180.0f / (float)Math.PI + " degrees, "
			//               + "roll: " + normal.Roll * 180.0f / (float)Math.PI + " degrees, "
			//               + "yaw: " + direction.Yaw * 180.0f / (float)Math.PI + " degrees");
		}
		
		// Get gestures
		GestureList gestures = frame.Gestures ();

		for (int i = 0; i < gestures.Count; i++) {
			Gesture gesture = gestures [i];

            if (gesture.Hands.Count > 0)
            {

            }
		}
		
		if (!frame.Hands.IsEmpty || !frame.Gestures ().IsEmpty) {
			//Debug.Log ("");
		}
	}
}
