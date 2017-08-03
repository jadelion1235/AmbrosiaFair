using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {
	public List<GameObject> spawnLocations;
	// Use this for initialization
	public List<GameObject> enemies;
	float spawnerDelay;
	int enemyLimit;
	GameObject enemyGroup;
	List<GameObject> currentEnemiesCounter;
	int wave;
	bool waveFin;
	int maxPerWave;
	int bossWave;
	bool bossSpawned;
	void Start () {
		spawnLocations = GameObject.FindGameObjectsWithTag("Spawner").ToList();

		bossSpawned = false;
		spawnerDelay = 0.25f;
		//StartCoroutine(Spawn());
		StartCoroutine(WaveF());
		enemyLimit = 50;
		enemyGroup = new GameObject("Enemy Group");
		wave = 1;
		waveFin = false;
		maxPerWave = wave * 5;
		bossWave = 5;


	}


	//IEnumerator Spawn()
	//{
	//	while (true) {
	//		spawnLocations = GameObject.FindGameObjectsWithTag ("Spawner").ToList ();
	//
	//	
	//		currentEnemiesCounter = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
	//		yield return new WaitForSeconds (spawnerDelay);
	//
	//		if (currentEnemiesCounter.Count < enemyLimit) {
	//			GameObject g = Instantiate (enemies [Random.Range (0, enemies.Count)], spawnLocations [Random.Range (0, spawnLocations.Count)].transform.position, Quaternion.identity) as GameObject;
	//			g.transform.parent = enemyGroup.transform;
	//		}
	//
	//		//StartCoroutine (Spawn ());
	//	}
	//}

	void Boss()
	{
		bossSpawned = true;
		GameObject g = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnLocations[Random.Range(0,spawnLocations.Count)].transform.position, Quaternion.identity) as GameObject;
		//g.transform.parent = enemyGroup.transform;
		g.transform.localScale += new Vector3(2,2,2);
		g.GetComponent<EnemyBehaviour>().immortalObject = true;
		g.gameObject.tag = "Boss";
		g.gameObject.name = "SaussageGod";
	}

	IEnumerator WaveF()
	{
		while (true) {
			maxPerWave = wave * 5;
			currentEnemiesCounter = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

			yield return new WaitForSeconds (spawnerDelay);

			if (currentEnemiesCounter.Count < maxPerWave && waveFin == false) {
				GameObject g = Instantiate (enemies [Random.Range (0, enemies.Count)], spawnLocations [Random.Range (0, spawnLocations.Count)].transform.position, Quaternion.identity) as GameObject;
				g.transform.parent = enemyGroup.transform;
			}
				
			if (maxPerWave == currentEnemiesCounter.Count) {
				waveFin = true;
			} 


			if (wave == bossWave && bossSpawned == false) {
				Boss ();
			}

			if(waveFin == true && currentEnemiesCounter.Count == 0)
			{
				wave++;
				waveFin = false;

			}
		}

	}

	// Update is called once per frame
	void Update () {
		
	

	}
}
