using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // [SerializeField] Bounds spawnableArea;

    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject destroyEffectPrefab;
    [SerializeField] LayerMask environmentLayer;
    [SerializeField] float rotationForce = 2f;
    [SerializeField] float moveForce = 10f;
    [SerializeField] float moveAwayForce = 5f;
    [SerializeField] AudioSource hitSound;

    //referece to the ScoreManager script
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameManager gameManager;

    private int hitCounter = 0;
    


    // [SerializeField] float resetTime = 3f;
    // private bool isResetting = false;
    // private float timer = 0f;

    private void OnValidate() {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
       scoreManager = FindObjectOfType<ScoreManager>();
       gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Update()
    {
        DestroyTarget();
    }

   //respawn after 3 seconds when projectile does on collision enter
   //and use try get componant to get the projectile script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            Destroy(collision.gameObject);

            //Add an impulse force to the target when hit upwards
            rb.AddForce(Vector3.up * moveForce, ForceMode.Impulse);

            // Add force to the target when it is hit from the hitpoint
            // Vector3 dir = collision.contacts[0].point - transform.position;
            // dir = -dir.normalized;
            Vector3 dir = projectile.transform.forward;
            rb.AddForce(dir * moveAwayForce, ForceMode.Impulse);

            // Add a random rotation to the target when it is hit
            Vector3 rot = Random.insideUnitSphere * rotationForce;
            rb.AddTorque(rot, ForceMode.Impulse);

            //add 1 point to the int score when the target is hit
            // AddPoint();

            //play the hit sound
            hitSound.Play();

            MultiHit();
        }

        //destroy the target when it hits the environment
        if(((1<<collision.gameObject.layer) & environmentLayer) != 0)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.takeDamage(1);
        }

    }

    //Destroy the target if y <=-10
    private void DestroyTarget()
    {
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
    
    //count how many times the target is hit. If it is hit 3 times, destroy the target. Each time the taget is hit increment the hit counter by 1
    private void MultiHit()
    {
        hitCounter++;
        Debug.Log(hitCounter);
        scoreManager.AddScore(hitCounter);
        if (hitCounter >= 3)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // void AddPoint()
    // {
    //     // scoreManager.AddScore(1);
    // }

}
