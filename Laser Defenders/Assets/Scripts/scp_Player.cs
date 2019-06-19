using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scp_Player : MonoBehaviour
{
    //Configuration parameters
    float xMin;
    float xMax;
    float yMin;
    float yMax;    

    Coroutine fireCoroutine;   
       
    [Header("Player Movement")]    
    [SerializeField] [Range(0f,10f)] float xPadding = 1f;
    [SerializeField] [Range(0f,10f)] float yPadding = 1f;
    [SerializeField] [Range(0.1f, 30f)] float moveSpeed = 10f;

    [Header("Health Settings")]
    [SerializeField] float playerHealth = 500f;
   

    [Header("Projectiles Settings")]
    [SerializeField] [Range(10f, 50f)] float projectileSpeed = 10f; //Serialized for testing
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] public float countdown = 2f;

    
    

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
        
    }  

    // Update is called once per frame
    void Update()
    {        
        Move();
        Fire();
        

    }

    

    public void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {            
            StopCoroutine(fireCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        scp_DamageDealer damageDealer = other.gameObject.GetComponent<scp_DamageDealer>();
        ProcessHit(damageDealer);

    }

    private void ProcessHit(scp_DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (playerHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            //Projectile will spawn in front of ship with this Vector 2
            Vector3 laserStartingPositionVector =
                new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);


            //This will create one projectiles
            GameObject laser = Instantiate(laserPrefab, laserStartingPositionVector,
                                Quaternion.identity) as GameObject;

            //Disable gravity,and all other useless things, and then apply force to the bullet
            laser.GetComponent<Rigidbody2D>().gravityScale = 0;
            laser.GetComponent<Rigidbody2D>().drag = 0;
            laser.GetComponent<Rigidbody2D>().angularDrag = 0;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            
            yield return new WaitForSeconds(projectileFiringPeriod);

            
        }

        
        
    }

    private void Move()
    {
        //Setting up controls on y and x
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //New Positions
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        //Making sure it does not exit the screen
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //Update position using new variables
        transform.position = new Vector2(newXPos, newYPos);
    }    
    

    private void SetupMoveBoundaries()
    {
        //Creating a camera reference
        Camera gameCamera = Camera.main;

        //Defining boundaries on x
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.0f, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x - xPadding;

        //Defining boundaries on y
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0f, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - yPadding;

    }
}
