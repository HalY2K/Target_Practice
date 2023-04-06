using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public int score = 0;

    [SerializeField] TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
   
}
