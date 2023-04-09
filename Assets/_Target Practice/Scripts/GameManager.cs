using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameStats[] history;
   public int life = 10;
   public int levelCounter = 0;

    [SerializeField] TargetSpawner targetSpawner;


    [SerializeField] TMP_Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "" + 10;
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageToSubtract)
    {
        life -= damageToSubtract;
        lifeText.text = life.ToString();
    }

    //increase the level counter
    public void IncreaseLevel()
    {
        levelCounter++;
    }

    //decrease spawn rate
    public void DecreaseSpawnRate()
    {
        targetSpawner.spawnInterval -= 1f;
    }


}
