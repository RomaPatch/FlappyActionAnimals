using UnityEngine;
using System.Collections;

public class ChestVisibility : MonoBehaviour
{


    private Transform chest;
    private bool isVisible;
 
    void Awake()
    {
        chest = transform.GetChild(0);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
        if (!isVisible || chest == null || chest.gameObject.activeInHierarchy){ return;}
        if (LevelManager.instance.keyPickedUp)
        {
            chest.gameObject.SetActive(true);
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

}
