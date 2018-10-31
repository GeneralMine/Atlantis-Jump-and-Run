﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    public static Transform[] points;
	// Use this for initialization
	void Awake () {
        points = new Transform[transform.childCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
