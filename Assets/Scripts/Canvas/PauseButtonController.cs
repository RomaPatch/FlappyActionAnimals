using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseButtonController : MonoBehaviour,IPointerClickHandler
{
    public GameObject pausedPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.isPaused){return;}
        pausedPanel.GetComponent<PausedGamePanelController>().Show();
        GameManager.instance.PauseGame();
       
    }
}
