using System;
using UnityEngine;
using System.Collections;

public class FlyBehaviour :MonoBehaviour {

    public float forceJump;
    public float velocityToChangeFallAngle;
    public float speedFall;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private bool flyPressed;
    private bool reverse;

    private bool flyEnabled = true;

    private Vector2 directionFlight = Vector2.up;
    private float angelFlight = 45;
    private float angleTarget;
    private float direction = 1;

    void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
    }
    // Use this for initializationlocal
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!flyEnabled){return;}

        if (Input.GetKeyDown(KeyCode.Space) /*|| Input.touchCount > 0*/)
        {
            flyPressed = true;
        }

        if (IsFlying() ) { return; }

        animator.SetBool("fly", false);
        angleTarget = GetFallAngle();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angleTarget), Time.deltaTime * speedFall);
    }

    void FixedUpdate()
    {
        if (flyPressed)
        {
            flyPressed = false;
            Fly();
        }
    }

    public void ScreenTouched()
    {
        if (!flyEnabled){return;}
        flyPressed = true;
    }

    public void Disable()
    {
        flyEnabled = false;
        rigidBody.gravityScale = 0;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Enable()
    {
        flyEnabled = true;
    }

    private void Fly()
    {
        animator.SetBool("fly", true);

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(directionFlight * forceJump);
        transform.rotation = Quaternion.Euler(0, 0, angelFlight * direction);
        
    }

    private float GetFallAngle()
    {
        if (IsFallingFast()) { return -angelFlight * transform.localScale.y; }

        return 0;
    }

    private bool IsFlying()
    {
        if (rigidBody.velocity.y * transform.localScale.y < 0) { return false; }

        return true;
    }

    private bool IsFallingFast()
    {
        if (rigidBody.velocity.y * transform.localScale.y <= velocityToChangeFallAngle) { return true; }

        return false;
    }

    private void ReverseFlight()
    {
        direction = -1;
        var flipDirection = -1;
        if (reverse) flipDirection = 1;

        reverse = true;
        directionFlight = Vector2.down;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * flipDirection);

        rigidBody.gravityScale = -1;
        rigidBody.velocity = Vector2.zero;
        Fly();
    }

    private void NormalFlight()
    {
        direction = 1;

        var flipDirection = 1;
        if (reverse) flipDirection = -1;
        reverse = false;
        directionFlight = Vector2.up;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y*flipDirection);

        rigidBody.gravityScale = 1;
        rigidBody.velocity = Vector2.zero;
        Fly();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ReverseFlight" && other.tag!="NormalFlight") {return;}
        if (other.tag == "ReverseFlight" && reverse  && flyEnabled) {return;}
        if (other.tag == "NormalFlight"  && !reverse && flyEnabled) {return;}

        flyEnabled = true;

        if (other.tag == "ReverseFlight")
        {
            ReverseFlight();
        }

        if (other.tag == "NormalFlight")
        {
           NormalFlight();
        }
    }
}
