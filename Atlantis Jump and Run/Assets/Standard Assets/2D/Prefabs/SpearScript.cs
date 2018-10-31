using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //gameObject.GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0.2f, 0);
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }
    public bool iJustSpawned = true;
    public bool imStuck = false;
	// Update is called once per frame
	void Update () {
       
		
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //wenn coll ne wand ist ist bleibt der Speer stecken
        
        if (!iJustSpawned&& coll.gameObject.tag != "Spear"&& coll.gameObject.tag != "SpearPickup"&& coll.gameObject.tag != "slimey")
        {
            if (coll.gameObject.tag == "Player"&&!imStuck)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000));
                
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = 
                    RigidbodyConstraints2D.FreezePosition |
                    RigidbodyConstraints2D.FreezeRotation;
                imStuck = true;
            }
        }
        
        
    }

    

    private void OnCollisionExit2D(Collision2D coll)
    {
        iJustSpawned = false;

        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
