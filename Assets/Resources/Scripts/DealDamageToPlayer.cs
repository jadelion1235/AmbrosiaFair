using UnityEngine;
using System.Collections;

public class DealDamageToPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}



	void OnTriggerEnter(Collider other)
	{

	if(transform.parent.gameObject.tag == "Boss" && other.tag =="Player")
	{
	
	other.GetComponent<PlayerControls>().healthPoints -=66;
	
	}

		if (other.tag == "Player" && transform.parent.tag == "Enemy")
		{

			other.GetComponent<PlayerControls>().healthPoints -=20;
			//print(other.GetComponent<PlayerControls>().healthPoints);
			
			
			

		}

	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
