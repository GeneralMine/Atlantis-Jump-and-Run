using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        public Text deathcounter;
        int deathcount;
        bool once = false;
        private void Start()
        {
            deathcount = PlayerPrefs.GetInt("Player Deaths");
            deathcounter.text = "DEATHS: " + deathcount;
            Debug.Log(deathcount);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player" && once == false)
            {
                once = true;
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
                deathcount += 1;
                deathcounter.text = "DEATHS: " + deathcount;
                PlayerPrefs.SetInt("Player Deaths", deathcount);
                Debug.Log(deathcount);
            }
        }
    }
}