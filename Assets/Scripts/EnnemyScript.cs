using UnityEngine;
using System.Collections;

public class EnnemyScript : MonoBehaviour {

    public float walkSpeed = 1;

    const uint STATE_WALK = 1;
    const uint STATE_DIE = 2;

    Animator animator;

    uint _currentAnimationState = STATE_WALK;
    string _currentDirection = "left";

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	 
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(_currentDirection);
        if (_currentDirection == "left")
        {
            transform.Translate(Vector3.left * walkSpeed * Time.fixedDeltaTime);
        }
        else if (_currentDirection == "right")
        {
            transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);
        }
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
        Debug.Log("collision");
        if(_currentDirection == "left")
        {
            changeDirection("right");
        }
        else if (_currentDirection == "right")
        {
            changeDirection("left");
        }

        if (coll.gameObject.name == "Player" )
        {
            foreach (ContactPoint2D contact in coll.contacts)
            {
                if (contact.point.y >= -0.64 && contact.point.x >= 0 && contact.point.x <= 0.52) {
                    Debug.Log("Mort ennemi");
                    die();
                }
            }
        }
         
    }

    void changeDirection(string direction) 
    {
        if (_currentDirection != direction)
        {
            Vector3 theScale = transform.localScale;
            if (direction == "left")
            {
                theScale.x = -1;
            }
            else if (direction == "right")
            {
                theScale.x = 1;
            }
            transform.localScale = theScale;
            _currentDirection = direction;
        }
    }

    void die()
    {
        changeState(STATE_DIE);
    }
}
