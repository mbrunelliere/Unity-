using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float walkSpeed = 100;
    public AudioClip walk; 

    bool _isGrounded = true;
    bool _isCrounching = false;
    bool _isHurting = false;


    Animator animator;
    AudioSource Audio;
    Bounds bounds;
    SpriteRenderer renderer;
    GameObject map;
	Rigidbody2D rigidbody;

    const uint STATE_IDLE = 0;
    const uint STATE_WALK = 1;
    const uint STATE_JUMP = 2;
    const uint STATE_CROUNCH = 3;
    const uint STATE_LAND = 4;
    const uint STATE_FALL = 5;
    const uint STATE_HURT = 6;
    const uint STATE_DIE = 7;

    string _currentDirection = "left";
    uint _currentAnimationState = STATE_IDLE;

	bool is_Jumping = false;
	bool is_Falling = false;
    bool isCollected = true;

    int jewels = 0;
    UnityEngine.UI.Text jewelsTotalText;

    Color tmpColor;

    // Use this for initialization
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
        map = GameObject.FindWithTag("Map");
        renderer = this.GetComponent<SpriteRenderer>();
        bounds = map.GetComponent<Renderer>().bounds;
		rigidbody = this.GetComponent<Rigidbody2D>();

        jewelsTotalText = GameObject.Find("itemsNumber").GetComponent<UnityEngine.UI.Text>();
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

        float posX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        float posY = Mathf.Clamp(transform.position.y, bottomBound, topBound);

        transform.position = new Vector3(posX, posY, transform.position.z);


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
			if (coll.gameObject.name == "Blade" || coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform_Grotte" || coll.gameObject.name =="Crate" )
            {
				_isGrounded = true;
            }

            if (coll.gameObject.name == "Floor")
            {
                _isGrounded = true;
                changeState(STATE_IDLE);
            }

            if (coll.gameObject.name == "Ennemy")
            {
                if (contact.normal == new Vector2(1, 0))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 100));
                    changeState(STATE_HURT);
                    GameObject.Find("lifesNumber").SendMessage("updateCounter", 1);
                }
                else if (contact.normal == new Vector2(-1, 0))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 100));
                    changeState(STATE_HURT);
                    GameObject.Find("lifesNumber").SendMessage("updateCounter", 1);
                }

                else if (contact.normal == new Vector2(0, -1))
                {
                    Debug.Log("mort ennemi");
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
                    changeState(STATE_JUMP);
                }
            }
            if (coll.gameObject.name == "jewel") 
            {
                Debug.Log(isCollected);
                Destroy(coll.gameObject);
                if(isCollected)
                {
                    isCollected = false;
                    GameObject.Find("itemsNumber").SendMessage("updateCounter", 1);
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

    void GameOver(bool isDied)
    {
        // Animation to Alpha 0
        tmpColor = renderer.color;
        renderer.color = Color.Lerp(tmpColor, new Color(0f, 0f, 0f, 0f), Time.fixedDeltaTime * 20.0f);
    }

} 
