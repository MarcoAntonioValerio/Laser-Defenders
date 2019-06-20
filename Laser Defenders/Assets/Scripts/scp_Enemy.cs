﻿using System;
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
    [SerializeField] GameObject deathVfx;
    [SerializeField] float durationOfExplosion = 1f;

    [SerializeField] AudioClip fireSounds;
    [SerializeField] AudioClip explosionSounds;
    [Range(0f, 1f)] [SerializeField] float fireVolumeSlider = 0.5f;
    [SerializeField] float explosionVolume = 1f;
    private AudioSource audioSource;

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
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(scp_DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0f)
        {
            Death();
            AudioSource.PlayClipAtPoint(fireSounds, Camera.main.transform.position, fireVolumeSlider);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVfx, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        ExplosionSound();
    }

    private void AudioPlayer()
    {
        Debug.Log("AudioPlayer() is firing.");
        //Get the AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void ExplosionSound()
    {
        Debug.Log("ExpolsionSound() fired");
        //Play the sounds        
        AudioSource.PlayClipAtPoint(explosionSounds, Camera.main.transform.position, explosionVolume);

    }

    private void FireSound()
    {
        //Change the pitch of the sound on a Random Range between 0.1/1
        audioSource.pitch = (UnityEngine.Random.Range(0.7f, 1f));
        //Play the sounds        
        AudioSource.PlayClipAtPoint(fireSounds, Camera.main.transform.position, fireVolumeSlider);



    }
}
