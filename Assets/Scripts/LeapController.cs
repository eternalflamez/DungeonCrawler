using UnityEngine;
using System.Collections;
using Leap;
using System;
using AssemblyCSharp;

public class LeapController : MonoBehaviour {
	Controller controller;
    ObjectController spells;
    float detectDelay = .5f;
    float currentDelay = 0f;
    Vector startPosition = Vector.Zero;
    bool fistDetected;
    
	// Use this for initialization
	void Start () {
		controller = new Controller ();
        this.gameObject.AddComponent("ObjectController");
        spells = (ObjectController) this.gameObject.GetComponent("ObjectController");
        controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        fistDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (startPosition != Vector.Zero)
        {
            currentDelay += Time.deltaTime;

            if (currentDelay > detectDelay)
            {
                startPosition = Vector.Zero;
                currentDelay = 0;
            }
        }

		spells.updateTimer(Time.deltaTime);

		if(Input.GetKey("f"))
		{
			spells.Cast("Fireball");
		}

        if (Input.GetKey("g"))
        {
            spells.Cast("FrostOrb");
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
			FingerList fingers = hand.Fingers.Extended();

            if (!startPosition.Equals(Vector.Zero) && fistDetected)
            {
                if (fingers.Count >= 4)
                {
                    if (hand.PalmPosition.DistanceTo(startPosition) > 20)
                    {
                        // We went from fist to open hand, that moved.
                        spells.Cast("Fireball");

                        startPosition = Vector.Zero;
                        currentDelay = 0;
                        fistDetected = false;
                    }
                }
            }

            if (fingers.IsEmpty)
            {
                startPosition = hand.PalmPosition;
                currentDelay = 0;
                fistDetected = true;
            }
            
            // Get gestures
            GestureList gestures = frame.Gestures();

            for (int i = 0; i < gestures.Count; i++)
            {
                Gesture gesture = gestures[i];

                if (gesture.Type == Gesture.GestureType.TYPESWIPE)
                {
                    SwipeGesture swipe = new SwipeGesture(gesture);
                    Vector3 direction = new Vector3(swipe.Direction.x, 0, 0);

                    if (direction.x > 0)
                    {
                        // Swipe naar rechts
                        Debug.Log("Swipe to the right");
                    }
                    else
                    {
                        // Swipe naar links
                        Debug.Log("Swipe to the left");
                    }
                }
            }
		}
	}
}
