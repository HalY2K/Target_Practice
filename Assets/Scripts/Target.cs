using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] Bounds spawnableArea;

    [SerializeField] Rigidbody rb;
    [SerializeField] float rotationForce = 2f;
    [SerializeField] float moveForce = 10f;
    [SerializeField] float moveAwayForce = 5f;
    [SerializeField] float resetTime = 3f;
    private bool isResetting = false;
    private float timer = 0f;

    private void OnValidate() {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        }
    }
    //Create on draw gizmos selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnableArea.center, spawnableArea.size);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnableArea.center, 0.5f);
    }

    private void Update()
    {
        //if isresetting is true subtract time.deltatime from timer
        if (isResetting)
        {
            timer -= Time.deltaTime;
        }
        //if the timer is 0
        if (timer <= 0)
        {
            // Respawn();
            //set isresetting to false
            isResetting = false;
            //set timer to 0
            timer = resetTime;
            
        }
    }


   //respawn after 3 seconds when projectile does on collision enter
   //and use try get componant to get the projectile script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            Destroy(collision.gameObject);

            timer = resetTime;
            isResetting = true;

            // Restart the respawn countdown
            // StopCoroutine(RespawnAfterDelay());
            // StartCoroutine(RespawnAfterDelay());

            //Add an impulse force to the target when hit upwards
            rb.AddForce(Vector3.up * moveForce, ForceMode.Impulse);

            // Add force to the target when it is hit from the hitpoint
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * moveAwayForce, ForceMode.Impulse);

            // Add a random rotation to the target when it is hit
            Vector3 rot = Random.insideUnitSphere * rotationForce;
            rb.AddTorque(rot, ForceMode.Impulse);

            rb.useGravity = true;
        }
    }


    

    //Create a coroutine to respawn after 3 seconds
    // private IEnumerator RespawnAfterDelay()
    // {
    //     yield return new WaitForSeconds(resetTime);
    //     Respawn();
    // }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnableArea.min.x, spawnableArea.max.x), Random.Range(spawnableArea.min.y, spawnableArea.max.y), Random.Range(spawnableArea.min.z, spawnableArea.max.z));
        return randomPosition;
    }

    //Create a respawn function
    private void Respawn()
    {
        rb.useGravity = false;
        transform.position = GetRandomPosition();
        //zero out the velocity of the target
        rb.velocity = Vector3.zero;

        //zero out the angular velocity of the target
        rb.angularVelocity = Vector3.zero;

        //reset the rotation of the target
        transform.rotation = Quaternion.identity;
    }
    
   

}
