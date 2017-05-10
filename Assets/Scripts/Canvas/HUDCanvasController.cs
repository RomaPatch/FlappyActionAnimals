using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDCanvasController : MonoBehaviour
{
    private HUDGameIconsController hudGameIconsController;
    void Awake()
    {
        hudGameIconsController = transform.FindChild("HUD").GetComponent<HUDGameIconsController>();
        //PlayerController.OnFinishedStage += ShowFinalScreen;
    }

	// Use this for initialization
	void Start ()
	{
	    Show();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show()
    {
        gameObject.SetActive(true);
        hudGameIconsController.Init();
    }

    private void ShowFinalScreen()
    {
        //gameObject.transform.FindChild("FinalScreen").gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //PlayerController.OnFinishedStage -= ShowFinalScreen;
    }
}
