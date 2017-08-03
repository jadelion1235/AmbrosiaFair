using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MassDamageExplosion : MonoBehaviour {
public List<GameObject> fireParticlez;

public GameObject explosiveBarrels;
public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
	public void Explode()
	{
	for(int i = 0; i < fireParticlez.Count; i++)
	{
	fireParticlez[i].gameObject.SetActive(false);
	}
	
	explosiveBarrels.gameObject.SetActive(false);
	this.gameObject.GetComponent<Collider>().enabled = true;
	Instantiate(explosion,transform.position, transform.rotation);	
	//this.gameObject.GetComponent<Collider>().enabled = false;
	StartCoroutine(ResetBarrels());
	}
	
	IEnumerator ResetBarrels()
	{
		yield return new WaitForSeconds(1);
		this.gameObject.GetComponent<Collider>().enabled = false;
	
	yield return new WaitForSeconds(60);
	explosiveBarrels.gameObject.SetActive(true);

	
	}
	
	void OnTriggerEnter(Collider other)
	{
	if(other.tag == "Enemy" || other.tag == "Boss")
	
		{
		other.gameObject.GetComponent<EnemyBehaviour>().health = 0;
		}
	
	if(other.tag == "Player")
	{
	
	other.GetComponent<PlayerControls>().healthPoints-= 20;
	
	}
	}
	
	
	void Update () {
	
	}
}
