using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelsIconsController levelsIconsController;
    public AnimalsIconsController animalsIconsController;

    [HideInInspector]
    public LevelConfig levelConfig;

    [HideInInspector]
    public ObjectsToUnlock objectsToUnlock;

    [HideInInspector]
    public int currentLevel;

    [HideInInspector]
    public int playerDeathsNumber;

    [HideInInspector]
    public ObjectToUnlock objectToUnlock;

    [HideInInspector]
    public bool animalUnlocked;

    // [HideInInspector]
    // public int animalsUnlockedCount = 1;

    [HideInInspector]
    public int animalSelectedIndex = 0;

    [HideInInspector]
    public SaveLoadGame saveLoadGame = new SaveLoadGame();

    [HideInInspector] public AnimalsData.animals animalSelected = AnimalsData.animals.BIRD;

    private bool stageCompleted;
    private int adsShowedNumber;
    private int totalGemsBefore;
    private bool allEnemiesKilledBefore;
    private bool chestPickedUpBefore;
    private List<bool> levelLockStateBefore; 


    public enum PausedType
    {
        Tutorial,
        User
    }

    private bool isPlaying;
    public bool isPaused { get; private set; }

    //Awake is always called before any Start functions
    void Awake()
    {

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        PlayerController.OnFinishedStage += FinishedStage;
        SaveLoadGame.OnSaveDataFinished += OnSaveDataFinished;

        saveLoadGame.LoadData();

        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadLevelConfig();
        LoadObjectsToUnlock();

        if (saveLoadGame.savedData != null)
        {
            animalSelectedIndex = saveLoadGame.savedData.animalSelectedIndex;
            //animalsUnlockedCount = saveLoadGame.savedData.animalsUnlockedCount;

            animalSelected = AnimalsData.GetAnimalByIndex(animalSelectedIndex);

        }

        GameObject.Find("AnimalsCanvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AnimalsCanvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    void FinishedStage()
    {
        if (isObjectUnlocked())
        {
            objectToUnlock = GetObjectToUnlock();
            saveLoadGame.ObjectUnlocked(objectToUnlock);
        }
       
        if (CanShowChestReward())
        {
            animalsIconsController.UnlockNextAnimal();
            saveLoadGame.animalsUnlockedCount++;
            animalUnlocked = true;
        }

        saveLoadGame.SaveData();
    }

    void OnSaveDataFinished()
    {
        GameObject.Find("HUDCanvas").transform.Find("FinalScreen").gameObject.SetActive(true);
        levelsIconsController.updateIconsLevels();
    }

    void Start()
    {
        #if UNITY_ADS
            Advertisement.Initialize("1345731", true);
         #endif

        //StartCoroutine(ShowAdWhenReady());
    }

    /* IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show();
      
    }*/

#if UNITY_ADS
    public void ShowAd()
    {
        if (!Advertisement.IsReady())
        {
            return;
        }

        Debug.Log("adsssss:" + playerDeathsNumber + ":" + adsShowedNumber);

        if (playerDeathsNumber == 1 || playerDeathsNumber == (3*adsShowedNumber) + 1)
        {
            Debug.Log("YEAHH AD:" + playerDeathsNumber + ":" + adsShowedNumber);
            adsShowedNumber++;
            Advertisement.Show();
        }

    }
#endif

    //Update is called every frame.
    void Update()
    {

    }

    public void StartGame(int level)
    {
        isPlaying = true;
        animalUnlocked = false;
        currentLevel = level;
        SceneManager.LoadScene("Level" + level);
  

        bool levelDataExists = saveLoadGame.savedData != null && saveLoadGame.savedData.levelData.Count >= currentLevel;
        allEnemiesKilledBefore = saveLoadGame.savedData != null && levelDataExists && saveLoadGame.savedData.levelData[currentLevel - 1].allEnemiesKilled;
        chestPickedUpBefore = saveLoadGame.savedData != null && levelDataExists && saveLoadGame.savedData.levelData[currentLevel - 1].chestPickedUp;
        levelLockStateBefore = levelsIconsController.GetLevelLockState();
        var pepe = 3;
    }

    public void BackToHome()
    {
        Time.timeScale = 1;
        isPlaying = false;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void StageCompleted()
    {
        stageCompleted = true;
        BackToHome();
    }

    public bool IsPlaying
    {
        get { return isPlaying; }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    private void LoadLevelConfig()
    {
        TextAsset asset = Resources.Load("LevelConfigData") as TextAsset;
        levelConfig = JsonUtility.FromJson<LevelConfig>(asset.text);
    }

    private void LoadObjectsToUnlock()
    {
        TextAsset asset = Resources.Load("ObjectsToUnlockData") as TextAsset;
        objectsToUnlock = JsonUtility.FromJson<ObjectsToUnlock>(asset.text);
    }

    public int GetGemsOnLevel()
    {
        return levelConfig.levels[currentLevel - 1].gems;
    }
    
    public int GetEnemiesOnLevel()
    {
        return levelConfig.levels[currentLevel - 1].enemies;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        SaveLoadGame.OnSaveDataFinished -= OnSaveDataFinished;
        PlayerController.OnFinishedStage -= FinishedStage;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && stageCompleted)
        {
            stageCompleted = false;
            GameObject.Find("LevelCanvas").transform.GetChild(0).gameObject.SetActive(true);
            return;
        }

        GameObject.Find("LevelCanvas").transform.GetChild(0).gameObject.SetActive(false);
    }
    
    public int GetShieldDuration()
    {
        if (saveLoadGame.savedData == null)
        {
            return 0;
        }
        return saveLoadGame.savedData.shieldDuration;
    }

    public int GetWeaponIndex()
    {
        return saveLoadGame.savedData == null ? 0: saveLoadGame.savedData.weaponIndex;
    }

    public bool isObjectUnlocked()
    {
        return !allEnemiesKilledBefore && LevelManager.instance.allEnemiesKilled;
    }

    private ObjectToUnlock GetObjectToUnlock()
    {
        var objectToUnlockIndex = 0;
        if (saveLoadGame.savedData != null) { objectToUnlockIndex = saveLoadGame.savedData.objectToUnlockIndex; }

        return objectsToUnlock.objectsToUnlock[objectToUnlockIndex];
    }

    private int GetAnimalIndexToUnlock()
    {
        var animalUnlockIndex = 0;
        if (saveLoadGame.savedData != null) { animalUnlockIndex = saveLoadGame.savedData.objectToUnlockIndex; }

        return animalUnlockIndex;
    }

    private bool CanShowChestReward()
    {
        return !chestPickedUpBefore && LevelManager.instance.chestPickedUp && !animalsIconsController.AllAnimalsUnlocked();
    }

    public int GetNextLevelUnlocked()
    {
        var levelUnlocked = 0;
        var levelLockState = levelsIconsController.GetLevelLockState();
        
        for (var i = 0; i < levelLockState.Count; i++)
        {
            if (currentLevel >= i + 1){continue;}

            if (levelLockStateBefore[i] && !levelLockState[i])
            {
                levelUnlocked = i + 1;
                break;
            }
        }

        return levelUnlocked;
    }

    public int GetAnimalUnlockedIndex()
    {
        return saveLoadGame.animalsUnlockedCount -1;
    }

    public void SaveAnimalSelected(int index)
    {
        if (index == animalSelectedIndex){return;}
        animalSelected = AnimalsData.GetAnimalByIndex(index);
        saveLoadGame.animalSelectedIndex = index;
        saveLoadGame.SaveAnimal();
    }

    public void SelectAnimal(int index)
    {
        animalsIconsController.SelectAnimal(index);
    }

}
