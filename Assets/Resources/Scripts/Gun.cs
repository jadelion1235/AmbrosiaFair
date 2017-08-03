using UnityEngine;
using System.Collections;

//for UI only
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
	//Inspector properites

	//Bullets will fire from this gameobjects location
	public GameObject shootLocation;
	public GameObject gunFlare;
	//Bullet prefab
	[Header("Basic Gun Properties")]
	public GameObject bullet;
	[Range(0,25)]
	public int bulletsPerSecond;
	[Tooltip("How much can be loaded into the gun at once?")]
	public int clipSize;
	[Header("")]
	//[SerializeField]

	bool readyToFire;


	[Tooltip("For Shotgun like weapons")]
	public bool multipleShellsPerShot;
	public int numberOfShellsPerShot;
	[Tooltip("FIRE RATE IS READ ONLY")]
	float fireRate;
	[Tooltip("Shotgun Shell Spread")]
	[Range(0,10)]
	public float dispersion;

	[Header("Ammunition Display")]

	public int currentLoadedAmmo;
	public int reserveAmmo;

	[Tooltip("E.g. Machine gun, hold down mouse button instead of clicking repeatedly")]
	public bool Automatic;
	//Ammunition type
	public enum AmmoEnum{Pistol,Shotgun,Minigun}
	//Drop down list for ammunition type, which reserves it draws from.
	[Tooltip("which type of collectible/drop will have an effect on the ammunition storage for this weapon? This setting does not automatically turn the gun into a Minigun or Shotgun.")]
	public AmmoEnum ammoType;
	[Tooltip("A number indicating how much damage the bullet will do. Bullet must have a variable for 'damage' ")]
	public int bulletDamageValue;
	public int currentReserve;

	//NON inspector variables

	//random values for shotgun shell offset
	float randX;
	float randY;
	float randZ;
	bool canReload;
	public int pistolAmmo;
	public int shotgunAmmo;
	public int minigunAmmo;
	int difference;


	//UI Specific, not neccessary to run
	GameObject uiAmmo;
	GameObject uiReserve;

	// Use this for initialization
	void Start ()
	{

		//UI Specific, not neccessary to run
		uiAmmo = GameObject.Find("AmmunitionCount");
		uiReserve = GameObject.Find("ReserveAmmoCount");

		readyToFire = true;
		fireRate = 1f/bulletsPerSecond;

		minigunAmmo = 1;
		currentLoadedAmmo = clipSize;

		canReload = false;

	}

	//Reload weapon
	void Reload()
	{
		if(canReload)
		{
			//if this gun draws from pistol ammo
			if(reserveAmmo == pistolAmmo)
			{
				pistolAmmo = pistolAmmo - clipSize;
				//Check if pistol ammo will be depleted
				if(pistolAmmo < clipSize)
				{
					currentLoadedAmmo += pistolAmmo;
					pistolAmmo = 0;
				}
				currentLoadedAmmo = currentLoadedAmmo + clipSize;
				//check that the gun clip has not been overloaded
				if (currentLoadedAmmo > clipSize)
				{
					difference = currentLoadedAmmo - clipSize;
					currentLoadedAmmo = currentLoadedAmmo - difference;
					pistolAmmo = pistolAmmo + difference;
				}
			}if(reserveAmmo == shotgunAmmo)
			{
				shotgunAmmo = shotgunAmmo - clipSize;
				//Check if pistol ammo will be depleted
				if(shotgunAmmo < clipSize){
					currentLoadedAmmo += shotgunAmmo;
					shotgunAmmo = 0;
				}
				currentLoadedAmmo = currentLoadedAmmo + clipSize;
				//check that the gun clip has not been overloaded
				if (currentLoadedAmmo > clipSize)
				{
					difference = currentLoadedAmmo - clipSize;
					currentLoadedAmmo = currentLoadedAmmo - difference;
					shotgunAmmo = shotgunAmmo + difference;
				}

			}if(reserveAmmo == minigunAmmo)
			{
				minigunAmmo = minigunAmmo - clipSize;
				//Check if pistol ammo will be depleted
				if(minigunAmmo < clipSize){
					currentLoadedAmmo += minigunAmmo;
					minigunAmmo = 0;
				}
				currentLoadedAmmo = currentLoadedAmmo + clipSize;
				//check that the gun clip has not been overloaded
				if (currentLoadedAmmo > clipSize)
				{
					difference = currentLoadedAmmo - clipSize;
					currentLoadedAmmo = currentLoadedAmmo - difference;
					minigunAmmo = minigunAmmo + difference;
				}
			}
		}
	}

	//Rechamber the gun a time delay. Gun will not fire (even though Fire() is called quite often) until rechambered.
	IEnumerator Rechamber ()
	{
		yield return new WaitForSeconds (fireRate);

		//Slow down the shotgun:
		if(multipleShellsPerShot){
			yield return new WaitForSeconds(fireRate);
		}

		readyToFire = true;
		if(Automatic){
			if(Input.GetMouseButton(0)){
				Fire ();
			}
		}
	}



	//Fire Gun
	void Fire ()
	{

		//Random dispersion between a set range. No gun is 100% accurate. Abuse this setting for Shotguns to get birdshot.
		randX = Random.Range(-dispersion , dispersion);
		randY = Random.Range(-dispersion , dispersion);
		randZ = Random.Range(-dispersion , dispersion);

		if(readyToFire && currentLoadedAmmo > 0){
			//Fire burst shells if needed (Shotgun)
			for(int i = numberOfShellsPerShot; i > 0; i--){ 

				GameObject g = Instantiate (bullet, shootLocation.transform.position, shootLocation.transform.rotation) as GameObject;
				g.GetComponent<Bullet>().damage = bulletDamageValue;

				if(gunFlare != null)
				{
					GameObject flare = Instantiate (gunFlare , shootLocation.transform.position, shootLocation.transform.rotation) as GameObject;
					flare.transform.parent = shootLocation.transform;
				}

				g.transform.Rotate(randX, randY, randZ);
				g.GetComponent<Rigidbody>().AddForce(g.transform.forward * 200000);
				randX = Random.Range(-dispersion , dispersion);
				randY = Random.Range(-dispersion , dispersion);
				randZ = Random.Range(-dispersion , dispersion);
			}
			currentLoadedAmmo --;
			readyToFire = false;
			StartCoroutine(Rechamber());
		}
	}

	// Update is called once per frame
	void Update ()
	{
		//print ("Reload Status:  "+canReload);
		//update the inspector with current ammo type. Debugging only.
		if(ammoType == AmmoEnum.Pistol){
			currentReserve = pistolAmmo;
			reserveAmmo = pistolAmmo;
		}else if(ammoType ==  AmmoEnum.Shotgun){
			currentReserve = shotgunAmmo;
			reserveAmmo = shotgunAmmo;
		}else if(ammoType == AmmoEnum.Minigun){
			currentReserve = minigunAmmo;
			reserveAmmo = minigunAmmo;
		}


		if (currentLoadedAmmo != clipSize && readyToFire == true){
			canReload = true;
		}
		//If multiple shells is not selected, shells fired = 1.
		if (!multipleShellsPerShot){
			numberOfShellsPerShot = 1;
		}
		fireRate = 1f/bulletsPerSecond;


		if(Input.GetMouseButtonDown(0)){
			Fire ();
		}
		if (Input.GetKey(KeyCode.R)){
			Reload();
		}

		//////////////////////////////
		/// UI SPECIFIC BEYOND THIS///
		//////////////////////////////

		uiAmmo.GetComponent<Text>().text = currentLoadedAmmo.ToString();
		uiReserve.GetComponent<Text>().text = currentReserve.ToString();



	}
}
