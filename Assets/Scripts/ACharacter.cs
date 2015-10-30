using UnityEngine;
using System.Collections;

public class ACharacter : MonoBehaviour {

    public float walkSpeed = 1;
    public GameObject limitsUniverse;
    Bounds bounds;
    SpriteRenderer renderer;
    Rigidbody2D rigidbody;
    Animator animator;

    bool _isGrounded = true;

    Color tmpColor;

    // Use this for initialization 
    public void Start()
    { 
        renderer = this.GetComponent<SpriteRenderer>();
        rigidbody = this.GetComponent<Rigidbody2D>(); 
        bounds = limitsUniverse.GetComponent<Renderer>().bounds;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
	public void Update()
	{
        //Keep the character in the universe
        //checkLimits();
    }

	//Physics update
	public void FixedUpdate()
    {
       
    }

    //Todo: DIE
    public void GameOver(bool isDead)
    {
        animator.SetBool("isDead", isDead);
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
      
    //Todo: Move
    public void move()
    {
        animator.SetBool("isWalking", true); 
        rigidbody.velocity = new Vector2(walkSpeed * transform.localScale.x * Time.fixedDeltaTime, rigidbody.velocity.y);
    }
	
	//Todo: Change direction
	public void changeDirection()
	{
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            move();
    }

}
