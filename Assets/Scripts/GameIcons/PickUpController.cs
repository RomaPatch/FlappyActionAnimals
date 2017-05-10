using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Shield") { return; }

        if (gameObject.tag == "Gem") { LevelManager.instance.GemPickedUp();}
        if (gameObject.tag == "Key") { LevelManager.instance.KeyPickedUp();}
        if (gameObject.tag == "Chest") { LevelManager.instance.ChestPickedUp();}
        Destroy(gameObject);
    }
}
