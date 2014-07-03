using UnityEngine;
using System.Collections;
using Leap;
using System;
using AssemblyCSharp;

public class LeapController : MonoBehaviour {
	Controller controller;
    ObjectController spells;
    float detectDelay = 100f;
    float currentDelay = 0f;
    Vector startPosition = Vector.Zero;
    Frame startFrame = Frame.Invalid;
    float handRotation;
    bool fistDetected;
    bool palmDetected;
    
	// Use this for initialization
	void Start () {
		controller = new Controller ();
        this.gameObject.AddComponent("ObjectController");
        spells = (ObjectController) this.gameObject.GetComponent("ObjectController");
        controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        fistDetected = false;
        palmDetected = false;
        handRotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!startPosition.Equals(Vector.Zero) || !startFrame.Equals(Frame.Invalid))
        {
            currentDelay += Time.deltaTime;

            if (currentDelay > detectDelay)
            {
                startPosition = Vector.Zero;
                startFrame = Frame.Invalid;
                currentDelay = 0;
                handRotation = 0;
                palmDetected = false;
                fistDetected = false;
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
                if (fingers.Count >= 4 && hand.PalmPosition.DistanceTo(startPosition) > 20)
                {
                    // We went from fist to open hand, that moved.
                    spells.Cast("Fireball");

                    startPosition = Vector.Zero;
                    currentDelay = 0;
                    fistDetected = false;
                }
            }

            if(!startFrame.Equals(Frame.Invalid) && palmDetected)
            {
                handRotation += frame.RotationAngle(startFrame, Vector.ZAxis);
                Debug.Log(handRotation);
                if (Mathf.Abs(handRotation) > 80)
                {
                    spells.Cast("FrostOrb");

                    startFrame = Frame.Invalid;
                    currentDelay = 0;
                    palmDetected = false;
                    handRotation = 0;
                }
            }

            if (fingers.IsEmpty)
            {
                startPosition = hand.PalmPosition;
                currentDelay = 0;
                fistDetected = true;
                palmDetected = false;
            }
            else if (fingers.Count >= 4 && !palmDetected)
            {
                Debug.Log("Palm detected!");
                fistDetected = false;
                palmDetected = true;
                currentDelay = 0;
                startFrame = frame;
                handRotation = 0;
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

                    SendMessage("PullLever");

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
