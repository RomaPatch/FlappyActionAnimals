using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowKeysController : MonoBehaviour,IPointerExitHandler
{
    public enum ArrowKeys
    {
        Up,Down,Left,Right
    }

    private Dictionary<string, ArrowKeys> arrowDictionary = new Dictionary<string, ArrowKeys>();

    private Button[] buttons;

    public delegate void OnArrowKeyPressed(ArrowKeys arrowKey);
    public static event OnArrowKeyPressed OnKeyPressed;

    public delegate void OnArrowKeyUnpressed(ArrowKeys arrowKey);
    public static event OnArrowKeyUnpressed OnKeyUnpressed;

    // Use this for initialization
    void Start ()
	{
	    buttons = transform.GetComponentsInChildren<Button>();
        SetAlphaButtons();

        arrowDictionary.Add("DownArrow", ArrowKeys.Down);
        arrowDictionary.Add("UpArrow",ArrowKeys.Up);
        arrowDictionary.Add("LeftArrow",ArrowKeys.Left);
        arrowDictionary.Add("RightArrow",ArrowKeys.Right);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlphaButtons();
    }

    private void SetAlphaButtons()
    {
        foreach (var button in buttons)
        {
            button.targetGraphic.CrossFadeAlpha(0.7f, 0, false);
        }
    }

    public void OnArrowKeyPointerDown(Button button)
    {
        OnKeyPressed(arrowDictionary[button.name]);
    }

    public void OnArrowKeyPointerUp(Button button)
    {
        SetAlphaButtons();
        OnKeyUnpressed(arrowDictionary[button.name]);
    }
}
