using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AnimalsButton : MonoBehaviour,IPointerUpHandler
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerUp(PointerEventData eventData)
    {
       GameObject.Find("AnimalsCanvas").transform.GetChild(0).gameObject.SetActive(true);
    }
}
