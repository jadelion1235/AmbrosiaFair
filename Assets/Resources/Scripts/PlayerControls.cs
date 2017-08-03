using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
	public Image healthUI;

	int sensitivity;
	int moveSensitivity;
	public int healthPoints;
	Animator anim;
	float rotY;
	float rotX;
	bool dead;
	
	public GameObject uiReplay;
	// Use this for initialization
	void Start ()
	{
		dead = false;
		sensitivity = 100;
		healthPoints = 100;
		moveSensitivity = 50;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//healthUI.fillAmount = healthPoints / 100f;

		if(healthPoints > 100)
		{
			healthPoints =100;
		}

		if (healthPoints < 0)
		{

			healthPoints = 0;
		}

		if(!dead){
			Screen.lockCursor = true;
		}
		if(dead){
			Screen.lockCursor = false;

		}
		if (!dead)
		{
			if (Input.GetKeyUp (KeyCode.LeftShift))
			{
				sensitivity = 100;
			}

			if (Input.GetKey (KeyCode.LeftShift))
			{
				sensitivity = 25;
			}


			if (Input.GetKeyUp (KeyCode.W))
			{
				anim.SetBool ("Walk", false);
			}
			if (Input.GetKeyUp (KeyCode.S))
			{
				anim.SetBool ("Walk", false);
			}
			if (Input.GetKeyUp (KeyCode.A))
			{
				anim.SetBool ("Walk", false);
			}
			if (Input.GetKeyUp (KeyCode.D))
			{
				anim.SetBool ("Walk", false);
			}

			if (Input.GetKey (KeyCode.W))
			{
				anim.SetBool ("Walk", true);
				transform.Translate (Vector3.forward * moveSensitivity * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.S))
			{
				anim.SetBool ("Walk", true);

				transform.Translate (-Vector3.forward * moveSensitivity * Time.deltaTime);
			
			}
			if (Input.GetKey (KeyCode.D))
			{
				anim.SetBool ("Walk", true);

				transform.Translate (Vector3.right * moveSensitivity / 2 * Time.deltaTime);
			
			}
			if (Input.GetKey (KeyCode.A))
			{
				anim.SetBool ("Walk", true);

				transform.Translate (Vector3.left * moveSensitivity / 2 * Time.deltaTime);
			
			}

			if (Input.GetAxis ("Mouse X") > 0)
			{
				transform.Rotate (0, sensitivity * Time.deltaTime, 0);
			}
			if (Input.GetAxis ("Mouse X") < 0)
			{
				transform.Rotate (0, -sensitivity * Time.deltaTime, 0);
			}

			if (healthPoints <= 0)
			{
				dead = true;
				anim.SetTrigger("Dead");
				Destroy(transform.GetChild(6).gameObject);
				uiReplay.gameObject.SetActive(true);
			}

			//if (Input.GetAxis ("Mouse Y") > 0)
			//{
			//	transform.Rotate(  sensitivity * Time.deltaTime, 0,0);
			//}
			//if (Input.GetAxis ("Mouse Y") < 0)
			//{
			//	transform.Rotate( - sensitivity * Time.deltaTime, 0,0);
			//}
		}

	}
}
