using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringController : MonoBehaviour
{
    public static ScoringController instance;
    public int score = 0;
    private Text scoreText;

    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Scoring(0);
    }

    void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text = score.ToString();
        }
    }

    public void Scoring(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
