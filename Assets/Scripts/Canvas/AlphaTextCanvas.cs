using UnityEngine;
using System.Collections;

public class AlphaTextCanvas : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private float damp;
    public float speed = 1;

    // Use this for initialization

    void Awake ()
	{
        canvasGroup = GetComponent<CanvasGroup>();
	}

    void Start()
    {
        StartCoroutine(FadeOut());
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.1f)
        {
          
            canvasGroup.alpha -= speed * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += speed * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut());
    }
}
