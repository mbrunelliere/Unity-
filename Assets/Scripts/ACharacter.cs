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
	void Update()
	{
		
	}

	//Physics update
	void FixedUpdate()
    {
       
    }

	//Todo: DIE

	//Todo: Move

	//Todo: Change direction
    void changeDirection(string direction)
    {

    }
}
