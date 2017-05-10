using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchAreaController : MonoBehaviour,IPointerDownHandler
{
    public delegate void OnPointerDownEvent();
    public static event OnPointerDownEvent OnTouchedScreen;

    protected TouchAreaController() { }

    // Use this for initialization
    void Start()
    {
        transform.GetComponent<Image>().CrossFadeAlpha(0, 0,true);
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.IsPlaying)
        {
         if (GameManager.instance.isPaused){return;}
            OnTouchedScreen();
        }
        else
        {
            GameObject.Find("LevelCanvas").transform.GetChild(0).gameObject.SetActive(true);
            //transform.parent.FindChild("Levels").gameObject.SetActive(true);
        }
    }
}
