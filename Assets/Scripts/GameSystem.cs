using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    GameObject trees;
    GameObject clouds;
    Vector3 trees_StartPosition;
    Vector3 clouds_StartPosition;
    Vector3 newPos;

    float old_bg_posX;

    // Use this for initialization
    void Start () {
        //Select decors elements
        trees = GameObject.Find("bg_trees"); 
        clouds = GameObject.Find("bg_clouds");
        trees_StartPosition = trees.transform.position;
        clouds_StartPosition = clouds.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //Manage parallax effect
        parallax(trees, 0.2f);
        parallax(clouds, 0.1f); 
    } 

    void parallax(GameObject obj, float scrollSpeed)  
    {
        //TODO : keep the x position
        newPos = new Vector3(Camera.main.transform.position.x * scrollSpeed, obj.transform.position.y, obj.transform.position.z);
        obj.transform.position = newPos; 
    }
}
