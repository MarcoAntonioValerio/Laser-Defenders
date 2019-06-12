using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy : MonoBehaviour
{
    [SerializeField] float health = 500f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 3f;
    [SerializeField] float maxTimeBetweenShots = 10f;

    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 1f;


    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    private void Update()
    {
        CountDownAndShoot();
        Debug.Log(health);
    }

 
    private void CountDownAndShoot()

    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        Vector3 laserStartingPosition = new Vector3
            (transform.position.x, transform.position.y - 1f, transform.position.z);

        GameObject laser = Instantiate (projectile, laserStartingPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        scp_DamageDealer damageDealer = other.gameObject.GetComponent<scp_DamageDealer>();
        ProcessHit(damageDealer);

    }

    private void ProcessHit(scp_DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
