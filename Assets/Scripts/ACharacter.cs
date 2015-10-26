using UnityEngine;
using System.Collections;

public class ACharacter : MonoBehaviour {

    public float walkSpeed = 1;
    bool _isGrounded = true;


    Animator animator;

    enum States {
        idle = 0,
        walk = 1,
        jump = 2
    };


    string _currentDirection = "right";
    uint _currentAnimationState = (uint)States.idle;

    // Use this for initialization 
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            changeDirection("right");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState((uint)States.walk);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            changeDirection("left");
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                changeState((uint)States.walk);
            }
        }
        else if (_isGrounded)
        {
            changeState((uint)States.idle);
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
            changeState((uint)States.idle);
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
