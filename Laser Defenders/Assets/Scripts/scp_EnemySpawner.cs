using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_EnemySpawner : MonoBehaviour
{
    [SerializeField] List<scp_WaveScriptableObject> waveConfig;
    int startingWave = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfig[startingWave];
        StartCoroutine(SpawnAllEnemiesInWaves(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWaves(scp_WaveScriptableObject waveConfig)
    {
        Debug.Log("Coroutine started");        
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            Instantiate(
                        waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWaypoints()[0].transform.position,
                        Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimesBetweenSpawns());
        }
    }

}
