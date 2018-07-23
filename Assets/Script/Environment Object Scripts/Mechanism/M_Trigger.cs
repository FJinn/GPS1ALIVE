using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Trigger : MonoBehaviour {
    
    [Header("Determine moving horizontal or vertical")]
    public bool MoveHorizontal;
    public bool MoveVertical;
    public bool MoveToSpecificPoint;
    public GameObject PointPosition;
    
    [Header("Object's moving distance and direction")]
    public float MovingDistance;

    [Header("Object's moving speed")]
    public float MovingSpeed;

    private Vector3 StartingPoint;
    private Vector3 FinalResultPoint;

    private int[] counter;
    private bool inGame = false;
    private int clickCounts;

    [Header("If applied, won't be controlled by any units.")]
    public bool AutoMove;
    [Header("Applying wait time for auto moving")]
    public float waitTime;
    private float initWaitTime;

    private bool[] Directions;
    public bool movingToDestination;
    private Vector3 originPosition;

    [Header("Especially for MoveToSpecificPosition use")]
    public bool DoesItFall;
    public bool EnterToFall;
    public float distanceUntilFall;
    public LayerMask ignoreEnemy;
    private bool isOnPlatform;
    private Rigidbody2D rb2d;
    [Header("When fall, which stuff to destroy?")]
    public GameObject destroyBox;

    public bool isThisDoor = false;
    public bool OpenedAtStart = false;
    private Animator doorAnimator;
    private BoxCollider2D myCollider;
    private int openDoorCounts;

    private GameObject[] players;
    
    // Use this for initialization
    void Start() {
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Player");
        players[1] = GameObject.FindGameObjectWithTag("Player2");
        myCollider = GetComponent<BoxCollider2D>();


        if (isThisDoor)
        {
            doorAnimator = GetComponent<Animator>();
            openDoorCounts = 0;
            if(OpenedAtStart)
            {
                Trigger();
            }
        }

        if(DoesItFall)
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // 	0     1   2   3
        // left right up down
        Directions = new bool[4];
        counter = new int[2];

        // clearing garbage values
        for (int i = 0; i < 4; i++) {
            Directions[i] = false;
        }

        movingToDestination = false;
        originPosition = transform.position;

        // setting up origin point
        StartingPoint = transform.position;
        inGame = true;
        initWaitTime = waitTime;

        // for auto move
        if(AutoMove)
        {
            if (MovingDistance > 0)
            {
                if (MoveHorizontal)
                {
                    Directions[1] = true;
                    FinalResultPoint.x = StartingPoint.x + MovingDistance;
                }
                if (MoveVertical)
                {
                    Directions[2] = true;
                    FinalResultPoint.y = StartingPoint.y + MovingDistance;
                }
            }
            else
            {
                if (MoveHorizontal)
                {
                    Directions[0] = true;
                    FinalResultPoint.x = StartingPoint.x + MovingDistance;
                }
                if (MoveVertical)
                {
                    Directions[3] = true;
                    FinalResultPoint.y = StartingPoint.y + MovingDistance;
                }
            }
        }

    }

    // manual control , not auto
    public void Trigger()
    {

        clickCounts++;

        if(isThisDoor)
        {
            openDoorCounts++;

            if(openDoorCounts == 1)
            {
                myCollider.enabled = false;
                doorAnimator.Play("DoorUnlocked");
                FindObjectOfType<AudioManager>().Play("DoorUnlocking");
            }
            else if(openDoorCounts >= 2)
            {
                myCollider.enabled = true;
                doorAnimator.Play("Doorlock");
                openDoorCounts = 0;
                FindObjectOfType<AudioManager>().Play("DoorLocking");
            }

            return;

        }

        if (MoveToSpecificPoint)
        {
            if(clickCounts == 1)
            {
                movingToDestination = true;
            }else if(clickCounts >= 2)
            {
                movingToDestination = false;
                clickCounts = 0;
            }
        }



        if (MovingDistance > 0 )
        {
            if (MoveHorizontal)
            {
                // moving right
                if(clickCounts == 1)
                {
                    FinalResultPoint.x = StartingPoint.x + MovingDistance;
                    Directions[1] = true;
                    Directions[0] = false;
                }
                
                if(clickCounts >= 2)
                {
                    FinalResultPoint.x = StartingPoint.x;
                    Directions[0] = true;
                    Directions[1] = false;
                    clickCounts = 0;
                }
            }
            if (MoveVertical)
            {
                // moving up
                if (clickCounts == 1)
                {
                    FinalResultPoint.y = StartingPoint.y + MovingDistance;
                    Directions[2] = true;
                    Directions[3] = false;
                }

                if (clickCounts >= 2)
                {
                    FinalResultPoint.y = StartingPoint.y;
                    Directions[3] = true;
                    Directions[2] = false;
                    clickCounts = 0;
                }
            }
        }else {
            if (MoveHorizontal)
            {
                // moving left
                if(clickCounts == 1)
                {
                    FinalResultPoint.x = StartingPoint.x + MovingDistance;
                    Directions[0] = true;
                    Directions[1] = false;
                }
                if (clickCounts >= 2)
                {
                    FinalResultPoint.x = StartingPoint.x;
                    Directions[1] = true;
                    Directions[0] = false;
                    clickCounts = 0;
                }
            }
            if (MoveVertical)
            {
                // moving up
                if (clickCounts == 1)
                {
                    FinalResultPoint.y = StartingPoint.y + MovingDistance;
                    Directions[3] = true;
                    Directions[2] = false;
                }

                if (clickCounts >= 2)
                {
                    FinalResultPoint.y = StartingPoint.y;
                    Directions[2] = true;
                    Directions[3] = false;
                    clickCounts = 0;
                }
            }

        }
        

    }

    
    
    // auto move checking its edge
    private void PlatformCheck(int C, int D1, int D2)
    {
           if(waitTime <= 0)
        {
            counter[C]++;
            Directions[D1] = false;
            Directions[D2] = true;
            if (C == 0)
            {
                FinalResultPoint.x = StartingPoint.x;
            }
            else
            {
                FinalResultPoint.y = StartingPoint.y;
            }
            if (counter[C] >= 2)
            {
                if (C == 0)
                {
                    FinalResultPoint.x = StartingPoint.x + MovingDistance; ;
                }
                else
                {
                    FinalResultPoint.y = StartingPoint.y + MovingDistance; ;
                }
                counter[C] = 0;
            }
            waitTime = initWaitTime;
        }else
        {
            waitTime -= Time.deltaTime;
        }
           
    }
    

    // Update is called once per frame
    void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.F))
        {
            Trigger();
        } 
        */


        if(MoveToSpecificPoint && rb2d.isKinematic)
        {
            if (movingToDestination)
            {
                transform.position = Vector2.MoveTowards(transform.position, PointPosition.transform.position, Time.deltaTime * MovingSpeed);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, originPosition, Time.deltaTime * MovingSpeed);
            }
        }

        // left
        if (Directions[0]) {
            if (transform.position.x >= FinalResultPoint.x) {
                transform.position -= transform.right * Time.deltaTime * MovingSpeed;
            }else if(AutoMove)
            {
                PlatformCheck(0, 0, 1);
            }

            
        }
        // right
        else if (Directions[1]) {
            if (transform.position.x <= FinalResultPoint.x) {
                transform.position += transform.right * Time.deltaTime * MovingSpeed;
            }
            else if (AutoMove)
            {
                PlatformCheck(0, 1, 0);
            }
             
        }
        
      // up
        if (Directions[2]) {
            if (transform.position.y <= FinalResultPoint.y) {
                transform.position += transform.up * Time.deltaTime * MovingSpeed;
            } else if (AutoMove)
            {
               PlatformCheck(1, 2, 3);
            }
        }
        // down
        else if (Directions[3]) {
            if (transform.position.y >= FinalResultPoint.y) {
                transform.position -= transform.up * Time.deltaTime * MovingSpeed;
        }
        else if(AutoMove)
        {
           PlatformCheck(1, 3, 2);
        }
        }

        }
    

        //Estimating Distance for Designers
        private void OnDrawGizmos()
        {
            if (MoveHorizontal && !inGame) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + MovingDistance, transform.position.y, transform.position.z));

            } else
            if (inGame && MoveHorizontal)
            {
                Debug.DrawLine(transform.position, new Vector3(StartingPoint.x + MovingDistance, StartingPoint.y, 0));
            }
            if (MoveVertical && !inGame) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + MovingDistance, transform.position.z));

            } else
            if (inGame && MoveVertical) {
                Debug.DrawLine(transform.position, new Vector3(StartingPoint.x, StartingPoint.y + MovingDistance, 0));
            }

            if(MoveToSpecificPoint)
            {
            Debug.DrawLine(transform.position, PointPosition.transform.position);
            }
        }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Player2"))
        {
            isOnPlatform = true;
        }

        if(DoesItFall)
        {
            if (!GetComponent<Rigidbody2D>().isKinematic)
            {
                if(destroyBox != null)
                {
                    if (collision.collider == destroyBox.GetComponent<BoxCollider2D>())
                    {
                        destroyBox.GetComponent<BoxCollider2D>().enabled = false;
                        foreach (Transform child in destroyBox.transform)
                        {
                            if (child.GetComponent<Rigidbody2D>() != null)
                            {
                                child.GetComponent<Rigidbody2D>().isKinematic = false;
                                child.transform.rotation = Quaternion.EulerRotation(0, 0, Random.Range(-20f, 20f));
                                child.GetComponent<BoxCollider2D>().isTrigger = true;
                                Destroy(child.gameObject, 3f);
                            }
                        }
                    }
                }
                

                Physics2D.IgnoreCollision(myCollider, players[0].GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(myCollider, players[1].GetComponent<BoxCollider2D>());

                Collider2D[] ignoreEnemies = Physics2D.OverlapCircleAll(transform.position, 5000f, ignoreEnemy);
                for(int i =0;i < ignoreEnemies.Length; i++)
                {
                    Physics2D.IgnoreCollision(myCollider, ignoreEnemies[i]);
                }

                transform.rotation = Quaternion.EulerRotation(0, 0, Random.Range(-5f, 5f));
                Destroy(gameObject, 3f);
            }
        }

        if(EnterToFall)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), players[0].GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), players[1].GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), destroyBox.GetComponent<BoxCollider2D>());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(DoesItFall)
        {
            if (Vector2.Distance(transform.position, PointPosition.transform.position) < distanceUntilFall)
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), players[0].GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), players[1].GetComponent<BoxCollider2D>());
            }

            if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Player2"))
            {
                isOnPlatform = false;
            }
        }
        
    }
}

