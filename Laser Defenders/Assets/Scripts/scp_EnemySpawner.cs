using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_EnemySpawner : MonoBehaviour
{
    [SerializeField] List<scp_WaveScriptableObject> waveConfig;
    [SerializeField]int startingWave = 0;
    [SerializeField] bool looping = false;
   

    // Start is a coroutine now, will execute once, and then will loop if looping is true.
    IEnumerator Start()
    {

        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        
    }


    /*This coroutine will call the sub coroutine at every iteration of its loop
      making sure that every waves will spawn.*/
    private IEnumerator SpawnAllWaves()
    {
        /*The for loop creates a new variable(waveIndex), that is equal to startingWave. 
         *if waveIndex is lower than the waveConfig.count, add one to waveIndex after 
          the statement run.*/
        for (int waveIndex = startingWave; waveIndex < waveConfig.Count; waveIndex++)
        {
            /*The variable of type scp_WaveScriptableObject will be equal to the list, 
              specifically taking waveIndex as a parameter.*/
            var currentWave = waveConfig[waveIndex];

            //Wait 3 seconds, then start the "sub" coroutine everytime the loop starts.
            yield return new WaitForSeconds(3);
            yield return StartCoroutine(SpawnAllEnemiesInWaves(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWaves(scp_WaveScriptableObject waveConfig)
    {
        Debug.Log("Coroutine started");        
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                        waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWaypoints()[0].transform.position,
                        Quaternion.identity);
            newEnemy.GetComponent<scp_EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimesBetweenSpawns());
        }
    }

}
