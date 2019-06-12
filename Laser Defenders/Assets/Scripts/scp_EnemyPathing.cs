using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_EnemyPathing : MonoBehaviour
{
    scp_WaveScriptableObject waveConfig;
    List<Transform> waypoints;    
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyShipMovement();
    }

    public void SetWaveConfig(scp_WaveScriptableObject waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void EnemyShipMovement()
    {
        //.Count in list, instead of array's .Lenght, since list starts at 0, we subtract 1 from the total
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed()* Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            //adding one to the waypointsIndex every time it reach a waypoints
            if (transform.position == targetPosition)
            {
                waypointIndex++;                
            }            
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Ship is destroyed");
        }
    }
}
