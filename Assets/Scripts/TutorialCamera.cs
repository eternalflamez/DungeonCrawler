using UnityEngine;
using System.Collections;

public class TutorialCamera : MonoBehaviour {
    public GameObject moveInfoText;
    public GameObject spellInfoText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.CompareTag("StopMoveInfo") && moveInfoText.activeSelf)
        {
            moveInfoText.SetActive(false);
            spellInfoText.SetActive(true);
        }
    }
}
