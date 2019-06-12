using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class scp_WaveScriptableObject : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timesBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] public int numberOfEnemies = 10;
    [SerializeField] float moveSpeed = 2f;

    public  GameObject GetEnemyPrefab() {return enemyPrefab;}

    public  List<Transform> GetWaypoints()
    {
        var wavePoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            wavePoints.Add(child);
        }
        return wavePoints;
    }

    public float GetTimesBetweenSpawns() { return timesBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
