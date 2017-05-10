using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject HUDIcons;
    private HUDGameIconsController HUDGameIconsController;
    public static LevelManager instance;

    public bool wereAllEnemiesKilled;
    public bool wasChestPickedUp;

    public int gemsOnLevel;
    public int enemiesOnLevel;
    public int enemiesKilled;

    public int gemsPickedUp;
    public bool keyPickedUp;

    public bool allEnemiesKilled;

    public bool allGemsPickedUp;
    public bool chestPickedUp;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        HUDGameIconsController = HUDIcons.GetComponent<HUDGameIconsController>();
        gemsOnLevel = GameManager.instance.GetGemsOnLevel();
        enemiesOnLevel = GameManager.instance.GetEnemiesOnLevel();

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GemPickedUp()
    {
        gemsPickedUp++;
        if (gemsPickedUp != gemsOnLevel) { return; }
        allGemsPickedUp = true;
        HUDGameIconsController.ShowGemIcon();
    }
    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled != enemiesOnLevel) { return; }
        allEnemiesKilled = true;
        HUDGameIconsController.ShowEnemiesIcon();
    }

    public void KeyPickedUp()
    {
        keyPickedUp = true;
    }

    public void ChestPickedUp()
    {
        chestPickedUp = true;
        HUDGameIconsController.ShowChestIcon();
    }
}
