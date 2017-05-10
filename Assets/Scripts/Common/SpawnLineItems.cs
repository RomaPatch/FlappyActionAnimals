using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnLineItems : MonoBehaviour
{
    public GameObject item;
    public float distance = 1;
    public LayerMask obstacle;

    private List<GameObject> items = new List<GameObject>();

    private Vector3 nextPosition;
    private float itemHeight;
    private int index;

    private float bottomItemPositionY;

    private Vector3 cameraSizePosition;
 
    // Use this for initialization
    void Start()
    {
        cameraSizePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        itemHeight = item.GetComponent<Renderer>().bounds.size.y;
     
        nextPosition = transform.position;

        AddItemsAndSetNextPosition();
    }

// Update is called once per frame
    void FixedUpdate ()
    {
       while ((bottomItemPositionY > -cameraSizePosition.y))
        {
            AddItemsAndSetNextPosition();
        }
       
    }

    void Update()
    {
      
    }

    private void AddItemsAndSetNextPosition()
    {
        if (WillCollide())
        {
            SetNextPosition();
            return;
        }
        items.Add((GameObject)Instantiate(item, nextPosition, Quaternion.identity));
        items[index].transform.parent = this.transform;
        index++;
        SetNextPosition();
    }

    private bool WillCollide()
    {
        bool willCollide = false;

       if (item.GetComponent<CircleCollider2D>() != null)
       {
            willCollide = Physics2D.OverlapCircle(nextPosition, item.GetComponent<CircleCollider2D>().radius, obstacle);

       }else if (item.GetComponent<BoxCollider2D>() != null)
       {
            willCollide = Physics2D.OverlapPoint(new Vector2(nextPosition.x,bottomItemPositionY), obstacle);
       }

       return willCollide;
   }

    private void SetNextPosition()
    {
        nextPosition = new Vector3(transform.position.x, nextPosition.y - itemHeight - distance, transform.position.z);
        bottomItemPositionY = nextPosition.y - (itemHeight / 2);
    }

}
