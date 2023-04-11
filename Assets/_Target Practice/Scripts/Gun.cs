using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Gun : MonoBehaviour
{
    [SerializeField,FoldoutGroup("Dependencies")] Transform projectileSpawnPoint;
    [SerializeField,FoldoutGroup("Dependencies")] GameObject projectilePrefab;
    [SerializeField] AudioSource shootSound;
    [SerializeField] ParticleSystem muzzleFlash;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(projectileSpawnPoint.position, projectileSpawnPoint.forward);
        Gizmos.DrawSphere(projectileSpawnPoint.position, 0.1f);
    }

    [Button]
    public void Shoot()
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        shootSound.Play();
        //smoke.Play();
        muzzleFlash.Play();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
}
