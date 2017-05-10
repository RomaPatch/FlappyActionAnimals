using UnityEngine;
using System.Collections;

public class MarginController : MonoBehaviour
{

    private float offsetMargin = 0;
    private float topBorder;
    private float bottomBorder;
    private float leftBorder;
    private float rightBorder;

    private float screenWidth;
    private float screenHeight;

    private Vector3 cameraSizePosition;

    private Vector2 objectSize;

    private float distance;

    private bool borderTouched;
    private bool controlMarginActivated;

    void Awake()
    {

        objectSize = transform.GetComponent<SpriteRenderer>().bounds.size;

        distance = (transform.position - Camera.main.transform.position).z;

        topBorder    = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - objectSize.y/2;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + objectSize.y/2;
        }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

        //horizontal borders changes because camera moves.
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + objectSize.x / 2;
	    rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - objectSize.x/2;

	    if (controlMarginActivated)
	    {
	        ControlMargin();
	    }
	    else
	    {
            CheckBourderTouch();
        }
    }

    private void CheckBourderTouch()
    {
        borderTouched = false;
        if (this.transform.position.y > topBorder || this.transform.position.y < bottomBorder)
        {
            borderTouched = true;
        }

        if (this.transform.position.x > rightBorder || this.transform.position.x < leftBorder)
        {
            borderTouched = true;
        }
    }

    private void ControlMargin()
    {

        if (this.transform.position.y + offsetMargin > topBorder)
        {
            transform.position = new Vector3(transform.position.x, topBorder - offsetMargin, transform.position.z);
        }

        if (this.transform.position.y - offsetMargin < bottomBorder)
        {
            transform.position = new Vector3(transform.position.x, bottomBorder + offsetMargin, transform.position.z);
        }

        if (this.transform.position.x - offsetMargin < leftBorder)
        {
            transform.position = new Vector3(leftBorder + offsetMargin, transform.position.y, transform.position.z);
        }

        if (this.transform.position.x + offsetMargin > rightBorder)
        {
            transform.position = new Vector3(rightBorder - offsetMargin, transform.position.y, transform.position.z);
        }

    }

    public bool BorderTouched
    {
        get { return borderTouched; }
    }

    public bool ControlMarginActivated
    {
        get { return controlMarginActivated; }
        set { controlMarginActivated = value; }
    }

    public void SetOffsetMargin(float offset)
    {
        offsetMargin = offset;
    }
}
