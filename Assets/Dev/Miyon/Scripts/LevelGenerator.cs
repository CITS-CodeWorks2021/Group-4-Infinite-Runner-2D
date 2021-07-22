using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 10f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;
    public Transform EndPosition;

    public List<GameObject> spawned = new List<GameObject>();


    private void Start()
    {
       
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

        private void Update()
        {
        lastEndPosition = EndPosition.position;
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                //Spawn another level part
                SpawnLevelPart();
            }
        }
    
    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        EndPosition = lastLevelPartTransform.GetComponent<LevelChunk>().EndPosition;
        lastEndPosition = lastLevelPartTransform.GetComponent<LevelChunk>().EndPosition.position;
        spawned.Add(lastLevelPartTransform.gameObject);
        if (spawned.Count > 5) RemoveOldest();
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    public void Reset()
    {
        foreach(GameObject g in spawned)
        {
            Destroy(g);
        }
        spawned.Clear();
    }

    public void RemoveOldest()
    {
        Destroy(spawned[0]);
        spawned.RemoveAt(0);

    }

}
