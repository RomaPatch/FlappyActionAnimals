using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PausedGamePanelController : MonoBehaviour
{



    private CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        Hide();

    }
	// Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void Show()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Hide()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
