using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class PressurePlate : MonoBehaviour {
    /*
    public Transform spawnLocations;
    public GameObject whatToSpawnPrefab;
    public GameObject whatToSpawnClone;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        { 
            whatToSpawnClone = Instantiate(whatToSpawnPrefab, spawnLocations.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
        }x
    }*/

    public GameObject moveObject;
    public bool used = false;
    public float moveBy;
    public bool explosion;
    public VideoPlayer video;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"|| collision.gameObject.tag == "Spear"&&used == false)
        {
            if (moveObject.activeSelf == true)
            {
                moveObject.SetActive(false);
            }
            else if (moveObject.activeSelf == false)
                moveObject.SetActive(true);
            used = true;
            Debug.Log("Explosion");
            if (explosion)
            {
                video.Play();
                explosion = false;
            }
        }
    }
}
