using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    //create a variable that is a game object and is equal to the target prefab
    [SerializeField] GameObject TargetPrefab; // assign the target prefab to this variable in the Inspector
    [SerializeField] Bounds spawnableArea;
    
    [SerializeField] GameObject[] TargetPrefabs;
    [SerializeField] GameManager gameManager;
    GameObject randomPrefab => TargetPrefabs[Random.Range(0, TargetPrefabs.Length)];

    public float spawnInterval = 5f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnableArea.center, spawnableArea.size);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnableArea.center, 0.5f);
    }

    // private void Start()
    // {
    //     gameManager = FindObjectOfType<GameManager>();
    // }

    void Update()
    {
       
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            Spawn();
            spawnInterval = 5f;
        }
        
    }
    
    Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
        return randomPosition;
    }

    
    void Spawn()
    {
        Vector3 randomPosition = GetRandomPositionInBounds(spawnableArea);
        Instantiate(randomPrefab, randomPosition, Quaternion.identity);
    }
    
    
}

