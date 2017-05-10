using UnityEngine;
using System.Collections;

public class BeeEnemy : MonoBehaviour
{

    public float speed = 1;

    private Rigidbody2D rb;

    private bool isVisible;

    private bool isDead;


	// Use this for initialization
	void Start ()
	{
        //rb =  transform.GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        if (!isVisible){return;}
     
       transform.Translate(Vector2.left * (Time.deltaTime * speed*-1));
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag!="Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag!= "Shield"){return;}
        if (isDead){return;}
       
        Invoke("Dead",0.01f);
    }

    void Dead()
    {
        isDead = true;
        transform.GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool("Die", true);
        LevelManager.instance.EnemyKilled();
    }

    void ExplosionFinished()
    {
        Destroy(gameObject);
    }

    //Called by ShieldBheaviour
    void Destroy()
    {
        Invoke("Dead", 0);
    }
}
