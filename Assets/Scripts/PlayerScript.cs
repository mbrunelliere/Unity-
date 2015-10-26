using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float walkSpeed = 1;

    bool _isGrounded = true;


    Animator animator;

    const uint STATE_IDLE = 0;
    const uint STATE_WALK = 1;
    const uint STATE_JUMP = 2;
     
    string _currentDirection = "right";
    uint _currentAnimationState = STATE_IDLE;

    // Use this for initialization 
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    { 
	     if (Input.GetKey(KeyCode.RightArrow))
        {
            changeDirection("right");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            changeDirection("left");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
            }
        }
        else if (_isGrounded)
        {
            changeState(STATE_IDLE);
        }
    }

    void changeState(uint state)
    {
        if (_currentAnimationState == state)
        {
            return; 

            animator.SetInteger("state", (int)state);
            _currentAnimationState = state;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
        }
        Debug.Log("une collision ...");
    }
     
    void changeDirection(string direction)
    {
        if (_currentDirection != direction) 
        {
            if (direction == "right")
            {
                transform.Rotate(0, -180, 0);
            }
            else if (direction == "left")
            {
                transform.Rotate(0, 180, 0);
            }
            _currentDirection = direction;
        }
    }
}
