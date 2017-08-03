using UnityEngine;
using System.Collections;

public class MinigunAnim : MonoBehaviour {

	public GameObject frame1;
	public GameObject frame2;
	public GameObject frame3;
	public GameObject frame4;
	public GameObject frame5;
	[Range(0.025f,0.5f)]
	public float delay;
	// Use this for initialization
	void Start () {
	
		frame2.GetComponent<MeshRenderer>().enabled = false;
		frame3.GetComponent<MeshRenderer>().enabled = false;
		frame4.GetComponent<MeshRenderer>().enabled = false;
		frame5.GetComponent<MeshRenderer>().enabled = false;

		StartCoroutine (VoxAnim());

	}

	IEnumerator VoxAnim(){

		yield return new WaitForSeconds(delay);

		frame1.GetComponent<MeshRenderer>().enabled = false;
		frame2.GetComponent<MeshRenderer>().enabled = true;

		yield return new WaitForSeconds(delay);
		
		frame2.GetComponent<MeshRenderer>().enabled = false;
		frame3.GetComponent<MeshRenderer>().enabled = true;

		yield return new WaitForSeconds(delay);
		
		frame3.GetComponent<MeshRenderer>().enabled = false;
		frame4.GetComponent<MeshRenderer>().enabled = true;

		yield return new WaitForSeconds(delay);
		
		frame4.GetComponent<MeshRenderer>().enabled = false;
		frame5.GetComponent<MeshRenderer>().enabled = true;

		yield return new WaitForSeconds(delay);
		
		frame5.GetComponent<MeshRenderer>().enabled = false;
		frame1.GetComponent<MeshRenderer>().enabled = true;

		StartCoroutine (VoxAnim());

	}

	// Update is called once per frame
	void Update () {
	
	}
}
