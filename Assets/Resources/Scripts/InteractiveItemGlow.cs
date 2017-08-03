using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItemGlow : MonoBehaviour {

	Color origColor;

	List<Color> secondColor;

	Renderer curRenderer;

	public Color glowColor;

	// Use this for initialization
	void Start () {

		origColor = GetComponent<Renderer> ().material.color;


	}





	// Update is called once per frame
	void Update () {
		Renderer thisRenderer = GetComponent<Renderer>();

		thisRenderer.material.color = Color.Lerp(origColor,glowColor * 10f, Mathf.PingPong(Time.time,1));

	}
}
