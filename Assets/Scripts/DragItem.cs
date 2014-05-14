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

		float x = Mathf.Round(curPosition.x);
		float y = Mathf.Round(curPosition.y);
		float z = curPosition.z;

		transform.position = new Vector3(x, y, z);
	}
	
}