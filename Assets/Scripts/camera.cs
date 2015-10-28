using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    Bounds bounds;
    GameObject map;

    // Use this for initialization
    void Start () {
        map = GameObject.FindWithTag("Map");
        bounds = map.GetComponent<Renderer>().bounds;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;
        float bottomBound = bounds.min.y;
        float topBound = bounds.max.y;

        float camX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(transform.position.y, bottomBound, topBound);

        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
