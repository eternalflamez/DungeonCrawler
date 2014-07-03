using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{
    private bool leverStatus = false;
    public GameObject Door;
    private bool pulled = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leverStatus == true)
        {
            leverStatus = false;
            transform.Rotate(40, 0, 0);
            Door.SendMessage("OpenDoor");
        }
    }

    void PullLever()
    {
        if (!pulled)
        {
            leverStatus = true;
            pulled = true;
        }
    }
}