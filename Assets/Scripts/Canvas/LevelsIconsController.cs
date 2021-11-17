using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.UI;

public class LevelsIconsController : MonoBehaviour
{
    private Button[] levelsButtons;

    private SavedData savedData;

    private List<bool> levelLockState = new List<bool>();

    void Awake()
    {
        levelsButtons = transform.GetComponentsInChildren<Button>();
        ConfigureIconsLevels();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateIconsLevels()
    {
        ConfigureIconsLevels();
    }
    
    private void ConfigureIconsLevels()
    {
        for (var i=0;i< levelsButtons.Length;i++)
        {
            ConfigureIconLevel(i);
        }
    }

    private void ConfigureIconLevel(int iconIndex)
    {
        savedData = GameManager.instance.saveLoadGame.savedData;

        SavedLevelData savedLevelLevelData = CreateEmptyData();

        if (savedData != null && savedData.levelData.Count >= iconIndex + 1)
        {
            savedLevelLevelData = savedData.levelData[iconIndex];
        }

        levelsButtons[iconIndex].interactable = false;
        levelsButtons[iconIndex].transform.Find("Text").GetComponent<Text>().text = "Stage " + (iconIndex + 1);
        ActiveLockOrUnlock(GameManager.instance.levelConfig.levels[iconIndex], levelsButtons[iconIndex], savedData, iconIndex);
        ConfigureIconsTexts(GameManager.instance.levelConfig.levels[iconIndex], levelsButtons[iconIndex], savedLevelLevelData);
        ConfigureIconsVisibility(levelsButtons[iconIndex], savedLevelLevelData);
       }

    private SavedLevelData CreateEmptyData()
    {
        SavedLevelData savedLevelData  = new SavedLevelData();
        savedLevelData.enemiesKilled = 0;
        savedLevelData.gemsPickedUp = 0;
        savedLevelData.chestPickedUp = false;

        return savedLevelData;
    }

    private void ActiveLockOrUnlock(Level levelData,Button button, SavedData savedData, int iconIndex)
    {
        var lockGO = button.transform.Find("Locked").gameObject;
        var unlockGO = button.transform.Find("Unlocked").gameObject;

        button.interactable = false;
     
        if (levelLockState.Count < iconIndex + 1)
        {
            levelLockState.Add(false);
        }

        var levelIsUnlock = savedData != null && savedData.totalGems >= levelData.gemsToUnlock;
        
        if (levelData.locked && !levelIsUnlock)
        {
            lockGO.SetActive(true);
            unlockGO.SetActive(false);
            levelLockState[iconIndex] = true;
            return;
        }

        lockGO.SetActive(false);
        unlockGO.SetActive(true);
        button.interactable = true;
        levelLockState[iconIndex] = false;
    }

    private void ConfigureIconsTexts(Level levelData, Button button, SavedLevelData savedLevelLevelData)
    {
        var gemsToUnlockText = button.transform.Find("Locked").Find("GemsToUnlock").GetChild(0).GetComponent<Text>();
        var enemiesText = button.transform.Find("Unlocked").Find("HUD").Find("EnemiesIcon").GetChild(0).GetComponent<Text>();
        var gemText = button.transform.Find("Unlocked").Find("HUD").Find("GemIcon").GetChild(0).GetComponent<Text>();

        gemsToUnlockText.text = "x" + levelData.gemsToUnlock;

        enemiesText.text = savedLevelLevelData.enemiesKilled + " / " + levelData.enemies;
        gemText.text = savedLevelLevelData.gemsPickedUp + " / " + levelData.gems;
    }

    private void ConfigureIconsVisibility(Button button, SavedLevelData savedLevelLevelData)
    {
        var enemiesIconImage = button.transform.Find("Unlocked").Find("HUD").Find("EnemiesIcon").GetComponent<Image>();
        var gemsIconGOImage  = button.transform.Find("Unlocked").Find("HUD").Find("GemIcon").GetComponent<Image>();
        var chestIconGoImage = button.transform.Find("Unlocked").Find("HUD").Find("ChestIcon").GetComponent<Image>();

        var enemiesIconAlpha = 0.3f;
        var gemsIconAlpha = 0.3f;
        var chestIconAlpha = 0.3f;
        if (savedLevelLevelData.allEnemiesKilled)
        {
            enemiesIconAlpha = 1;
        }

        if (savedLevelLevelData.allGemsPickedUp)
        {
            gemsIconAlpha = 1;
        }

        if (savedLevelLevelData.chestPickedUp)
        {
            chestIconAlpha = 1;
        }
       
        enemiesIconImage.CrossFadeAlpha(enemiesIconAlpha,0,true);
        gemsIconGOImage.CrossFadeAlpha(gemsIconAlpha, 0,true);
        chestIconGoImage.CrossFadeAlpha(chestIconAlpha, 0,true);
    }

    public void LevelIconPointerUp(Button button)
    {
        if (!button.interactable){return;}
        List<Button> buttonList = levelsButtons.ToList();

        GameManager.instance.StartGame(buttonList.IndexOf(button)+ 1);
    }

    public List<bool> GetLevelLockState()
    {
        List<bool> result = new List<bool>();

        for (var i = 0; i < levelLockState.Count; i++)
        {
            result.Add(levelLockState[i]);
        }
          
        return result;
    }

    void OnEnable()
    {
        ConfigureIconsLevels();
    }
}
