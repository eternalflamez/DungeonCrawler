using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public int maxHeight = 0;
    public string direction;
    bool isMoving = false;
    float currentHeight = 0;

    // Use this for initialization
    void Start()
    {
        if (maxHeight == 0)
        {
            maxHeight = (int)renderer.bounds.extents.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            float move = Time.deltaTime;

            transform.position += new Vector3(0, move, 0);
            currentHeight += move;

            if (currentHeight > maxHeight)
            {
                isMoving = false;
                currentHeight = 0;
            }
        }
    }

    void OpenDoor()
    {
        isMoving = true;
    }
}
