using UnityEngine;
using System.Collections;


public class BackgroundMovement : MonoBehaviour
{

    // Use this for initialization
    private Vector2 size;


    void Awake()
    {
        size = transform.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void OnBecameInvisible()
    {
        transform.position = new Vector2(transform.position.x + (size.x*2), transform.position.y);
    }
}