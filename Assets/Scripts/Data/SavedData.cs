using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class SavedData {
    public List<SavedLevelData> levelData = new List<SavedLevelData>();
    public int totalGems = 0;
    public int totalEnemiesKilled = 0;
    public int totalChests = 0;
    public int shieldDuration = 0;
    public int bombsNumber = 0;
    public int weaponIndex = -1;
    public int objectToUnlockIndex = 0;
    public int animalSelectedIndex = 0;
    public int animalsUnlockedCount= 1;
}
