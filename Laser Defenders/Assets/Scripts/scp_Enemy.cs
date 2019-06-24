using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy : MonoBehaviour
{
    [Header("Initialising Settings")]
    [SerializeField] public float health = 500f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 3f;
    [SerializeField] float maxTimeBetweenShots = 10f;
    [Header("Projectiles Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] GameObject deathVfx;
    [SerializeField] float durationOfExplosion = 1f;
    [Header("Audio Settings")]
    [SerializeField] AudioClip fireSounds;
    [SerializeField] AudioClip explosionSounds;
    [Range(0f, 1f)] [SerializeField] float fireVolumeSlider = 0.5f;
    [SerializeField] float explosionVolume = 1f;    
    private AudioSource audioSource;
    [Header("Score Settings")]
    [SerializeField] float scorePointsEachSpaceship = 1234f;
    scp_GameManager gameMan;
    

    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameMan = FindObjectOfType<scp_GameManager>();
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


        FireSound();
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
            ExplosionSound();
            AddToScore();
            
        }
    }

    private void AddToScore()
    {
        gameMan.totalScore += scorePointsEachSpaceship;
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
        var source = GetComponent<AudioSource>();
        source.pitch = UnityEngine.Random.Range(0.8f, 1f);
        source.PlayOneShot(fireSounds, 0.1f);
    }
}
