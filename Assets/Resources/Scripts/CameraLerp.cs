using UnityEngine;
using System.Collections;
public class CameraLerp :MonoBehaviour
{
	//public GameObject gTarget;
	//public GameObject tParent;
	public GameObject worldCamera;
	bool gameStarted;
	GameObject target;
	public GameObject player;
	GameObject deadCam;
	// Use this for initialization
	void Start ()
	{
		deadCam = GameObject.Find("DeadLerp");
		gameStarted = false;
		target = worldCamera;
		//gTarget = tParent.transform.GetChild (0).GetChild (0).GetChild(0).gameObject;
		//gTarget = GameObject.Find ("CameraTarget");

	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(player.GetComponent<PlayerControls>().healthPoints <= 0)
		{
			target = deadCam;
		}
			
		transform.position = Vector3.Lerp (transform.position, target.transform.position, Time.deltaTime * 12f);
		transform.rotation = Quaternion.Lerp (transform.rotation, target.transform.rotation, Time.deltaTime * 12f);


		 
	}
}
