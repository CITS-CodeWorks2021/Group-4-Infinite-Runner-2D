using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int Score;
    public Text ScoreText;
    public ScoreBoard scoreBoard;
    public GameObject endScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        InstantKill.playerDies.AddListener(EndGame); 
    }

   

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        AddScore();
    }

    void AddScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }

    void EndGame()
    {
        scoreBoard.AddScore(Score);
        endScoreBoard.SetActive(true);
    }
}
