using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_controls : MonoBehaviour {

    public float JumpSpeed;
    public float walkSpeed;
    public float climbSpeed;
    private float moveHorizontal;

    private Rigidbody2D rb2d;
    private BoxCollider2D b2d;
    private float iniGravity;

	private Vector2 IniColSize;
	private Vector2 HalfColSize;
	private Vector2 CrouchReposition;
	private float xPos;

    private Vector2 climbPosition;


    private bool IsCrouch;
    // To disable jump in vent
    public bool onVent = false;

    public bool OnLadder; // after throw, it will stay true which cause player cannot jump -> OnTriggerStay

    [Header("Ignore everything below")]
    public KeyCode KeyUp;
    public KeyCode KeyUse;
    public KeyCode KeyDown;
    public KeyCode KeyLeft;
    public KeyCode KeyRight;
    
    public bool isPlayer1;
    public Animator anim;
    public string[] animList;


    private void Awake()
    {
        animList = new string[4];
        anim = GetComponent<Animator>();
        // setting up all the keys for 2 players;
        if (gameObject.tag == "Player")
        {
            KeyUp = KeyCode.W;
            KeyUse = KeyCode.E;
            KeyDown = KeyCode.S;
            KeyLeft = KeyCode.A;
            KeyRight = KeyCode.D;
            isPlayer1 = true;
            animList[0] = "B_IdleAnim";
            animList[1] = "B_WalkAnim";
            animList[2] = "B_CrawlIdleAnim";
            animList[3] = "B_CrawlAnim";
        }
        else if (gameObject.tag == "Player2")
        {
            KeyUp = KeyCode.UpArrow;
            KeyUse = KeyCode.M;
            KeyDown = KeyCode.DownArrow;
            KeyLeft = KeyCode.LeftArrow;
            KeyRight = KeyCode.RightArrow;
            isPlayer1 = false;
            animList[0] = "G_IdleAnim";
            animList[1] = "G_WalkAnim";
            animList[2] = "G_CrawlIdleAnim";
            animList[3] = "G_CrawlAnim";
        }
    }

    void Start()
    {
        // setup rigidbody for easier use(just for saving getcomponent<rigidbody2d>() space);

        rb2d = GetComponent<Rigidbody2D>();
        iniGravity = rb2d.gravityScale;

        // setup boxcollider2d for easier use(just for saving getcomponent<boxcollider2d>() space) -> btw, why boxcollider but not circle collider?

        b2d = GetComponent<BoxCollider2D>();
        
		// Get initial collider size
		IniColSize = b2d.size;
		// Half its height
		HalfColSize = new Vector2 (b2d.size.x, b2d.size.y/2);
        
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
		// keep track of xPosition
		xPos = transform.position.x;

        if(isPlayer1)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
        }

        if (IsCrouch)
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 2f || gameObject.GetComponent<Rigidbody2D>().velocity.x < -2f)
            {
                anim.Play(animList[3]);
            }
            else
            {
                anim.Play(animList[2]);
            }
        }
        

        if (!GetComponent<P_throw>().onThrow) {

            if (!OnLadder){

                if (Input.GetKeyDown(KeyLeft))
                {
                    var tempScale = transform.localScale.x;
                    tempScale = Mathf.Abs(tempScale);
                    transform.localScale = new Vector3(-tempScale, transform.localScale.y, transform.localScale.z); // player flipping
                }
                else if (Input.GetKeyDown(KeyRight))
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // player flipping
                }

                if (!IsCrouch && !onVent)
                {
                    if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 2f || gameObject.GetComponent<Rigidbody2D>().velocity.x < -2f)
                    {
                        anim.Play(animList[1]);
                    }
                    else
                    {
                        anim.Play(animList[0]);
                    }
                }
                
                // just moving lmao
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * walkSpeed, rb2d.velocity.y);
            }

			//jump // can't jump after throw, unless move to another object/platform
			if (Input.GetKeyDown(KeyUp) && Grounded() && !OnLadder)
			{
                if (!onVent) Jump();
			}

			//crouch
			if (Grounded()) 
			{
                if (!onVent) Crouch();
			}
		}
        climbPosition.y = transform.position.y;
    }

    void Jump()
    {
       // add force to jump (DOUBT WILL BE USING THIS FOR THE GAME)
       rb2d.AddForce(Vector2.up * JumpSpeed * 1000);
    }

    private bool ladderPositionChanged = false;

	private void OnTriggerStay2D(Collider2D ladder) 
    {

        climbPosition.x = ladder.gameObject.transform.position.x;
        //! check whether the player is near ladder or not
        if (ladder.gameObject.tag == "Climbable") {
            rb2d.gravityScale = 0;
			if (Input.GetKey(KeyUp))
			{
                OnLadder = true;
                if (!ladderPositionChanged){
                    transform.position = climbPosition;
                    ladderPositionChanged = true;
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);
			}
			else if (Input.GetKey(KeyDown))
			{
                OnLadder = true;
                if (!ladderPositionChanged)
                {
                    transform.position = climbPosition;
                    ladderPositionChanged = true;
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
			}
			else
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			}
		}
    }

    private void OnTriggerExit2D(Collider2D ladder)
    {
        // whenever player leaves the ladder
        if (ladder.gameObject.tag == "Climbable")
        {
            OnLadder = false;
            ladderPositionChanged = false;
            rb2d.gravityScale = iniGravity;
        }
    }

    private void Crouch()
	{
		// Readjust the position Y of character so the coliider will be on the ground even when its size changes during crouch
		CrouchReposition = new Vector2(xPos, transform.position.y - HalfColSize.y);
		if (Input.GetKey(KeyDown)) // crouch
		{
			if (!IsCrouch) 
			{
				b2d.size = HalfColSize;
				transform.position = CrouchReposition;
				IsCrouch = true;
            }
		}
		else if (Input.GetKeyUp(KeyDown))
		{
			b2d.size = IniColSize;
			IsCrouch = false;
		}
	}
}
