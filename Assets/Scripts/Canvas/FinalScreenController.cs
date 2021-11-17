using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreenController : MonoBehaviour
{

    private const String ENEMIES_REWARD_SCREEN = "ShowEnemiesKilledReward";
    private const String CHEST_REWARD_SCREEN = "ShowChestReward";
    private const String LEVEL_UNLOCKED_SCREEN = "ShowLevelUnlocked";
    private const String STAGE_COMPLETED_SCREEN = "StageCompleted";

    private Transform title;
    private Transform icons;
    private Transform nextButton;

    private Transform enemiesKilledReward;

    private String nextScreenMethod;
    private String currentScreen;
    private int levelUnlocked;

    void Awake()
    {
        gameObject.SetActive(false);
        transform.Find("BackGround").GetComponent<Image>().CrossFadeAlpha(0, 0, true);

        title = transform.Find("Title");
        icons = transform.Find("HUD");
        nextButton = transform.Find("NextButton");
        nextButton.gameObject.SetActive(false);

        title.GetComponent<Text>().CrossFadeAlpha(0, 0, true);
        icons.Find("EnemiesIcon").GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        icons.Find("EnemiesIcon").GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 0, true);
        icons.Find("GemIcon").GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        icons.Find("GemIcon").GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 0, true);
        icons.Find("ChestIcon").GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        nextButton.GetComponent<Image>().CrossFadeAlpha(0, 0, true);

        enemiesKilledReward = transform.Find("EnemiesKilledReward");
        enemiesKilledReward.Find("Object").GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        enemiesKilledReward.Find("Text").GetComponent<Text>().CrossFadeAlpha(0, 0, true);
        enemiesKilledReward.Find("Button").GetComponent<Image>().CrossFadeAlpha(0,0,true);

        nextScreenMethod = ENEMIES_REWARD_SCREEN;

    }
 
    // Use this for initialization
    void Start()
    {
        
        StartCoroutine(AnimeFinalScreen());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonPointerUp()
    {
        
    }

    IEnumerator AnimeFinalScreen()
    {
        yield return new WaitForSeconds(3.0f);
        transform.Find("BackGround").GetComponent<Image>().CrossFadeAlpha(1, 0, true);
        SetTitleText();
        SetEnemiesKilledText();
        SetGemsPickedUpText();
        title.GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        icons.Find("EnemiesIcon").GetComponent<Image>().CrossFadeAlpha(GetEnemiesIconAlpha(), 1, true);
        yield return new WaitForSeconds(1);
        icons.Find("EnemiesIcon").GetChild(0).GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        icons.Find("GemIcon").GetComponent<Image>().CrossFadeAlpha(GetGemIconAlpha(), 1, true);
        yield return new WaitForSeconds(1);
        icons.Find("GemIcon").GetChild(0).GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        icons.Find("ChestIcon").GetComponent<Image>().CrossFadeAlpha(GetChestIconAlpha(), 1, true);
        yield return new WaitForSeconds(1);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
    }

    private void SetTitleText()
    {
        title.GetComponent<Text>().text = "STAGE " + GameManager.instance.currentLevel + " COMPLETED!";
    }

    private void SetEnemiesKilledText()
    {
        var enemiesKilled = LevelManager.instance.enemiesKilled;
        var enemiesOnLevel = LevelManager.instance.enemiesOnLevel;

        icons.Find("EnemiesIcon").GetChild(0).GetComponent<Text>().text = enemiesKilled + " / " + enemiesOnLevel;
    }

    private void SetGemsPickedUpText()
    {
        var gemsPickedUp = LevelManager.instance.gemsPickedUp;
        var gemsOnLevel = LevelManager.instance.gemsOnLevel;
        icons.Find("GemIcon").GetChild(0).GetComponent<Text>().text = gemsPickedUp + " / " + gemsOnLevel;
    }

    private float GetEnemiesIconAlpha()
    {
        float alpha = 0.5f;
        if (!LevelManager.instance.allEnemiesKilled)
        {
            return alpha;
        }

        alpha = 1;
        return alpha;
    }

    private float GetGemIconAlpha()
    {
        float alpha = 0.5f;
        if (!LevelManager.instance.allGemsPickedUp)
        {
            return alpha;
        }

        alpha = 1;
        return alpha;
    }

    private float GetChestIconAlpha()
    {
        float alpha = 0.5f;
        if (!LevelManager.instance.chestPickedUp)
        {
            return alpha;
        }

        alpha = 1;
        return alpha;
    }

    public void ShowNextScreen()
    {
        nextButton.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        nextButton.gameObject.SetActive(false);

        enemiesKilledReward.Find("Button").GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        enemiesKilledReward.Find("Button").gameObject.SetActive(false);
        Invoke(nextScreenMethod,0);
    }

    public void ShowEnemiesKilledReward()
    {
        nextScreenMethod = CHEST_REWARD_SCREEN;
        currentScreen = ENEMIES_REWARD_SCREEN;

        if (!GameManager.instance.isObjectUnlocked())
        {
            Invoke(nextScreenMethod,0);
            return;
        }
        enemiesKilledReward.gameObject.SetActive(true);
        icons.gameObject.SetActive(false);

        StartCoroutine(AnimeEnemiesKilledReward());
    }

    IEnumerator AnimeEnemiesKilledReward()
    {
        ObjectToUnlock objectToUnlock = GameManager.instance.objectToUnlock;
        title.GetComponent<Text>().text = "ALL ENEMIES KILLED!";

        SetObjectUnlockText(objectToUnlock);
        SetObjectUnlockImage(objectToUnlock);

        enemiesKilledReward.Find("Object").GetComponent<Image>().CrossFadeAlpha(1, 2, true);
        enemiesKilledReward.Find("Text").GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(3.0f);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
        yield return null;
    }

    private void SetObjectUnlockText(ObjectToUnlock objectToUnlock)
    {
        if (objectToUnlock.type == "Shield")
        {
            var shieldTitle = "Shield Unlocked! \n";
            var shieldText = "Use it once in each try for " + objectToUnlock.attribute + " seconds";
            var isShieldUnlocked = objectToUnlock.attribute == 5;

            if (!isShieldUnlocked){shieldTitle = "Shield Upgrade! \n";}

            enemiesKilledReward.Find("Text").GetComponent<Text>().text = shieldTitle + shieldText;
        }

        if (objectToUnlock.type == "Weapon")
        {
            var shieldTitle = "Weapon Unlocked! \n";
            var shieldText = "Kill enemies and destroy objects faster. Try it!";
            
            enemiesKilledReward.Find("Text").GetComponent<Text>().text = shieldTitle + shieldText;
        }
    }

    private void SetObjectUnlockImage(ObjectToUnlock objectToUnlock)
    {
        if (objectToUnlock.type == "Shield")
        {
            GameObject shieldSprite = (GameObject)Resources.Load("greenShieldSprite");
            enemiesKilledReward.Find("Object").GetComponent<Image>().overrideSprite = shieldSprite.GetComponent<SpriteRenderer>().sprite;
            enemiesKilledReward.Find("Object").localScale = new Vector3(1, 1, 1);
        }

        if (objectToUnlock.type == "Weapon")
        {
            GameObject weaponSprite = (GameObject)Resources.Load("DoubleShootPrefab");
            enemiesKilledReward.Find("Object").GetComponent<Image>().overrideSprite = weaponSprite.GetComponent<SpriteRenderer>().sprite;
            enemiesKilledReward.Find("Object").localScale = new Vector3(1,0.6f,1);
        }
    }

    public void ShowChestReward()
    {
        nextScreenMethod = LEVEL_UNLOCKED_SCREEN;
        currentScreen = CHEST_REWARD_SCREEN;

        if (!GameManager.instance.animalUnlocked)
        {           
            Invoke(nextScreenMethod, 0);
            return;
        }

        enemiesKilledReward.gameObject.SetActive(true);
        icons.gameObject.SetActive(false);

        StartCoroutine(AnimeChestReward());
    }

    IEnumerator AnimeChestReward()
    {
        title.GetComponent<Text>().text = "CHEST PICKED UP!";
     
        SetAnimalUnlockedImageAndText();

        enemiesKilledReward.Find("Object").GetComponent<Image>().CrossFadeAlpha(1, 2, true);
        enemiesKilledReward.Find("Text").GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1.0f);
        enemiesKilledReward.Find("Button").Find("Text").GetComponent<Text>().text = "SELECT!";
        enemiesKilledReward.Find("Button").gameObject.SetActive(true);
        enemiesKilledReward.Find("Button").GetComponent<Image>().CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(3.0f);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
        yield return null;
    }

    private void SetAnimalUnlockedImageAndText()
    {

        var animal = AnimalsData.GetAnimalByIndex(GameManager.instance.GetAnimalUnlockedIndex());
        String animalResourceName = (string)AnimalsData.animalsResourceName[AnimalsData.GetAnimalByIndex(GameManager.instance.GetAnimalUnlockedIndex())];
        GameObject animalResource = (GameObject)Resources.Load(animalResourceName);

        enemiesKilledReward.Find("Text").GetComponent<Text>().text = "ANIMAL UNLOCKED: " + animal +"!";

        enemiesKilledReward.Find("Object").GetComponent<Image>().overrideSprite = animalResource.GetComponent<SpriteRenderer>().sprite;
        
        enemiesKilledReward.Find("Object").localScale = new Vector3(1, 1, 1);
    }


    public void ShowLevelUnlocked()
    {
        nextScreenMethod = STAGE_COMPLETED_SCREEN;
        currentScreen = LEVEL_UNLOCKED_SCREEN;

        levelUnlocked = GameManager.instance.GetNextLevelUnlocked();

        if (levelUnlocked == 0)
        {
            Invoke(nextScreenMethod, 0);
            return;
        }

        
        icons.gameObject.SetActive(false);
        enemiesKilledReward.gameObject.SetActive(true);
        StartCoroutine(AnimeLeveLUnlocked(levelUnlocked));
    }

    IEnumerator AnimeLeveLUnlocked(int level)
    {
        ObjectToUnlock objectToUnlock = GameManager.instance.objectToUnlock;
        title.GetComponent<Text>().text = "LEVEL UNLOCKED!";

        enemiesKilledReward.Find("Text").GetComponent<Text>().text = "You have unlocked LEVEL " + level;

        var lockSprite = (GameObject) Resources.Load("unlocked");
        enemiesKilledReward.Find("Object").GetComponent<Image>().overrideSprite = lockSprite.GetComponent<SpriteRenderer>().sprite;
        enemiesKilledReward.Find("Object").localScale = new Vector3(1, 1, 1);

        enemiesKilledReward.Find("Object").GetComponent<Image>().CrossFadeAlpha(1, 2, true);
        enemiesKilledReward.Find("Text").GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1.0f);
        enemiesKilledReward.Find("Button").Find("Text").GetComponent<Text>().text = "PLAY NOW!";
        enemiesKilledReward.Find("Button").gameObject.SetActive(true);
        enemiesKilledReward.Find("Button").GetComponent<Image>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(3.0f);
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
        yield return null;
    }

    public void StageCompleted()
    {
        currentScreen = STAGE_COMPLETED_SCREEN;
        GameManager.instance.StageCompleted();
    }


    public void ActionButtonPointerUp()
    {
        enemiesKilledReward.Find("Button").gameObject.SetActive(false);
        if (currentScreen == LEVEL_UNLOCKED_SCREEN)
        {
            GameManager.instance.StartGame(levelUnlocked);
        }

        if (currentScreen == CHEST_REWARD_SCREEN)
        {
            GameManager.instance.SaveAnimalSelected(GameManager.instance.GetAnimalUnlockedIndex());
        }
    }
}
