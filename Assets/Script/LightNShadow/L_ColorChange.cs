using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_ColorChange : MonoBehaviour {

	private float t;
	[Header("Duration between 2 colors")]
	public float duration;
	public Color firstColor;
	public Color secondColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t = Mathf.PingPong (Time.time, duration)/duration;
		GetComponent<Light> ().color = Color.Lerp(firstColor, secondColor, t);
	}
}
