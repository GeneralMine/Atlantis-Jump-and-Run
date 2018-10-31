using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class DeathStar : MonoBehaviour {
    public bool play = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (play)
        {
            enabled = true;
            GetComponent<VideoPlayer>().Play();
        }
	}
    public void startExplosion()
    {
        enabled = true;
        GetComponent<VideoPlayer>().Play();
    }
}
