using UnityEngine;
using System.Collections;

public class WallDamage : MonoBehaviour
{
    public GameObject wallDamaged;
    public GameObject key;
    public bool hasKey;
    private bool isDamaged;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet"){return;}
        collision.gameObject.SetActive(false);
        if (isDamaged)
        {
            if (hasKey)
            {
                Instantiate(key, transform.position,Quaternion.identity);
            }

            Invoke("Destroy", 0.01f);

        }
        else
        {
            isDamaged = true;
            transform.GetComponent<SpriteRenderer>().sprite = wallDamaged.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
