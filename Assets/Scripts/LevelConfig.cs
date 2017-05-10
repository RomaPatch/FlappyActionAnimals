using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelConfig
{
    public List<Level> levels;
}

[Serializable]
public class Level
{
    public int id;
    public int enemies;
    public int gems;
    public bool locked;
    public int gemsToUnlock;
}




