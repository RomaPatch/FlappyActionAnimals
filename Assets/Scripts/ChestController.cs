﻿using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }
    
        LevelManager.instance.ChestPickedUp();
        transform.gameObject.SetActive(false);
    }
}
