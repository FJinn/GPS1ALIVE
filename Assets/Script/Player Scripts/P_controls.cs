using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class P_controls : MonoBehaviour {

    public float JumpSpeed;
    public float walkSpeed;
    public float climbSpeed;
    private float moveHorizontal;
    public AudioSource audiosource2;

    private Rigidbody2D rb2d;
    private BoxCollider2D b2d;
    private float iniGravity;
    private GameObject[] walls;
    

    private Vector2 climbPosition;
    
    // To disable jump in vent
    public bool onVent = false;
    // To disable jump when pull/push box
    public bool noJump = false;

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
    public bool Walking = false;
    public bool Jumping = false;
    public bool Idle = false;
    public bool Crawling = false;
    public bool CrawlingIdle = false;

    private void Awake()
    {
        animList = new string[4];
        anim = GetComponent<Animator>();
        walls = GameObject.FindGameObjectsWithTag("Walls");

        // setting up all the keys for 2 players;
        if (gameObject.tag == "Player")
        {
            KeyUp = KeyCode.W;
            KeyUse = KeyCode.E;
            KeyDown = KeyCode.S;
            KeyLeft = KeyCode.A;
            KeyRight = KeyCode.D;
            isPlayer1 = true;

            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player2").GetComponent<BoxCollider2D>());
        }
        else if (gameObject.tag == "Player2")
        {
            KeyUp = KeyCode.UpArrow;
            KeyUse = KeyCode.Slash;
            KeyDown = KeyCode.DownArrow;
            KeyLeft = KeyCode.LeftArrow;
            KeyRight = KeyCode.RightArrow;
            isPlayer1 = false;
            animList[0] = "G_IdleAnim";
            animList[1] = "G_WalkAnim";
            animList[2] = "G_CrawlIdleAnim";
            animList[3] = "G_CrawlAnim";

            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>());
        }

        Collider2D[] foundEnemies = Physics2D.OverlapCircleAll(transform.position, 500000f);
        for (int k = 0; k < foundEnemies.Length; k++)
        {
            if(foundEnemies[k].CompareTag("Enemy"))
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), foundEnemies[k]);
            }
        }
    }

    void Start()
    {
        // setup rigidbody for easier use(just for saving getcomponent<rigidbody2d>() space);

        rb2d = GetComponent<Rigidbody2D>();
        iniGravity = rb2d.gravityScale;
    //    myBoxCollider = GetComponent<BoxCollider2D>();

        // setup boxcollider2d for easier use(just for saving getcomponent<boxcollider2d>() space) -> btw, why boxcollider but not circle collider?

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
       
        if (!GetComponent<P_throw>().onThrow && !StopGameControl) {

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

                if (!onVent)
                {
                    if (rb2d.velocity.x > 0.1f || rb2d.velocity.x < -0.1f)
                    {
                        //anim.Play(animList[1]);
                        Walking = true;
                        Idle = false;
                        Crawling = false;
                        CrawlingIdle = false;
                        audiosource2.UnPause();
                    }
                    else
                    {
                        //anim.Play(animList[0]);
                        Walking = false;
                        Idle = true;
                        Crawling = false;
                        CrawlingIdle = false;
                        audiosource2.Pause();
                    }
                }
                else
                {
                    if (rb2d.velocity.x > 0.3f || rb2d.velocity.x < -0.3f)
                    {                   
                        //anim.Play(animList[3]);
                        CrawlingIdle = false;
                        Crawling = true;
                        
                        
                       
                    }
                    else
                    {                       
                        //anim.Play(animList[2]);
                        CrawlingIdle = true;
                        Crawling = false;                                         
                    }
                }

                // move

                rb2d.velocity = new Vector2(moveHorizontal * walkSpeed, rb2d.velocity.y);
                
               
            }

			//jump // can't jump after throw, unless move to another object/platform
			if (Input.GetKeyDown(KeyUp) && Grounded() && !OnLadder&& !onVent)
			{
                Jump();
                Jumping = true;
			}               
            else if(Grounded() && !OnLadder && !onVent)
            {
                Jumping = false;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        climbPosition.y = transform.position.y;

        if (CameraStarted)
        {
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }

    void Jump()
    {
        if (!noJump)
        {
            // add force to jump (DOUBT WILL BE USING THIS FOR THE GAME)
            rb2d.AddForce(Vector2.up * JumpSpeed * 1000);
        }
    }

    private bool ladderPositionChanged = false;

	private void OnTriggerStay2D(Collider2D ladder) 
    {

        climbPosition.x = ladder.gameObject.transform.position.x;
        //! check whether the player is near ladder or not
        if (ladder.gameObject.tag == "Climbable") {
            rb2d.gravityScale = 0;

            for(int i =0;i < walls.Length; i ++)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>());
            }

			if (Input.GetKey(KeyUp))
			{
                OnLadder = true;
                if (!ladderPositionChanged){
                    transform.position = climbPosition;
                    ladderPositionChanged = true;
                }
                rb2d.velocity = new Vector2(0, climbSpeed);
			}
			else if (Input.GetKey(KeyDown))
			{
                OnLadder = true;
                if (!ladderPositionChanged)
                {
                    transform.position = climbPosition;
                    ladderPositionChanged = true;
                }
                rb2d.velocity = new Vector2(0, -climbSpeed);
			}
			else
			{
				rb2d.velocity = new Vector2(0, 0);
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
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>(),false);
            }
            OnLadder = false;
            ladderPositionChanged = false;
            rb2d.gravityScale = iniGravity;
        }
    }
    
}
