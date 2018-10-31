using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class SceneController : MonoBehaviour {

    public static SceneController Instance;
    private string sceneName;
    public VideoPlayer video;
    bool start = false;
	// Use this for initialization
	void Awake () {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
    private void Update()
    {
        if(!(sceneName == "") && video.isPlaying==false && start == true)
        {
            SceneManager.LoadScene(sceneName);
            sceneName = "";
            start = false;
        }
    }
    public void loadScene(string scene)     
    {
        start = true;
        sceneName = scene;
        //SceneManager.LoadScene(scene);
    }
}
