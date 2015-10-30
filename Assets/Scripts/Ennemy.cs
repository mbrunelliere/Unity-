using UnityEngine;
using System.Collections;

public class Ennemy : ACharacter {

	// Use this for initialization
	void Start () {
        base.Start();
        Debug.Log("Monstre qui marche");
	}
	 
	// Update is called once per frame
	void Update () {
        base.Update();
        move();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Wall") { 
                changeDirection();
        }
    }


    void GameOver(bool state)
    {
        base.GameOver(state);
    }

    void changeDirection()
    {
        base.changeDirection();
    }

}
