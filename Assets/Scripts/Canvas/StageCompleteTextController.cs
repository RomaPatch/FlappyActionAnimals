using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageCompleteTextController : MonoBehaviour
{
    private Text stageCompleteText;
    void Awake()
    {
        Debug.Log("awake text");
        stageCompleteText = gameObject.GetComponent<Text>();
        stageCompleteText.raycastTarget = false;
        Hide();
        PlayerController.OnFinishedStage += Show;
     
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show()
    {
        stageCompleteText.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

    public void Hide()
    {
        stageCompleteText.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    private void OnDisable()
    {
        PlayerController.OnFinishedStage -= Show;
    }
}
