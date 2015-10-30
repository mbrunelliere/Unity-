using UnityEngine;
using System.Collections;

public class EnnemyScript : MonoBehaviour {

    public float walkSpeed = 100;

    const uint STATE_WALK = 1;
    const uint STATE_DIE = 2;

    Animator animator;
    SpriteRenderer renderer;
    Bounds bounds;
    GameObject map;
	Rigidbody2D rigidbody;
    public AnimationClip Monster_die;

    uint _currentAnimationState = STATE_WALK;
    string _currentDirection = "left";

    Color tmpColor;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        renderer = this.GetComponent<SpriteRenderer>();
        map = GameObject.FindWithTag("Map");
        bounds = map.GetComponent<Renderer>().bounds;
		rigidbody = this.GetComponent<Rigidbody2D>();
    }
	 
	// Update is called once per frame
	void FixedUpdate () {

        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;

        float newPos = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);

        if (transform.position.x == leftBound || transform.position.x == rightBound)
        {
            if (_currentDirection == "left")
            {
                changeDirection("right");
            }
            else if (_currentDirection == "right") 
            {
                changeDirection("left");
            }
        }
        

        if (_currentDirection == "left")
        {
			rigidbody.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);
        }
        else if (_currentDirection == "right")
        {
			rigidbody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);
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
        foreach (ContactPoint2D contact in coll.contacts)
        { 
            if (coll.gameObject.name == "Player")
            {
 
                if(contact.normal == new Vector2(0, -1))
                {
                    Debug.Log("Mort ennemi");
                    die();
                }
                else if (contact.normal == new Vector2(1, 0))
                {
                    Debug.Log("Choc gauche");
                    changeDirection("right");
                }
                else if (contact.normal == new Vector2(-1, 0))
                {
                    Debug.Log("Choc droit");
                    changeDirection("left");
                }

            }

        }
         
    }

    void OnTriggerEnter(Collider collider)
    {
		Debug.Log("monstre vs mur");
        if (collider.gameObject.name != "Player" && collider.gameObject.name == "Floor")
        {
            if (_currentDirection == "left")
            {
                changeDirection("right");
            }
            else if (_currentDirection == "right")
            { 
                changeDirection("left");
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
        //Change state 
        changeState(STATE_DIE);
        // Animation to Alpha 0
        tmpColor = renderer.color;
        renderer.color = Color.Lerp(tmpColor, new Color(0f, 0f, 0f, 0f), Time.fixedDeltaTime * 20.0f);
        // Destruct gameObject
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
