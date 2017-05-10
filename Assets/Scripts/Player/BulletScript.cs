using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float diagonalSpeed;

    private Rigidbody2D rigidBody;

    private int diagonalDirection = 0;

    void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    void OnEnable()
    {
      rigidBody.velocity = new Vector2(Vector2.right.x * bulletSpeed, Vector2.up.y * diagonalSpeed * diagonalDirection);
    }

    void OnBecameInvisible()
    {
       diagonalDirection = 0;
       gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collision");
        diagonalDirection = 0;
        gameObject.SetActive(false);
    }

    public void SetDiagonalDirection(int direction)
    {
        diagonalDirection = direction;
    }
}
