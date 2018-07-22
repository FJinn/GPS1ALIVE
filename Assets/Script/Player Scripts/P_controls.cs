using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class P_controls : MonoBehaviour {

    public float JumpSpeed;
    public float walkSpeed;
    public float climbSpeed;
    private float moveHorizontal;

    private Rigidbody2D rb2d;
    private BoxCollider2D b2d;
    private float iniGravity;
    private GameObject[] walls;
    private GameObject otherPlayer;
    private SpriteRenderer mySpriteRenderer;
    private int orderingLayer;
    
    // To disable jump in vent
    public bool onVent = false;
    // To disable jump when pull/push box
    public bool noJump = false;

    private bool ignoreLadderOnce;
    public bool OnLadder; // after throw, it will stay true which cause player cannot jump -> OnTriggerStay
    public float myVelocityX;

    [Header("Ignore everything below")]
    public KeyCode KeyUp;
    public KeyCode KeyUse;
    public KeyCode KeyDown;
    public KeyCode KeyLeft;
    public KeyCode KeyRight;
    
    public bool isPlayer1;
    public Animator anim;
    public string[] animList;

    public bool StopGameControl;
    public bool CameraStarted = false;

    private BoxCollider2D myBoxCollider;
    public bool faceLeft = false;
    public bool faceRight = false;
    public bool openDoor = false;
    private bool inTheAir = false;

    public GameObject audioManager;

    private void Awake()
    {
        animList = new string[4];
        anim = GetComponent<Animator>();
        walls = GameObject.FindGameObjectsWithTag("Walls");       
    }

    void Start()
    {
        // setup rigidbody for easier use(just for saving getcomponent<rigidbody2d>() space);

        rb2d = GetComponent<Rigidbody2D>();
        iniGravity = rb2d.gravityScale;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider = GetComponent<BoxCollider2D>();

        // setup boxcollider2d for easier use(just for saving getcomponent<boxcollider2d>() space) -> btw, why boxcollider but not circle collider?

        // setting up all the keys for 2 players;
        orderingLayer = mySpriteRenderer.sortingOrder;

        if (CompareTag("Player"))
        {
            KeyUp = KeyCode.W;
            KeyUse = KeyCode.E;
            KeyDown = KeyCode.S;
            KeyLeft = KeyCode.A;
            KeyRight = KeyCode.D;
            isPlayer1 = true;

            otherPlayer = GameObject.FindGameObjectWithTag("Player2");
            Physics2D.IgnoreCollision(myBoxCollider, otherPlayer.GetComponent<BoxCollider2D>());
        }
        else if (CompareTag ("Player2"))
        {
            KeyUp = KeyCode.UpArrow;
            KeyUse = KeyCode.Slash;
            KeyDown = KeyCode.DownArrow;
            KeyLeft = KeyCode.LeftArrow;
            KeyRight = KeyCode.RightArrow;
            isPlayer1 = false;

            otherPlayer = GameObject.FindGameObjectWithTag("Player");
            Physics2D.IgnoreCollision(myBoxCollider, otherPlayer.GetComponent<BoxCollider2D>());
        }


        Collider2D[] foundEnemies = Physics2D.OverlapCircleAll(transform.position, 500000f);
        for (int k = 0; k < foundEnemies.Length; k++)
        {
            if (foundEnemies[k].CompareTag("Enemy"))
            {
                Physics2D.IgnoreCollision(myBoxCollider, foundEnemies[k]);
            }
        }
    }

    private bool Grounded()
    {
        // cannot double jump or whatever while u're in air
        if (rb2d.velocity.y > 0.1 || rb2d.velocity.y < -0.1)
        {
            return false;
        } else
        {
            return true;
        }
    }

    void FixedUpdate()
	{
        if(isPlayer1)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
        }
       
        if (!GetComponent<P_throw>().throwStance && !StopGameControl) {

            if (!OnLadder){

                if (rb2d.velocity.x < -0.1f && gameObject.GetComponent<P_pushPull>().OnBox == false) 
                {
                    var tempScale = transform.localScale.x;
                    tempScale = Mathf.Abs(tempScale);
                    transform.localScale = new Vector3(-tempScale, transform.localScale.y, transform.localScale.z); // player flipping
                    faceLeft = true;
                    faceRight = false;                    
                }
                else if (rb2d.velocity.x > 0.1f && gameObject.GetComponent<P_pushPull>().OnBox == false)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // player flipping
                    faceLeft = false;
                    faceRight = true;                    
                }          

                // move

                rb2d.velocity = new Vector2(moveHorizontal * walkSpeed, rb2d.velocity.y);
                myVelocityX = rb2d.velocity.x;
               
            }

			//jump 
			if (Input.GetKeyDown(KeyUp) && Grounded() && !OnLadder&& !onVent)
			{
                Jump();
			}          
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        

        // Determine whether players wanna get locked within camera view, if camera started == true, yes player will get locked within camera view
        if (CameraStarted)
        {
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        if(rb2d.velocity.y < 0)
        {
            inTheAir = false;
        }
    }

    void Jump()
    {
        if (!noJump)
        {
            // add force to jump (DOUBT WILL BE USING THIS FOR THE GAME)
            if(!inTheAir)
            {
                rb2d.AddForce(Vector2.up * JumpSpeed * 1000);
                inTheAir = true;
            }
        }
    }

    private bool ladderPositionChanged = false;

    private void LadderCollision()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].GetComponent<TilemapCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(myBoxCollider, walls[i].GetComponent<TilemapCollider2D>());
            }
            if (walls[i].GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(myBoxCollider, walls[i].GetComponent<BoxCollider2D>());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D ladder) 
    {
        //! check whether the player is near ladder or not
        if (ladder.gameObject.tag == "Climbable") {

            mySpriteRenderer.sortingOrder = orderingLayer + 20;

            if (OnLadder)
            {
                if(!ignoreLadderOnce)
                {
                    LadderCollision();
                    ignoreLadderOnce = true;
                }
                rb2d.velocity = new Vector2(0, 0);
                rb2d.gravityScale = 0;
            }

            if (Input.GetKey(KeyUp)&& !StopGameControl)
			{
                OnLadder = true;
                rb2d.velocity = new Vector2(0, climbSpeed);
                transform.position = new Vector2(ladder.transform.position.x, transform.position.y);
            }   
			else if (Input.GetKey(KeyDown) && !StopGameControl)
			{
                OnLadder = true;
                rb2d.velocity = new Vector2(0, -climbSpeed);
                transform.position = new Vector2(ladder.transform.position.x, transform.position.y);
            }
			
		}
    }
    

    private void OnTriggerExit2D(Collider2D ladder)
    {
        // whenever player leaves the ladder
        if (ladder.gameObject.tag == "Climbable")
        {
            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i].GetComponent<TilemapCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(myBoxCollider, walls[i].GetComponent<TilemapCollider2D>(),false);
                }
                if (walls[i].GetComponent<BoxCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(myBoxCollider, walls[i].GetComponent<BoxCollider2D>(),false);
                }
            }
            OnLadder = false;
            rb2d.gravityScale = iniGravity;
            ignoreLadderOnce = false;
            mySpriteRenderer.sortingOrder = orderingLayer;

        }
    }

    public void WalkingSound()
    {
        FindObjectOfType<AudioManager>().Play("Walking");
    }

    public void JumpingSound()
    {
        FindObjectOfType<AudioManager>().Play("Jumping");
    }

    public void BoxDraggingSound()
    {
        FindObjectOfType<AudioManager>().Play("BoxDragging");
    }

    public void ClimbingSound()
    {
        FindObjectOfType<AudioManager>().Play("Climbing");
    }

    public void CrawlingSound()
    {
        FindObjectOfType<AudioManager>().Play("Crawling");
    }
}
