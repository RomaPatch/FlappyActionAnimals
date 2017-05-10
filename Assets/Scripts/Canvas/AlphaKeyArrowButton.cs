using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlphaKeyArrowButton : MonoBehaviour {


    void OnEnable()
    {
        transform.GetComponent<Button>().targetGraphic.CrossFadeAlpha(0.7f, 0, false);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
