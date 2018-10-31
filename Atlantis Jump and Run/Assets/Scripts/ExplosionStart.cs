using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class ExplosionStart : MonoBehaviour {
    public Button button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void startExplosion()
    {
        enabled = true;
        button.gameObject.SetActive(false);
        GetComponent<AudioSource>().Play();
        GetComponent<VideoPlayer>().Play();
    }
}
