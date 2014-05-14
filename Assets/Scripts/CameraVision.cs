using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CameraVision : MonoBehaviour 
{
	public GameObject invCamera;

	void Start() {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("q")) 
		{
			if(!invCamera.activeSelf)
			{
				transform.Rotate(0, -2, 0);
			}
		}
		
		if (Input.GetKey ("e")) 
		{
			if(!invCamera.activeSelf)
			{
				transform.Rotate(0, 2, 0);
			}
		}

		if(Input.GetKey("w"))
		{
			if(!invCamera.activeSelf)
			{
				transform.position += transform.forward * Time.deltaTime * 5;
			}
		}

		if(Input.GetKey("s"))
		{
			if(!invCamera.activeSelf)
			{
				transform.position += transform.forward * -5 * Time.deltaTime;
			}
		}

		if(Input.GetKey("d"))
		{
			if(!invCamera.activeSelf)
			{
				transform.position += transform.right * Time.deltaTime * 5;
			}
		}
		
		if(Input.GetKey("a"))
		{
			if(!invCamera.activeSelf)
			{
				transform.position += transform.right * -5 * Time.deltaTime;
			}
		}

		if(Input.GetKeyDown("i"))
		{
			if(invCamera.activeSelf)
			{
				invCamera.SetActive(false);
			}
			else
			{
				invCamera.SetActive(true);
			}
		}
	}
}