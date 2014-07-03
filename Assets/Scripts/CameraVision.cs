using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CameraVision : MonoBehaviour
{
    private bool falling;
    private int fallingTime;
    private float speed = 5;
    public float MaxBuff = 5f;
    private float buffTimer = 0f;
    private GameObject Lever = null;


    void Start()
    {
        falling = false;
        fallingTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -2, 0);
        }

        if (Input.GetKey("e"))
        {
            transform.Rotate(0, 2, 0);
        }

        Vector3 move = new Vector3(); 

        if (Input.GetKey("w"))
        {
            move += transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey("s"))
        {
            move += transform.forward * -speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            move += transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey("a"))
        {
            move += transform.right * -speed * Time.deltaTime;
        }

        RaycastHit wallHit;
        Ray wallDetector = new Ray(this.transform.position, move);

        if (Physics.Raycast(wallDetector, out wallHit, 1f))
        {
            if (wallHit.collider.gameObject.CompareTag("StopMoveInfo")
                || wallHit.collider.gameObject.CompareTag("StartBossInfo")
                || wallHit.collider.gameObject.CompareTag("StopBossInfo")
                || wallHit.collider.gameObject.CompareTag("StopIceText")
                || wallHit.collider.gameObject.CompareTag("Lever"))
            {
                this.transform.position += move;
            }
            else if (wallHit.collider.gameObject.CompareTag("Potion"))
            {
                switch (wallHit.collider.gameObject.name.ToString())
                {
                    case "Potion1":
                        this.SendMessage("AdjustcurHealth", 20);
                        break;
                    case "Potion2":
                        buffTimer += MaxBuff;
                        if (speed == 5)
                        {
                            speed += 3;
                        }
                        break;
                    default:
                        break;
                }

                Destroy(wallHit.collider.gameObject);
            }
        }
        else
        {
            this.transform.position += move;
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

                    if (hit.transform.CompareTag("LevelEnd"))
                    {
                        Application.LoadLevel("Level1");
                    }
                }
            }
            else
            {
                // We fell off the level.
                Application.LoadLevel("Dead");
            }

        }
        else if (falling)
        {
            falling = false;
            fallingTime = 0;
        }

        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
            if (buffTimer == 0)
            {
                speed = 5;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Lever")
        {
            Lever = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Lever")
        {
            Lever = null;
        }
    }

    void PullLever()
    {
        if (Lever != null)
        {
            Lever.SendMessage("PullLever");
        }
    }
}