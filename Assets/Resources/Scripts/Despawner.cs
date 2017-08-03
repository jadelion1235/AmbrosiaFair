using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Despawn());
	}

	IEnumerator Despawn()
	{

		yield return new WaitForSeconds(5);
	
		Destroy(this.gameObject);
		

	}

	// Update is called once per frame
	void Update () {
	
	}
}

