using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIHealth : MonoBehaviour {

	Image healthBar;
	public GameObject player;
	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
		healthBar.fillAmount = player.GetComponent<PlayerControls>().healthPoints/100f;

	}
}
