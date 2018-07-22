using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Control : MonoBehaviour {

	private GameObject p; 

	public Vector2 launchVelocity;
	public float timeForLaunching;

	public float SpeedX;
	public float SpeedY;

	public bool launched = true;
	private Rigidbody2D s_rigidbody;
    
    private GameObject[] player = new GameObject[2];
    private GameObject[] e;

    private SpriteRenderer sr;
    private float t = 1;

    void Awake(){
		s_rigidbody = GetComponent<Rigidbody2D> ();
        p = GetComponent<FS_searchNearest>().FindClosestEnemy();
	}

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        e = GameObject.FindGameObjectsWithTag("Enemy");
        player[0] = GameObject.FindGameObjectWithTag("Player");
        player[1] = GameObject.FindGameObjectWithTag("Player2");

        // ignore collision with enemy
        for (int i = 0; i < e.Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), e[i].GetComponent<BoxCollider2D>());
        }
    }

    // Update is called once per frame
    void Update () {
		SpeedX = p.GetComponent<P_throw>().speedX; 
		SpeedY = p.GetComponent<P_throw>().speedY;
		launchVelocity = new Vector2 (SpeedX * p.transform.localScale.x , SpeedY);

		timeForLaunching -= Time.deltaTime;

		if (!launched && timeForLaunching <= 0) {
			Launch ();
        }

        if(Mathf.Abs(s_rigidbody.velocity.x) <= 1f)
        {
            t -= Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(0,1,t));

            if (sr.color.a <= 0.2f)
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StoneDroppingSound();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void Launch(){
		s_rigidbody.velocity = launchVelocity;

		launched = true;
	}

    public void StoneDroppingSound()
    {
        FindObjectOfType<AudioManager>().Play("StoneDropping");
    }

}
