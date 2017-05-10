using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadGame  {

    [HideInInspector]
    public SavedData savedData;

    [HideInInspector]
    public int animalSelectedIndex;
    [HideInInspector]
    public int animalsUnlockedCount = 1;

    private int shieldDuration;
    private int bombsNumber;
    private int weaponIndex;
    private int objectToUnlockIndex;
   

    private const String FILENAME = "/FlappyData7.dat";

    public delegate void OnSavedData();
    public static event  OnSavedData OnSaveDataFinished;


    public void SaveAnimal()
    {
        if (savedData == null)
        {
            savedData = new SavedData();
        }

        savedData.animalSelectedIndex = animalSelectedIndex;
        savedData.animalsUnlockedCount = animalsUnlockedCount;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILENAME);

        bf.Serialize(file, savedData);
        file.Close();

    }

    public void SaveData()
    {
        if (savedData == null)
        {
            savedData = new SavedData();
        }

        SavedLevelData savedLevelData = GetOrAddSavedLevelData();
        UpdateItemLevelData(LevelManager.instance.enemiesKilled, ref savedLevelData.enemiesKilled,ref savedData.totalEnemiesKilled);
        UpdateItemLevelData(LevelManager.instance.gemsPickedUp, ref savedLevelData.gemsPickedUp, ref savedData.totalGems);

        //Increments Before Updates because sets the variable an the check would be different.
        IncrementItemData(LevelManager.instance.chestPickedUp, ref savedLevelData.chestPickedUp,ref savedData.totalChests);

        UpdateItemData(LevelManager.instance.allEnemiesKilled, ref savedLevelData.allEnemiesKilled);
        UpdateItemData(LevelManager.instance.allGemsPickedUp, ref savedLevelData.allGemsPickedUp);
        UpdateItemData(LevelManager.instance.chestPickedUp, ref savedLevelData.chestPickedUp);

        savedData.levelData[GameManager.instance.currentLevel - 1] = savedLevelData;

        savedData.shieldDuration = shieldDuration;
        savedData.bombsNumber = bombsNumber;
        savedData.weaponIndex = weaponIndex;
        savedData.objectToUnlockIndex = objectToUnlockIndex;
        savedData.animalSelectedIndex = animalSelectedIndex;
        savedData.animalsUnlockedCount = animalsUnlockedCount;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILENAME);

        bf.Serialize(file, savedData);
        file.Close();
        OnSaveDataFinished();
    }

    private void UpdateItemLevelData(int levelItemData, ref int savedItemData, ref int savedCommonData)
    {
        if (levelItemData <= savedItemData)
        {
            return;
        }

        savedCommonData += (levelItemData - savedItemData);
        savedItemData = levelItemData;
    }

    private void IncrementItemData(bool levelItemData, ref bool savedItemData, ref int savedItemToIncrement)
    {
        if (savedItemData || !levelItemData)
        {
            return;
        }

        savedItemToIncrement += 1;
    }

    private void UpdateItemData(bool levelItemData, ref bool savedItemData)
    {
        if (savedItemData || !levelItemData)
        {
            return;
        }

        savedItemData = true;
    }

    private SavedLevelData GetOrAddSavedLevelData()
    {

        var currentLevel = GameManager.instance.currentLevel;

        SavedLevelData savedLevelData;
        if (savedData.levelData.Count < currentLevel)
        {
            savedLevelData = new SavedLevelData();
            savedData.levelData.Add(savedLevelData);
        }
        else
        {
            savedLevelData = savedData.levelData[currentLevel - 1];
        }

        return savedLevelData;
    }

    public void ObjectUnlocked(ObjectToUnlock objectToUnlock)
    {
        objectToUnlockIndex++;
        if (objectToUnlock.type == "Shield"){shieldDuration = objectToUnlock.attribute;} 
        if (objectToUnlock.type == "Weapon"){weaponIndex = objectToUnlock.attribute;} 
        if (objectToUnlock.type == "Bomb")  {bombsNumber = objectToUnlock.attribute;} 
    }

    public void LoadData()
    {
        if (!File.Exists(Application.persistentDataPath + FILENAME))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + FILENAME, FileMode.Open);
        savedData = (SavedData)bf.Deserialize(file);
        file.Close();

        shieldDuration = savedData.shieldDuration;
        bombsNumber = savedData.bombsNumber;
        weaponIndex = savedData.weaponIndex;
        objectToUnlockIndex = savedData.objectToUnlockIndex;
        animalSelectedIndex = savedData.animalSelectedIndex;
        animalsUnlockedCount = savedData.animalsUnlockedCount;

        /* Debug.Log("totalChests:" + savedData.totalChests);
        Debug.Log("totalEnemiesKilled:" + savedData.totalEnemiesKilled);
        Debug.Log("totalGems:" + savedData.totalGems);

        for (var i=0;i<savedData.levelData.Count; i++)
        {
            Debug.Log("Level:" + (i + 1) + "\n");
            Debug.Log("enemiesKilled:"+savedData.levelData[i].enemiesKilled + "\n");
            Debug.Log("gemsPickedUp:"+savedData.levelData[i].gemsPickedUp + "\n");
            Debug.Log("chestPickedUp:"+savedData.levelData[i].chestPickedUp + "\n");
            Debug.Log("allEnemiesKilled:"+savedData.levelData[i].allEnemiesKilled + "\n");
            Debug.Log("allGemsPickedUp:"+savedData.levelData[i].allGemsPickedUp + "\n");
        }*/
    }
}
