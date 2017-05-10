using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ResumeButtonController : MonoBehaviour, IPointerClickHandler { 
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.ResumeGame();
        transform.parent.GetComponent<PausedGamePanelController>().Hide();
    }
    
}
