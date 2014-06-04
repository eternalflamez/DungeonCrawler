using UnityEngine;
using System.Collections;
using Leap;
using System;
using AssemblyCSharp;

public class LeapController : MonoBehaviour {
	Controller controller;
	ObjectController spells = new ObjectController();

	// Use this for initialization
	void Start () {
		controller = new Controller ();

		controller.EnableGesture (Gesture.GestureType.TYPECIRCLE);
		controller.EnableGesture (Gesture.GestureType.TYPEKEYTAP);
		controller.EnableGesture (Gesture.GestureType.TYPESCREENTAP);
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
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
			spells.Cast("Frost Ray");
		}

		spells.moveAllObjects();

		foreach (Spell item in spells.getFinishedObjects()) 
		{
			Destroy(item.getObject());
		}

		Frame frame = controller.Frame();


		Debug.Log ("Frame id: " + frame.Id
		               + ", timestamp: " + frame.Timestamp
		               + ", hands: " + frame.Hands.Count
		               + ", fingers: " + frame.Fingers.Count
		               + ", tools: " + frame.Tools.Count
		               + ", gestures: " + frame.Gestures ().Count);

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
			Vector normal = hand.PalmNormal;
			Vector direction = hand.Direction;
			
			// Calculate the hand's pitch, roll, and yaw angles
			//Debug.Log ("Hand pitch: " + direction.Pitch * 180.0f / (float)Math.PI + " degrees, "
			//               + "roll: " + normal.Roll * 180.0f / (float)Math.PI + " degrees, "
			//               + "yaw: " + direction.Yaw * 180.0f / (float)Math.PI + " degrees");
		}
		
		// Get gestures
		GestureList gestures = frame.Gestures ();
		for (int i = 0; i < gestures.Count; i++) {
			Gesture gesture = gestures [i];
			
			switch (gesture.Type) {
			case Gesture.GestureType.TYPECIRCLE:
				CircleGesture circle = new CircleGesture (gesture);
				
				// Calculate clock direction using the angle between circle normal and pointable
				string clockwiseness;
				if (circle.Pointable.Direction.AngleTo (circle.Normal) <= Math.PI / 4) {
					//Clockwise if angle is less than 90 degrees
					clockwiseness = "clockwise";
					spells.Cast("Fireball");
				} else {
					clockwiseness = "counterclockwise";
				}
				
				float sweptAngle = 0;
				
				// Calculate angle swept since last frame
				if (circle.State != Gesture.GestureState.STATESTART) {
					CircleGesture previousUpdate = new CircleGesture (controller.Frame (1).Gesture (circle.Id));
					sweptAngle = (circle.Progress - previousUpdate.Progress) * 360;
				}
				
				Debug.Log ("Circle id: " + circle.Id
				               + ", " + circle.State
				               + ", progress: " + circle.Progress
				               + ", radius: " + circle.Radius
				               + ", angle: " + sweptAngle
				               + ", " + clockwiseness);
				break;
			case Gesture.GestureType.TYPESWIPE:
				SwipeGesture swipe = new SwipeGesture (gesture);

				Debug.Log ("Swipe id: " + swipe.Id
				               + ", " + swipe.State
				               + ", position: " + swipe.Position
				               + ", direction: " + swipe.Direction
				               + ", speed: " + swipe.Speed);


				Vector3 direction = new Vector3(swipe.Direction.x, 0, 0);


				break;
			case Gesture.GestureType.TYPEKEYTAP:
				KeyTapGesture keytap = new KeyTapGesture (gesture);
				Debug.Log ("Tap id: " + keytap.Id
				               + ", " + keytap.State
				               + ", position: " + keytap.Position
				               + ", direction: " + keytap.Direction);
				break;
			case Gesture.GestureType.TYPESCREENTAP:
				ScreenTapGesture screentap = new ScreenTapGesture (gesture);
				Debug.Log ("Tap id: " + screentap.Id
				               + ", " + screentap.State
				               + ", position: " + screentap.Position
				               + ", direction: " + screentap.Direction);
				spells.Cast("Frost Ray");

				break;
			default:
				Debug.Log ("Unknown gesture type.");
				break;
			}
		}
		
		if (!frame.Hands.IsEmpty || !frame.Gestures ().IsEmpty) {
			//Debug.Log ("");
		}
	}
}
