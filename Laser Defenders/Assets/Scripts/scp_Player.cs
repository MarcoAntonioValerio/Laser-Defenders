﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player : MonoBehaviour
{
    //Configuration parameters
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    bool isDestroyed = false;

    [SerializeField] float countdown = 3f;

    [SerializeField][Range(10f,50f)]float projectileSpeed = 10f; //Serialized for testing

    [SerializeField] [Range(0f,10f)] float xPadding = 1f;
    [SerializeField] [Range(0f,10f)] float yPadding = 1f;
    [SerializeField] [Range(0.1f, 30f)] float moveSpeed = 10f;

    
    [SerializeField] GameObject laserPrefab;


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
            //Projectile will spawn in front of ship with this Vector 2
            Vector3 laserStartingPositionVector = 
                new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            
            //This will create the projectiles
            GameObject laser =  Instantiate(laserPrefab,laserStartingPositionVector,
                                Quaternion.identity) as GameObject;

            //Disable gravity,and all other useless things, and then apply force to the bullet
            laser.GetComponent<Rigidbody2D>().gravityScale = 0;
            laser.GetComponent<Rigidbody2D>().drag = 0;
            laser.GetComponent<Rigidbody2D>().angularDrag = 0;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            //Destroy bullet
            
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