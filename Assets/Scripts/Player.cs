using UnityEngine;
using System.Collections;

public class Player : ACharacter {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

        //Player Mouvement
        /* if (Input.GetKeyDown(KeyCode.Space))
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
            rigidbody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);

            if (_isGrounded)
            {
                changeState(STATE_WALK);
                Audio.clip = walk;
                Audio.loop = false;
                Audio.Play();
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isCrounching && !_isHurting)
        {
            changeDirection("left");
            rigidbody.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);

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
        isCollected = true;
        isHurted = true; */
    }

    void FixedUpdate()
    {
        base.FixedUpdate();

    }

}
