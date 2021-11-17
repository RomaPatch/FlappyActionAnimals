using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class SpaceShipBehaviour : MonoBehaviour {

    public float speedSpaceShip;
    public GameObject arrowKeys;
    public GameObject shieldButton;

    public ObjectPool objectPool;

    public float keySpeed;

    private Rigidbody2D rigidBody;
    private Animator animator;
    
    private float moveX;
    private float moveY;

    private int buttonPressedX;
    private int buttonPressedY;

    private Transform spawnBullets;
    private bool spaceShipEnabled;

    private Weapon weapon;

    
    private Dictionary<ArrowKeysController.ArrowKeys,int> axisDic = new Dictionary<ArrowKeysController.ArrowKeys, int>();  
    private Dictionary<ArrowKeysController.ArrowKeys,bool> arrowIsPressedDic = new Dictionary<ArrowKeysController.ArrowKeys, bool>();  

    // Use this for initialization
    void Awake ()
    {

        weapon = FactoryWeapon.GetWeaponByName(FactoryWeapon.WEAPON.NORMAL);

        ArrowKeysController.OnKeyPressed += OnKeyPressed;
        ArrowKeysController.OnKeyUnpressed += OnKeyUnpressed;

        rigidBody = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        spawnBullets = transform.Find("spawnBullets");

        axisDic.Add(ArrowKeysController.ArrowKeys.Up, 1);
        axisDic.Add(ArrowKeysController.ArrowKeys.Down, -1);
        axisDic.Add(ArrowKeysController.ArrowKeys.Left, -1);
        axisDic.Add(ArrowKeysController.ArrowKeys.Right, 1);

        arrowIsPressedDic.Add(ArrowKeysController.ArrowKeys.Up, false);
        arrowIsPressedDic.Add(ArrowKeysController.ArrowKeys.Down, false);
        arrowIsPressedDic.Add(ArrowKeysController.ArrowKeys.Left, false);
        arrowIsPressedDic.Add(ArrowKeysController.ArrowKeys.Right, false);

        arrowKeys.SetActive(false);
    }

    void OnKeyPressed(ArrowKeysController.ArrowKeys arrowKey)
    {
        arrowIsPressedDic[arrowKey] = true;
        if (arrowIsPressedDic[ArrowKeysController.ArrowKeys.Up] || arrowIsPressedDic[ArrowKeysController.ArrowKeys.Down])
        {
            buttonPressedY = axisDic[arrowKey];
        }
        if (arrowIsPressedDic[ArrowKeysController.ArrowKeys.Left] || arrowIsPressedDic[ArrowKeysController.ArrowKeys.Right])
        {
            buttonPressedX = axisDic[arrowKey];
        }
    }

    void OnKeyUnpressed(ArrowKeysController.ArrowKeys arrowKey)
    {
        arrowIsPressedDic[arrowKey] = false;

        if (arrowKey == ArrowKeysController.ArrowKeys.Up || arrowKey == ArrowKeysController.ArrowKeys.Down)
        {
            UpdateDirectionValues(ArrowKeysController.ArrowKeys.Down, ArrowKeysController.ArrowKeys.Up, ref buttonPressedY,ref moveY);
        }

        if (arrowKey == ArrowKeysController.ArrowKeys.Left || arrowKey == ArrowKeysController.ArrowKeys.Right)
        {
            UpdateDirectionValues(ArrowKeysController.ArrowKeys.Left, ArrowKeysController.ArrowKeys.Right, ref buttonPressedX, ref moveX);
        }
    }

    private void UpdateDirectionValues(ArrowKeysController.ArrowKeys arrowKey1,ArrowKeysController.ArrowKeys arrowKey2, ref int buttonPressedRef, ref float moveDirRef )
    {
        if (!arrowIsPressedDic[arrowKey1] && !arrowIsPressedDic[arrowKey2])
            {
                buttonPressedRef = 0;
                moveDirRef = 0;
            }

            if (arrowIsPressedDic[arrowKey1] && !arrowIsPressedDic[arrowKey2])
            {
                buttonPressedRef = axisDic[arrowKey1];
                if (moveDirRef > 0) moveDirRef = 0;
            }

            if (arrowIsPressedDic[arrowKey2] && !arrowIsPressedDic[arrowKey1])
            {
                buttonPressedRef  = axisDic[arrowKey2];
                if (moveDirRef < 0) moveDirRef = 0;
            }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spaceShipEnabled) { return; }
        
        rigidBody.velocity = new Vector2(moveX * speedSpaceShip, moveY * speedSpaceShip);
    }

    // Update is called once per frame
    void Update () {

        if (!spaceShipEnabled) { return; }

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            ManageDesktopInputs();
        }
        else
        {
            UpdateAxisOnMobile();
        }
    }

    private void ManageDesktopInputs()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Shoot();
        }
    }

    private void UpdateAxisOnMobile()
    {
        if (buttonPressedX != 0)
        {
            moveX = Mathf.Lerp(moveX, buttonPressedX, Time.deltaTime * keySpeed);
        }

        if (buttonPressedY != 0)
        {
            moveY = Mathf.Lerp(moveY, buttonPressedY, Time.deltaTime * keySpeed);
        }
    }

    public void ScreenTouched()
    {
        if (!spaceShipEnabled) { return; }
        weapon.Shoot();
    }

    public void Disable()
    {
        spaceShipEnabled = false;
        arrowKeys.SetActive(false);
        shieldButton.SetActive(false);
        resetAxis();
        transform.Find("shield").GetComponent<ShieldBehaviour>().StopShield();
    }

    private void resetAxis()
    {
        buttonPressedX = 0;
        buttonPressedY = 0;
        moveX = 0;
        moveY = 0;
    }

    public void Enable()
    {
        spaceShipEnabled = true;
        arrowKeys.SetActive(true);
        if (GameManager.instance.GetShieldDuration() > 0)
        {
            shieldButton.SetActive(true);
        }

        weapon = FactoryWeapon.GetWeaponByIndex(GameManager.instance.GetWeaponIndex());
    }

    public bool isEnabled()
    {
        return spaceShipEnabled;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "BecameSpaceShip") { return; }
        if (spaceShipEnabled) { return; }
        ConvertToSpaceShip();
    }

    private void ConvertToSpaceShip()
    {
        rigidBody.gravityScale = 0;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Enable();
    }

    public void ShieldButtonClicked()
    {
        transform.Find("shield").gameObject.SetActive(true);
        shieldButton.SetActive(false);
    }

    private void OnDisable()
    {
        ArrowKeysController.OnKeyPressed -= OnKeyPressed;
        ArrowKeysController.OnKeyUnpressed -= OnKeyUnpressed;
    }


}
