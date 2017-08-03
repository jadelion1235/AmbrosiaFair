using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour {
	GameObject target;
	UnityEngine.AI.NavMeshAgent agent;
	public int health;
	bool updateDisabler;
	bool dead;
	Animator anim;
	GameObject attackBox;
	public List<GameObject> drops;
	public bool immortalObject;
	bool nearPlayer;
	int speed;

	bool amIOffTheNavMesh;
	// Use this for initialization
	void Start () {
		speed = Random.Range(20,30);
		immortalObject = false;



		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.speed = speed;
		target = GameObject.Find ("LilithFBX");
		health = 100;
		updateDisabler = false;
		StartCoroutine(UpdatePosition());
		dead = false;
		anim = GetComponent<Animator>();
		attackBox = transform.GetChild(2).gameObject;
		attackBox.GetComponent<Collider>().enabled = false;
	}

	IEnumerator UpdatePosition()
	{
		while (true) {
			if (immortalObject) {
				this.gameObject.tag = "Boss";
			}

			yield return new WaitForSeconds (1 / speed);
			//if (agent.hasPath) {
			NavMeshPath navpath = new NavMeshPath ();
			NavMesh.CalculatePath (this.transform.position, target.transform.position, -1, navpath);
			if (navpath.status == UnityEngine.AI.NavMeshPathStatus.PathPartial || navpath.status == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
				// Not reachable

				//print ("NotReachable");
				amIOffTheNavMesh = true;

				Destroy (this.gameObject);
			}
			if (amIOffTheNavMesh == false) {
				agent.SetDestination (target.transform.position);
			}		
		}
	}

	IEnumerator IamDead()
	{
		anim.SetTrigger("dead");
		agent.Stop();
		dead = true;
		yield return new WaitForSeconds(2);

		int dropchance = Random.Range (0, 5);
		if (dropchance > 3) {
			Instantiate (drops [Random.Range (0, 5)],new Vector3(transform.position.x, transform.position.y +1,transform.position.z), transform.rotation);
		}

		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{

		nearPlayer = true;

		if (other.tag == "Player" && dead == false)
		{
			StartCoroutine(Attacker());
			//anim.SetTrigger("Attack");
		}

	}

	IEnumerator Attacker()
	{
		while (true) {
			if (nearPlayer) {
			anim.SetTrigger ("Attack");
			yield return new WaitForSeconds (Random.Range (2, 4));

			transform.GetChild (2).gameObject.GetComponent<Collider> ().enabled = false;



			}
		}
	}

	void OnTriggerExit(Collider other)
	{


		nearPlayer = false;


	}

	void DamagePlayer()
	{

		//print(target.GetComponent<PlayerControls>().healthPoints);

		transform.GetChild(2).gameObject.GetComponent<Collider>().enabled = true;

		//	transform.root.GetChild(2).gameObject.GetComponent<Collider>().enabled = false;

		//attackBox.GetComponent<Collider>().enabled = true;
		//attackBox.GetComponent<Collider>().enabled = false;

	}

	// Update is called once per frame
	void Update () {

		if( health <= 0 && updateDisabler == false)
		{
			
			StopAllCoroutines();
			updateDisabler = true;
			StartCoroutine(IamDead());

		}

	}
}
