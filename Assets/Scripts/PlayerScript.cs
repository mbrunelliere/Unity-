using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float walkSpeed = 1;

    bool _isGrounded = true;
    bool _isCrounching = false;
    bool _isHurting = false;


    Animator animator;
    Bounds bounds;
    GameObject map;

    const uint STATE_IDLE = 0;
    const uint STATE_WALK = 1;
    const uint STATE_JUMP = 2;
    const uint STATE_CROUNCH = 3;
    const uint STATE_LAND = 4;
    const uint STATE_FALL = 5;
    const uint STATE_HURT = 6;

    string _currentDirection = "left";
    uint _currentAnimationState = STATE_IDLE;
  

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        map = GameObject.FindWithTag("Map");
        bounds = map.GetComponent<Renderer>().bounds;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        //Camera Movement
        Camera cam = GetComponent<Camera>(); 

        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;
        float bottomBound = bounds.min.y; 
        float topBound = bounds.max.y;

        float camX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(transform.position.y, bottomBound, topBound);

        cam.transform.position = new Vector3(camX, camY, cam.transform.position.z);


        //Player Movement
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                changeState(STATE_LAND); 
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_isGrounded) 
            {
                _isGrounded = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
            }
            changeState(STATE_JUMP);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !_isCrounching && !_isHurting)
        {
            changeDirection("right");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isCrounching && !_isHurting)
        {
            changeDirection("left");
            transform.Translate(Vector3.left * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            changeState(STATE_CROUNCH);
        }
        else if (_isGrounded)
        {
            changeState(STATE_IDLE);
        } 
        else if (!_isGrounded)
        {
            changeState(STATE_FALL);
        }

        _isCrounching = animator.GetCurrentAnimatorStateInfo(0).IsName("Patch_duck");
        _isHurting = animator.GetCurrentAnimatorStateInfo(0).IsName("Patch_hurt");
    }

    void changeState(uint state)
    {
        if (_currentAnimationState == state)
        {
            return;
        }
        animator.SetInteger("state", (int)state);
        _currentAnimationState = state;
          
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (coll.gameObject.name == "Blade")
            {
                
            }

            if (coll.gameObject.name == "Floor")
            {
                _isGrounded = true;
                changeState(STATE_IDLE);
                //Debug.Log("une collision ...");
            }

            if (coll.gameObject.name == "Ennemy")
            {
                if(contact.normal == new Vector2(1, 0))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 100));
                    changeState(STATE_HURT);
                }
                else if (contact.normal == new Vector2(-1, 0))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 100));
                    changeState(STATE_HURT);

                }
                
                else if (contact.normal == new Vector2(0, -1))
                {
                    Debug.Log("mort ennemi"); 
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
                    changeState(STATE_JUMP);
                }
            }
        }

    }

    void changeDirection(string direction)
    {
        if (_currentDirection != direction)
        {
            Vector3 theScale = transform.localScale;
            if (_currentDirection == "left")
            {
                theScale.x = 1;
            }
            if (_currentDirection == "right")
            {
                theScale.x = -1;
            }
            transform.localScale = theScale;
            _currentDirection = direction;
        }
    }
}
