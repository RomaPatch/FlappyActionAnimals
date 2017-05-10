using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BackToHomeFromSelectScreen :MonoBehaviour,IPointerUpHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
