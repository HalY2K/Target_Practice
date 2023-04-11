using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void OnValidate() {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        }
    }
    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        // Target target = FindObjectOfType<Target>();
        // transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //zDebug look for target
        // Target target = FindObjectOfType<Target>();  
        // if(target) transform.LookAt(target.transform);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //destroy the projectile after 8 seconds
    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

}
