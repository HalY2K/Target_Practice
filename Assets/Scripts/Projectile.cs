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
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Target target = FindObjectOfType<Target>();
        // transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
