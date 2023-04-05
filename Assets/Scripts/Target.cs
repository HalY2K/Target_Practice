using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] Bounds spawnableArea;

    [SerializeField] Rigidbody rb;

    [SerializeField] float rotationForce = 8f;
    [SerializeField] float moveForce = 10f;

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

   //respawn after 3 seconds when projectile does on collision enter
   //and use try get componant to get the projectile script
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            Destroy(collision.gameObject);
            StartCoroutine(RespawnAfterDelay());
            //add force to the target when it is hit from the hitpoint
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * moveForce, ForceMode.Impulse);

            //add a random rotation to the target when it is hit
            Vector3 rot = Random.insideUnitSphere * rotationForce;
            rb.AddTorque(rot, ForceMode.Impulse);

        }
    }

    

    //Create a coroutine to respawn after 3 seconds
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(3);
        Respawn();
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnableArea.min.x, spawnableArea.max.x), Random.Range(spawnableArea.min.y, spawnableArea.max.y), Random.Range(spawnableArea.min.z, spawnableArea.max.z));
        return randomPosition;
    }

    //Create a respawn function
    private void Respawn()
    {
        transform.position = GetRandomPosition();
        //zero out the velocity of the target
        rb.velocity = Vector3.zero;

        //zero out the angular velocity of the target
        rb.angularVelocity = Vector3.zero;

        //reset the rotation of the target
        transform.rotation = Quaternion.identity;
    }
    
}
