using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDGameIconsController : MonoBehaviour
{
    // Use this for initialization


    private const float alphaGroup = 0.3f; 

    private float opacity = 1 / alphaGroup;
    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init()
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().alpha = alphaGroup;
     }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowEnemiesIcon()
    {
        this.transform.Find("EnemiesIcon").GetComponent<Image>().CrossFadeAlpha(opacity, 0, true);
    }

    public void ShowGemIcon()
    {
        this.transform.Find("GemIcon").GetComponent<Image>().CrossFadeAlpha(opacity, 0, true);
    }

    public void ShowChestIcon()
    {
        this.transform.Find("ChestIcon").GetComponent<Image>().CrossFadeAlpha(opacity, 0, true);
    }
}
