using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_ColorChange : MonoBehaviour {

	private float t;
    private float t2;
    private float t3;
    [Header("Duration between 2 colors")]
    public bool colorLerp;
	public float duration;
	public Color firstColor;
	public Color secondColor;

    public bool intensityLerp;
    public float duration2;
    public float firstIntensity;
    public float secondIntensity;

    public bool rangeLerp;
    public float duration3;
    public float firstRange;
    public float secondRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(colorLerp)
        {
            t = Mathf.PingPong(Time.time, duration) / duration;
            GetComponent<Light>().color = Color.Lerp(firstColor, secondColor, t);
        }

        if(intensityLerp)
        {
            t2 = Mathf.PingPong(Time.time, duration2) / duration2;
            GetComponent<Light>().intensity = Mathf.Lerp(firstIntensity, secondIntensity, t2);
        }

        if(rangeLerp)
        {

            t3 = Mathf.PingPong(Time.time, duration3) / duration3;
            GetComponent<Light>().range = Mathf.Lerp(firstRange, secondRange, t3);
        }
	}
}
