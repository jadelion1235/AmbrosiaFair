using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour {

	public Transform playerCam, character, centerPoint;

	float mouseX, mouseY;
	public float mouseSpeed = 10f;
	public float mouseYPosition = 6f;

	private float moveFB, moveLR;
	public float moveSpeed = 2f;

	float zoom;
	public float zoomSpeed = 2f;

	public float zoomMin = -2f;
	public float zoomMax = -10f;

	public float rotationSpeed = 5f;

	public float gravity = 100f;
	float fallSpeed = 2f;
	Vector3 movement;
	// Use this for initialization
	void Start () {
		zoom = -3f;
	}
	
	// Update is called once per frame
	void Update () {

		zoom += Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;

		if (zoom > zoomMin) 
		{
			zoom = zoomMin;
		}
		if (zoom < zoomMax) {
			zoom = zoomMax;
		}

		playerCam.transform.localPosition = new Vector3 (0f, 0f, zoom);

		//if (Input.GetMouseButton (1)) {

			mouseX += Input.GetAxis ("Mouse X");

			mouseY += Input.GetAxis ("Mouse Y");

		//}

		mouseY = Mathf.Clamp (mouseY, -20f, 100f);
		playerCam.LookAt (centerPoint);
		centerPoint.localRotation = Quaternion.Euler (mouseY, mouseX, 0);

		CharacterController cc = GetComponent<CharacterController> ();
		if (cc.isGrounded) {
			moveFB = Input.GetAxis ("Vertical") * moveSpeed;
			moveLR = Input.GetAxis ("Horizontal") * moveSpeed;
		}
		//Gravity
		
		movement = new Vector3 (moveLR, movement.y -= gravity * Time.deltaTime, moveFB);
			movement = transform.TransformDirection (movement);
			//movement *= fallSpeed;
			
			//movement = character.rotation * movement;
		
	
		character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
		centerPoint.position = new Vector3 (character.position.x, character.position.y+mouseYPosition, character.position.z);
	

		if (Input.GetAxis ("Vertical") > 0 | Input.GetAxis("Vertical") < 0) {

			Quaternion turnAngle = Quaternion.Euler (0, centerPoint.eulerAngles.y, 0);

			character.rotation = Quaternion.Slerp (character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
		}
	}
}
