using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float walkSpeed = 1;

    bool _isGrounded = true;
    bool _isCrounching = false;
    bool _islanding = false;
    bool _isJumping = false;
    bool _isFalling = false;

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
        //bounds = map.GetComponent<SpriteRenderer>().sprite.bounds;
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
        //Debug.Log(leftBound + " " + rightBound + " " + bottomBound + " " + topBound);

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
                changeState(STATE_JUMP);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !_isCrounching)
        {
            changeDirection("right");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isCrounching )
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
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
            Debug.Log("une collision ...");
        }

        if (coll.gameObject.name == "Ennemy")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 150));
            changeState(STATE_HURT);
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
