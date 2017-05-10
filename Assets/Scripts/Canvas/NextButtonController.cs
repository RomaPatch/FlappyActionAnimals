using UnityEngine;
using UnityEngine.EventSystems;

public class NextButtonController : MonoBehaviour,IPointerUpHandler {

	// Use this for initialization
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerUp(PointerEventData eventData)
    {
       //transform.parent.GetComponent<FinalScreenController>().StageCompleted();
       transform.parent.GetComponent<FinalScreenController>().ShowNextScreen();
    }
}
