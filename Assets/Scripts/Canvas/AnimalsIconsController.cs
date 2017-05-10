using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class AnimalsIconsController : MonoBehaviour
{

    private List<Button> animalsButtons = new List<Button>();

    private int selectedIndex = 0;

    void Awake()
    {

        for (var i = 0; i < transform.childCount; i++)
        {
            animalsButtons.Add(transform.GetChild(i).GetComponent<Button>());

            if (i > GameManager.instance.GetAnimalUnlockedIndex())
            {
                animalsButtons[i].interactable = false;
                animalsButtons[i].transform.FindChild("Locked").gameObject.SetActive(true);
                animalsButtons[i].transform.FindChild("Unlocked").gameObject.SetActive(false);
            }
        }

        SelectAnimal(GameManager.instance.animalSelectedIndex);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AnimalIconPointerUp(Button button)
    {
        if (!button.interactable) { return; }
        List<Button> buttonList = animalsButtons.ToList();

        int buttonIndex = buttonList.IndexOf(button);
        SelectAnimal(buttonIndex);
    }

    public void SelectAnimal(int index)
    {
        for (var i = 0; i < animalsButtons.Count; i++)
        {
            var animalButton = animalsButtons[i];

            animalButton.transform.FindChild("Unlocked").FindChild("Selected").gameObject.SetActive(false);
            animalButton.transform.FindChild("Unlocked").FindChild("Button").gameObject.SetActive(true);

            if (i == index)
            {
                animalButton.transform.FindChild("Unlocked").FindChild("Selected").gameObject.SetActive(true);
                animalButton.transform.FindChild("Unlocked").FindChild("Button").gameObject.SetActive(false);
                selectedIndex = i;

                GameManager.instance.SaveAnimalSelected(selectedIndex);
            }
        }
    }

    public void UnlockNextAnimal()
    {
        var animalToUnlock = animalsButtons[GameManager.instance.GetAnimalUnlockedIndex()+1];
        animalToUnlock.interactable = true;
        animalToUnlock.transform.FindChild("Locked").gameObject.SetActive(false);
        animalToUnlock.transform.FindChild("Unlocked").gameObject.SetActive(true);

    }

    public bool AllAnimalsUnlocked()
    {
        return GameManager.instance.GetAnimalUnlockedIndex() >= animalsButtons.Count-1;
    }

}
