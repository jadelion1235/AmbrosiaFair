using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour {
	int sensitivity;
	// Use this for initialization
	void Start () {
		sensitivity = 15;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse Y") < 0)
		{
			transform.Rotate( 0,0, sensitivity * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse Y") > 0)
		{
			transform.Rotate(0,0, -sensitivity * Time.deltaTime);
		}
	}
}
