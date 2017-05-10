using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneTutorialController : MonoBehaviour
{

    private const string TUTORIAL_STEP_0 = "Red coin reverse flight and gravity." + "\n" +"\n" + "Be careful with obstacles and borders!";
    private const string TUTORIAL_STEP_1 = "Yellow coin returns you to normal flight.";
    private const string TUTORIAL_STEP_2 = "Pick Up gems to unblock next stages."+ "\n" +"\n" + "Each stage has 3.";
    private const string TUTORIAL_STEP_3 = "Blue coin transform you on a SpaceShip." + "\n" + "\n" +  "Move with arrows, shoot with touch!";
    private const string TUTORIAL_STEP_4 = "Kill all enemies to upgrade with special powers." + "\n" + "\n" + "You will need it....";
    private const string TUTORIAL_STEP_5 = "Some obstacles need more than one shoot." + "\n" + "\n" + "One of them hide a key to open the animals chest!";
    private const string TUTORIAL_STEP_6 = "Only If you have the key it will appear a chest near the finish flag." + "\n" + "\n" +"Get it!";
    private const string TUTORIAL_STEP_7 = "Green coin is a shield." + "\n" + "\n" + "You can pass through obstacles and bullets but only for a few seconds!";

    public Transform player;
    private GameObject tutorialInfo;
    private int actualStep;
    private Button okButton;

    private PlayerController playerController;

    private bool tutorialStepsCompleted;

    void Awake()
    {
        tutorialInfo = GameObject.FindGameObjectWithTag("TutorialInfo");

        tutorialInfo.SetActive(false);

        okButton = tutorialInfo.transform.Find("TutorialButton").GetComponent<Button>();
        okButton.onClick.AddListener(TutorialButtonClicked);

        playerController = player.GetComponent<PlayerController>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (tutorialStepsCompleted){return;}

        if (player==null ) { return; }

        if(playerController.IsPlayerDeath() || playerController.stageFinished) { return; }


	    if (player.position.x < this.transform.GetChild(actualStep).transform.position.x){return;}
	    
	    tutorialInfo.GetComponentInChildren<Text>().text = GetStepInfo();
        GameManager.instance.PauseGame();
	    tutorialInfo.SetActive(true);
	}

    private string GetStepInfo()
    {
        var stepInfo = "";
        try
        {
            var info = GetType().GetField("TUTORIAL_STEP_" + actualStep, BindingFlags.NonPublic | BindingFlags.Static);
            stepInfo = (string)info.GetValue(this);
        }
        catch (NullReferenceException e)
        {
            stepInfo = "";
        }

        return stepInfo;
    }

    private void TutorialButtonClicked()
    {
        GameManager.instance.ResumeGame();
        tutorialInfo.SetActive(false);
        actualStep++;

        if (actualStep >= this.transform.childCount)
        {
            tutorialStepsCompleted = true;
        }
    }

  }
