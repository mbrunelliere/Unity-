using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

	[Tooltip("Ambiance sound for the game")]
    public AudioClip SongTheme;
	[Tooltip("The camera to see the game")]
	public Camera camera;
    [Tooltip("Decors tab used for parallax effect. The first element is the background and the last is the foreground.")]
	public GameObject[] ParallaxDecors; 
	[Tooltip("Speed parallax for background")]
	public float SpeedParallax = 0.3f;
	[Tooltip("Speed Range parallax beetween a background and the next")]
	public float RangeParallax = 0.2f;

	AudioSource Audio;
		
	Vector3[] StartPosition;
	Vector3 newPos;
	float oldPos;

	Vector3 camera_StartPosition;

    // Use this for initialization
    void Start () {

		Audio = this.GetComponent<AudioSource>();
		
        //Manage song theme
		playThemeSong();

		//Init parallax parameters
		initParallax();
	}
	
	// Update is called once per frame
	void Update () {
        //Manage parallax effect 
		updateParallax();
    } 

	/**************************
	 *  Parallax functions
	 * ***********************/

	void initParallax(){
		//init camera position
		camera_StartPosition = camera.transform.position;
		oldPos = camera_StartPosition.x;
		
		//Select decors elements
		StartPosition = new Vector3[ParallaxDecors.Length];
		for(int i=0; i<ParallaxDecors.Length; i++)
		{
			StartPosition[i] = ParallaxDecors[i].transform.position;
		}
		newPos = new Vector3(0,0,0);
	}
	
	
	void parallax(GameObject obj, float scrollSpeed, Vector3 obj_StartPosition )  
    {
		oldPos = camera.transform.position.x;
		newPos = obj_StartPosition - ((camera_StartPosition - camera.transform.position) * scrollSpeed );
		obj.transform.position = newPos;
    }

	void updateParallax() {
		if(camera.transform.position.x != oldPos) {
			for(int i=0; i<ParallaxDecors.Length; i++)
			{
				parallax(ParallaxDecors[i], SpeedParallax + (RangeParallax * i), StartPosition[i]);
			}
		}
	}

	/**************************
	 *  Sounds functions
	 * ***********************/

	void playThemeSong() {
		Audio.PlayOneShot(SongTheme, 1F);
	}
}
