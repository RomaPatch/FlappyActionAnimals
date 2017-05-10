using UnityEngine;
using System.Collections;

public class ShieldBehaviour : MonoBehaviour
{
    private float duration;
    private BoxCollider2D collider;
    private float elapsedTime;
    private bool stopShield;

    void Awake()
    {
        collider = transform.parent.GetComponent<BoxCollider2D>();
        duration = GameManager.instance.GetShieldDuration();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnEnable()
    {
        stopShield = false;
        StartCoroutine(ShieldControl());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy"){return;}

        other.gameObject.SendMessage("Destroy");
    }

    IEnumerator ShieldControl()
    {
        float elapsedTime = 0;
        collider.enabled = false;

        while (elapsedTime < duration && !stopShield)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        collider.enabled = true;
        gameObject.SetActive(false);
    }

    public void StopShield()
    {
        stopShield = true;
    }

}
