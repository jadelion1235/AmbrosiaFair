using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int damage;
	// Use this for initialization
	void Start () {
	
		StartCoroutine(DespawnBullet());

	}

	IEnumerator DespawnBullet()
	{
		yield return new WaitForSeconds(5);

		Destroy(this.gameObject);
	}



	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Enemy")
		{
			//print("Collided!");
			other.GetComponent<EnemyBehaviour>().health -= damage;

			Destroy(this.gameObject);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
