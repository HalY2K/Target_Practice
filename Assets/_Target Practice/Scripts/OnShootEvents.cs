using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnShootEvents : MonoBehaviour
{

    public UnityEvent OnShoot;
    [SerializeField] private bool shouldDestroyProjectile = true;

    
    private void Start() {
        GameObject signs = GameObject.Find("Signs");
        if(signs) {
            Debug.Log("signs is not null");
        }
    }
    private void OnTriggerEnter(Collider other) {
        //if try get component projectile
        Projectile projectile = other.GetComponent<Projectile>();
        if(projectile) {
            if(shouldDestroyProjectile) {
                // Destroy(projectile.gameObject);
                projectile.gameObject.SetActive(false);
            }
            OnShoot.Invoke();
        }
    }

    //method to set shouldDestroyProjectile to false when the A button is pressed
    private void Update() {
        if(Input.GetKeyDown(KeyCode.G)) {
            shouldDestroyProjectile = false;
            if(!shouldDestroyProjectile) {
                Debug.Log("shouldDestroyProjectile is false");

                OnShoot.Invoke();
            }

        }
    }
    

    private void OnCollisionEnter(Collision other) {
        //if try get component projectile
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile) {
            if(shouldDestroyProjectile) {
                //set signs to active false
                GameObject signs = GameObject.Find("Signs");//Crim, why do I need this if I have this in Start?
                signs.SetActive(false);
            }
            OnShoot.Invoke();
        }
    }
}
