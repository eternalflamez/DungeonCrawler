using UnityEngine;
using System.Collections;

public class GeneralInfo : MonoBehaviour {
	public GameObject GeneralText;
	public GameObject BossText;
	public GameObject IceText;
	// Use this for initialization
	void Start () {
	    BossText.SetActive(false);
	    IceText.SetActive (false);
	}
		

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("StopMoveInfo") /*&& GeneralText.activeSelf*/)
		{
			GeneralText.SetActive(false);
			IceText.SetActive(true);
			//BossText.SetActive(false);

		}
		if (col.CompareTag("StartBossInfo") /*&& BossText.activeSelf*/)
		{
			BossText.SetActive(true);
		}
		if (col.CompareTag("StopBossInfo") /*&& BossText.activeSelf*/)
		{
			BossText.SetActive(false);
<<<<<<< HEAD
		}
		if (col.CompareTag("StopIceText") /*&& BossText.activeSelf*/)
		{
=======
>>>>>>> 3fd13380a0d2b2d8c642d3a86d60b08dc8db0a89
			IceText.SetActive(false);
		}

	}
}

