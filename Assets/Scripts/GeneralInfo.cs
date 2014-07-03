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
		if (col.CompareTag("StopMoveInfo") && GeneralText.activeSelf)
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
		}
		if (col.CompareTag("StopIceText") /*&& BossText.activeSelf*/)
		{
			IceText.SetActive(false);
			
		}
	}
}

