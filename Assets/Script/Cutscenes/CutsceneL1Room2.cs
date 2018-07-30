using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneL1Room2 : MonoBehaviour {

    public static CutsceneL1Room2 onlyOnce;

    [SerializeField]GameObject EnemyObject;
    [SerializeField] GameObject NPCObject;
    [SerializeField] GameObject spawnLocation;
    [SerializeField] float NPCMoveSpeed;
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] GameObject targetPosition;
    [SerializeField] GameObject exitPosition;

    private GameObject NPC;
    private GameObject Enemy;
    private Animator NPCAnimator;
    private Animator EMAnimator;
    private GameObject[] players;
    private bool triggerOnce;
    private bool exitOnce;
    private float counter = 0;
    private int Stages = 0;

    // Use this for initialization
    private void Awake()
    {
        if (onlyOnce == null)
        {
            onlyOnce = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start ()
    {

        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Player");
        players[1] = GameObject.FindGameObjectWithTag("Player2");

        NPC = (GameObject)Instantiate(NPCObject, spawnLocation.transform.position + new Vector3(0f, -2.45f, 0), Quaternion.identity);
        Enemy = (GameObject)Instantiate(EnemyObject, spawnLocation.transform.position + new Vector3(5f, 0, 0), Quaternion.identity);

        NPCAnimator = NPC.GetComponent<Animator>();
        EMAnimator = Enemy.GetComponent<Animator>();

        DontDestroyOnLoad(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
        
        if(!exitOnce)
        {
            switch (Stages)
            {
                case 1:
                    NPC.transform.position = Vector2.MoveTowards(NPC.transform.position, new Vector2(targetPosition.transform.position.x, NPC.transform.position.y), NPCMoveSpeed * Time.deltaTime);

                    if (Vector2.Distance(NPC.transform.position, targetPosition.transform.position) <= 5f)
                    {
                        Stages = 2;
                    }

                    break;
                case 2:
                    NPCAnimator.Play("NG_Scare");
                    if (counter >= 2)
                    {
                        Stages = 3;
                        counter = 0;
                    }
                    else
                    {
                        counter += 1f * Time.deltaTime;
                    }

                    break;
                case 3:
                    NPC.transform.localScale = new Vector3(1, 1, 1);
                    NPCAnimator.Play("NG_Scare");
                    Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, NPC.transform.position, enemyMoveSpeed * Time.deltaTime);
                    EMAnimator.Play("N_PatrolAnim");
                    if (Vector2.Distance(Enemy.transform.position, NPC.transform.position) <= 1.6f)
                    {
                        Stages = 4;
                    }
                    break;
                case 4:
                    if(counter >= 1)
                    {
                        NPCAnimator.Play("NG_PullStruggle");
                    }
                    else
                    {
                        counter += 1f * Time.deltaTime;
                        NPCAnimator.Play("NG_Pull");
                    }
                    
                    EMAnimator.Play("N_PullAnim");
                    NPC.transform.parent = Enemy.transform;


                    Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, exitPosition.transform.position, (enemyMoveSpeed -4f)* Time.deltaTime);
                    if (Vector2.Distance(Enemy.transform.position, exitPosition.transform.position) <= 5f)
                    {
                        Stages = 5;
                        counter = 0;
                    }
                    break;
                case 5:
                    Destroy(NPC);
                    Destroy(Enemy);
                    for (int i = 0; i < players.Length; i++)
                    {
                        players[i].GetComponent<P_controls>().enabled = true;
                    }
                    exitOnce = true;
                    break;
            }
        }
		
        
            
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == players[0].GetComponent<BoxCollider2D>() || collision == players[1].GetComponent<BoxCollider2D>())
        {
            if(!triggerOnce)
            {
                triggerOnce = true;
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<P_controls>().enabled = false;
                    players[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                Stages = 1;


            }
        }
    }
}
