using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float spaceShipOffsetMargin = 0;

    public List<AnimatorOverrideController> animatorsOverrideControllers; 


    public bool stageFinished;

    public delegate void OnStageFinished();
    public static event OnStageFinished OnFinishedStage;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private RuntimeAnimatorController originalAnimatorController;
    private BoxCollider2D collider;

    private Vector3 cameraPositionMiddle;
    
    private FlyBehaviour flyBehaviour;
    private SpaceShipBehaviour spaceShipBehaviour;
    private MarginController marginController;

    private GameObject explosion;

    private bool isPlayerDeath;
    

    // Use this for initialization
    void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        flyBehaviour = GetComponent<FlyBehaviour>();
        spaceShipBehaviour = GetComponent<SpaceShipBehaviour>();
        marginController = GetComponent<MarginController>();

        collider = transform.GetComponent<BoxCollider2D>();
       
        cameraPositionMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        Physics2D.IgnoreLayerCollision(10,11);

        originalAnimatorController = animator.runtimeAnimatorController;

       // animalSelected = AnimalsData.animals.DUCK;

        selectPlayer(GameManager.instance.animalSelected);
    }

    public void selectPlayer(AnimalsData.animals animalToSelect)
    {
        String animalResourceName = (string)AnimalsData.animalsResourceName[animalToSelect];
        GameObject animal = (GameObject)Resources.Load(animalResourceName);

        transform.GetComponent<SpriteRenderer>().sprite = animal.GetComponent<SpriteRenderer>().sprite;

        if (animalToSelect == AnimalsData.animals.BIRD)
        {
            animator.runtimeAnimatorController = originalAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = animatorsOverrideControllers[((int) animalToSelect) - 1];
        }

        collider.size = (Vector2)AnimalsData.animalsColliderSize[animalToSelect];
    }

    void OnEnable()
    {
        TouchAreaController.OnTouchedScreen += ScreenTouched;
    }

    void OnDisable()
    {
        TouchAreaController.OnTouchedScreen -= ScreenTouched;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {
        if (!explosion && marginController.BorderTouched && !spaceShipBehaviour.isEnabled())
        {
            PlayerDies();
            return;
        }

        if (!explosion || explosion.GetComponent<Animation>().isPlaying){return;}

        Destroy(explosion);

        #if UNITY_ADS
            GameManager.instance.ShowAd();
        #endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ScreenTouched()
    {
        flyBehaviour.ScreenTouched();
        spaceShipBehaviour.ScreenTouched();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BecameSpaceShip")
        {
            Destroy(other.gameObject);
            DisableFly();
            EnableMarginController();
        }

        if (other.tag == "ReverseFlight" || other.tag =="NormalFlight")
        {
            Destroy(other.gameObject);
            DisableSpaceShip();
            DisableMarginController();
        }

        if (other.tag == "FinishStage")
        {
            stageFinished = true;
            DisableFly();
            OnFinishedStage();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "BecameSpaceShip"  || other.tag == "ReverseFlight" || other.tag == "NormalFlight"){
            Destroy(other.gameObject);
        }
        
    }

    private void EnableMarginController()
    {
        marginController.SetOffsetMargin(spaceShipOffsetMargin);
        marginController.ControlMarginActivated = true;
    }


    private void DisableMarginController()
    {
        marginController.ControlMarginActivated = false;
    }

    private void DisableFly()
    {
        animator.SetBool("fly", false);
       
        flyBehaviour.Disable();
    }

    private void DisableSpaceShip()
    {
        marginController.SetOffsetMargin(0);
        marginController.ControlMarginActivated = false;
        spaceShipBehaviour.Disable();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colllisionTag:" + collision.gameObject.tag);
        Debug.Log("colllisionTag:" + collision.gameObject.name);
    
        PlayerDies();
    }

    private void PlayerDies()
    {
        isPlayerDeath = true;
        GameManager.instance.playerDeathsNumber++; 
        DisableFly();
        DisableSpaceShip();
        animator.SetBool("die", true);

        collider.enabled = false;

        explosion = (GameObject)Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity);

        explosion.transform.parent = gameObject.transform.parent;
        var explosionRb = explosion.GetComponent<Rigidbody2D>();

        if (rigidBody.velocity.y > 0)
        {
            explosionRb.gravityScale *= -1;
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool IsPlayerDeath()
    {
        return isPlayerDeath;
    }

}
