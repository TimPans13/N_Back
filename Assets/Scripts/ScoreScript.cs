using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int score ;
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text=score.ToString();
    }
    public void IncreaseScore() 
    {
        score++;
    }
    public void DecreaseScore()
    {
        score--;
    }
}
