using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    public int lowestScore;
    public int currentScore;
    public List<int> highScores;
   
    public Text t_lowScore, t_curScore, t_highScores;

    bool newHigh, newLow, newHighest;

    // First we get the scores which already exist
    void Start()
    {
        // 4 high scores
        // 1 low
        if (PlayerPrefs.HasKey("firstScore")) highScores.Add(PlayerPrefs.GetInt("firstScore"));
        if (PlayerPrefs.HasKey("secondScore")) highScores.Add(PlayerPrefs.GetInt("secondScore"));
        if (PlayerPrefs.HasKey("thirdScore")) highScores.Add(PlayerPrefs.GetInt("thirdScore"));
        if (PlayerPrefs.HasKey("fourthScore")) highScores.Add(PlayerPrefs.GetInt("fourthScore"));

        if (PlayerPrefs.HasKey("lowScore")) lowestScore = PlayerPrefs.GetInt("lowScore");

        // Current score
        // lowest score
        // highest score
    }

    public void AddScore(int newScore)
    {
        bool newHighScore = false;
        foreach (int i in highScores)
        {
            if (i < newScore) newHighScore = true;
        }

        if (newHighScore)
        {
            highScores.Add(newScore);
            highScores.Sort();
            highScores.Reverse();
            if (highScores.Count > 4) highScores.Capacity = 4;
            newHigh = true;
            if (highScores[0] == newScore) newHighest = true;
        }

        if (newScore < lowestScore)
        {
            lowestScore = newScore;
            newLow = true;
        }


        if (newLow)
        {
            // do a new Low stuff

        }
        if (newHigh)
        {

        }
        if (newHighest)
        {

        }

        WriteScores();
        UpdateUI();
    }

    public void UpdateUI()
    {
        t_curScore.text = currentScore.ToString();
        t_lowScore.text = currentScore.ToString();

        for (int i = 0; i < highScores.Count; i++)
        {
            t_highScores.text = highScores[i].ToString() + "\n";
        }
    }


    public void WriteScores()
    {
        if (highScores.Count > 0) PlayerPrefs.SetInt("firstScore", highScores[0]);
        if (highScores.Count > 1) PlayerPrefs.SetInt("secondScore", highScores[1]);
        if (highScores.Count > 2) PlayerPrefs.SetInt("thirdScore", highScores[2]);
        if (highScores.Count > 3) PlayerPrefs.SetInt("fourthScore", highScores[3]);

        PlayerPrefs.SetFloat("lowScore", lowestScore);
    }


    // Update is called once per frame
    void Update()
    {
  
    }
}
