using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IncreasingAlpha : MonoBehaviour {


	private float alpha;
	public GameObject objectSpawner;
	private bool isGoingDestroy;

	public float alphachecker;


	void Awake() {
		alpha = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
    }

	public void callingFadeTo(){
		isGoingDestroy = false;
		alpha = gameObject.GetComponent<SpriteRenderer> ().color.a;
		StartCoroutine (FadeTo(1.0f, 0.5f));
	}

	public void callingFadeOut(){
		isGoingDestroy = true;
		alpha = gameObject.GetComponent<SpriteRenderer> ().color.a;
		StartCoroutine (FadeOut(0.0f, 0.5f));
	}

	IEnumerator FadeTo(float aValue, float aTime) {		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (255, 255, 255, Mathf.Lerp (alpha, aValue, t));
			gameObject.GetComponent<SpriteRenderer> ().color = newColor;
			yield return null;
		}

	}

	IEnumerator FadeOut(float aValue, float aTime) {
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (255, 255, 255, Mathf.Lerp (alpha, aValue, t));
			gameObject.GetComponent<SpriteRenderer> ().color = newColor;
			yield return null;	
		}

	}

	public void cancelEverything()
	{
		StopAllCoroutines ();
	}

	void Update()
	{
		if (gameObject.GetComponent<SpriteRenderer> ().color.a < 0.1f && isGoingDestroy) {
			DestroySelf ();
		}

		alphachecker = gameObject.GetComponent<SpriteRenderer> ().color.a;
	}

	void DestroySelf()
	{
		//objectSpawner.GetComponent<ButtonPressed> ().canSpawn = true;
		Destroy (gameObject);
	}

}
