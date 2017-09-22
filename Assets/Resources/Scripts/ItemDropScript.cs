using UnityEngine;
using System.Collections;

public class ItemDropScript : MonoBehaviour {

	bool bFlip;
	GameObject gunHolster;
	GameObject player;
	public enum type{health,Pistol,Shotgun,Minigun,ammo}
	GameObject pistol;
	GameObject shotGun;
	GameObject miniGun;

	public type dropType;

	//----------------------------------------------------------
	void Start () {


		player = GameObject.Find ("NewLilith");
		pistol = Resources.Load ("Prefabs/Gun") as GameObject;
		shotGun = Resources.Load ("Prefabs/Shotgun") as GameObject;
		miniGun = Resources.Load ("Prefabs/MiniGun") as GameObject;
		bFlip = true;
		//StartCoroutine (Despawn ());
	}

	//----------------------------------------------------------

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds (60);
		Destroy (this.gameObject);

	}


	//----------------------------------------------------------

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && dropType == type.health) 
		{
			other.GetComponent<PlayerControls> ().healthPoints += 5;
			Destroy (this.gameObject);
		}

		if (other.tag == "Player" && dropType == type.Pistol) 
		{
			GameObject currentGun = GameObject.FindGameObjectWithTag("Gun");
			GameObject g = Instantiate(pistol, currentGun.transform.position,currentGun.transform.rotation) as GameObject;
			g.transform.parent = player.transform;
			Destroy(currentGun.gameObject);
		}
		if (other.tag == "Player" && dropType == type.Shotgun) 
		{
			GameObject currentGun = GameObject.FindGameObjectWithTag("Gun");
			GameObject g = Instantiate(shotGun, currentGun.transform.position, currentGun.transform.rotation) as GameObject;
			g.transform.parent = player.transform;

			Destroy(currentGun.gameObject);

		}
		if (other.tag == "Player" && dropType == type.Minigun) 
		{
			GameObject currentGun = GameObject.FindGameObjectWithTag("Gun");
			GameObject g = Instantiate(miniGun, currentGun.transform.position, currentGun.transform.rotation) as GameObject;
			g.transform.parent = player.transform;

			Destroy(currentGun.gameObject);

		}
		if (other.tag == "Player" && dropType == type.ammo) 
		{

			GameObject gun = GameObject.FindGameObjectWithTag("Gun") as GameObject;

			if (gun.transform.GetChild(0).GetComponent<Gun>().ammoType == Gun.AmmoEnum.Pistol)
			{
				gun.transform.GetChild(0).GetComponent<Gun>().pistolAmmo += 20;
			}
			if (gun.transform.GetChild(0).GetComponent<Gun>().ammoType == Gun.AmmoEnum.Shotgun)
			{
				gun.transform.GetChild(0).GetComponent<Gun>().shotgunAmmo += 40;
			}
			if (gun.transform.GetChild(0).GetComponent<Gun>().ammoType == Gun.AmmoEnum.Minigun)
			{
				//print (gun.transform.GetChild(0).name);
				gun.transform.GetChild(0).GetComponent<Gun>().minigunAmmo += 100;
				//print ("Reserve:"+ gun.transform.GetChild(0).GetComponent<Gun>().currentReserve);
				if(gun.transform.GetChild(0).GetComponent<Gun>().minigunAmmo > gun.transform.GetChild(0).GetComponent<Gun>().clipSize)
				{
				//	gun.transform.GetChild(0).GetComponent<Gun>().currentReserve = gun.transform.GetChild(0).GetComponent<Gun>().clipSize;
				}

			}

		}

		if (other.tag == "Player") 
		{
			Destroy(this.gameObject);
		}

	}

	//----------------------------------------------------------

	// Update is called once per frame
	void Update () {

		transform.Rotate (0,50*Time.deltaTime,0); 
		transform.position = new Vector3(transform.parent.position.x, Mathf.PingPong(Time.time/2,3)+transform.parent.position.y, transform.parent.position.z);
	}
}
