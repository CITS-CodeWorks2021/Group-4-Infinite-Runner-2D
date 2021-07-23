using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantKill : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        if (col2D.gameObject.CompareTag("Obstacle")) //If object hit is tagged "Obstacle"
        {
            Destroy(gameObject); //Then destroy the player
            FindObjectOfType<GameController>().EndGame();
            
        }
    }
}
