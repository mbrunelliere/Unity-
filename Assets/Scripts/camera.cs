using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    Bounds bounds;
    GameObject map;
    GameObject player;

    float minX;
    float maxX;
    float minY;
    float maxY;

    // Use this for initialization
    void Start () {
        map = GameObject.FindWithTag("Map");
        bounds = map.GetComponent<Renderer>().bounds;
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void LateUpdate () {

        Vector3 worldToScreen = Camera.main.ScreenToWorldPoint(transform.position);

        float leftBound = bounds.min.x + 4 ;
        float rightBound = bounds.max.x - 4;
        float bottomBound = bounds.min.y + 1.8f; 
        float topBound = bounds.max.y - 1.8f; 
          
        float camX = Mathf.Clamp(player.transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(player.transform.position.y, bottomBound , topBound);

        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
