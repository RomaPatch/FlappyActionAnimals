using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    
    public float cameraSpeed;
    public GameObject player;

    private Vector3 offset;


    // Use this for initialization
    void Start()
    {

        offset = transform.position - player.transform.position;

    }

    // Update is called once per frames
    void Update()
    {
        

        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
        offset = transform.position - player.transform.position;

    }
    

}
