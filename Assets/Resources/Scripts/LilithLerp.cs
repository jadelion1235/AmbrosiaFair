using UnityEngine;
using System.Collections;

public class LilithLerp : MonoBehaviour {
public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.rotation = Quaternion.Lerp (transform.rotation, target.transform.rotation, Time.deltaTime/2f);
	}
}
