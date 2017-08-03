using UnityEngine;
using System.Collections;

public class SpawnerTagChange : MonoBehaviour {



	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnTriggerEnter(Collider other)
	{
	
	if(other.tag == "SpawnPlayer")
	
		{
		this.gameObject.tag = "Untagged";
		}
	
	}
	
	void OnTriggerExit(Collider other)
	{
	this.gameObject.tag = "Spawner";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
