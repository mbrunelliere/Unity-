using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float walkSpeed = 1;

    bool _isGrounded = true;


    Animator animator;

    const uint STATE_IDLE = 0;
    const uint STATE_WALK = 1;
    const uint STATE_JUMP = 2;
     
    bool _currentDirection = true;
    uint _currentAnimationState = STATE_IDLE;


    // Use this for initialization 
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * walkSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if(move > 0 && !_currentDirection)
        {
            changeDirection();
        }
        if (move < 0 && _currentDirection)
        {
            changeDirection();
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
     
    void changeDirection()
    {
        _currentDirection = !_currentDirection;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
