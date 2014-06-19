using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CameraVision : MonoBehaviour 
{
    private bool falling;
    private int fallingTime;

	void Start() {
        falling = false;
        fallingTime = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("q")) 
		{
			transform.Rotate(0, -2, 0);
		}
			
		if (Input.GetKey ("e")) 
		{
			transform.Rotate(0, 2, 0);
		}

		if(Input.GetKey("w"))
		{
			transform.position += transform.forward * Time.deltaTime * 5;
		}

		if(Input.GetKey("s"))
		{
			transform.position += transform.forward * -5 * Time.deltaTime;
		}

		if(Input.GetKey("d"))
		{
			transform.position += transform.right * Time.deltaTime * 5;
		}
			
		if(Input.GetKey("a"))
		{
			transform.position += transform.right * -5 * Time.deltaTime;
		}

        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, -this.transform.up);

        if (!Physics.Raycast(ray, 1.84f))
        {
            fallingTime++;

            if (!falling)
            {
                falling = true;
            }

            if (Physics.Raycast(ray, out hit, 40))
            {
                if (hit.distance > 1.83 + (this.transform.up * Time.deltaTime * fallingTime).y)
                {
                    this.transform.position -= this.transform.up * Time.deltaTime * fallingTime * .5f;
                }
                else
                {
                    this.transform.position -= new Vector3(0, hit.distance - 1.83f, 0);
                }
            }
            else
            {
                // We fell off the level.
            }
            
        }
        else if (falling)
        {
            falling = false;
            fallingTime = 0;
        }
	}
}