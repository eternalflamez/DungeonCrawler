using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class DragItem : MonoBehaviour 
{
	public Vector3 screenPoint;
	public Vector3 offset;

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		// snap x to original pos + ((1 + 10/1920) * blocks moved)
		float x = RoundOdd(curPosition.x, .123f);

		// snap y to original pos + ((1 + 10/1080) * blocks moved)
		float y = RoundOdd(curPosition.y, .124f);
		float z = curPosition.z;

		transform.position = new Vector3(x, y, z);
	}

	float RoundOdd(float input, float roundper)
	{
		float roundPer = 1 + roundper;
		float position = (float)System.Math.Floor(input / roundPer);
		
		if (input >= position * roundPer + roundPer / 2)
		{
			input = (roundPer * (position + 1));
		}
		else
		{
			input = (position * roundPer);
		}

		return input;
	}
}