using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ExplosiveCanister : MonoBehaviour {

public GameObject fireParticle;
public GameObject massDamageExplosion;
public List<GameObject> fireParticlez;
	// Use this for initialization
	void Start () {
	fireParticle.gameObject.SetActive(false);
	}
	
	void OnTriggerEnter(Collider other)
	{
	
	if (other.tag == "bullet")
	{
	
	fireParticle.gameObject.SetActive(true);
	StartCoroutine(Explode());
	}
	
	}
	
	IEnumerator Explode()
	{
	yield return new WaitForSeconds(3f);
	
	
	
	fireParticle.gameObject.SetActive(false);
	massDamageExplosion.GetComponent<MassDamageExplosion>().Explode();
	
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
