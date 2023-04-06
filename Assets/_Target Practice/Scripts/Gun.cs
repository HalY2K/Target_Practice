using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Gun : MonoBehaviour
{
    [SerializeField,FoldoutGroup("Dependencies")] Transform projectileSpawnPoint;
    [SerializeField,FoldoutGroup("Dependencies")] GameObject projectilePrefab;
    
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
}
