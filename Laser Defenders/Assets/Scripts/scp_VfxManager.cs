using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_VfxManager : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] scp_Enemy[] enemy;

    public void Explosion()
    {
        if (enemy != null)
        {
            Debug.Log("Explosion() executed");

            Vector2 explosionStartingPositionZero =
                new Vector2(enemy[0].transform.position.x, enemy[0].transform.position.y);

            Vector2 explosionStartingPositionOne =
                new Vector2(enemy[1].transform.position.x, enemy[1].transform.position.y);

            //Vector2[] explosionPositions = { explosionStartingPositionZero, explosionStartingPositionOne };

            
            GameObject explosionZero =
                    Instantiate(explosionParticle, explosionStartingPositionZero, Quaternion.identity);
           
            GameObject explosionOne =
                    Instantiate(explosionParticle, explosionStartingPositionOne, Quaternion.identity);
           
        }
        
        
    }
}
