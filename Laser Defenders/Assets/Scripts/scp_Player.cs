using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player : MonoBehaviour
{
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    //Serialized for debugging
    [SerializeField] [Range(0f,10f)] float xPadding = 1f;
    [SerializeField] [Range(0f,10f)] float yPadding = 1f;
    [SerializeField] [Range(0.1f, 30f)] float moveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }  

    // Update is called once per frame
    void Update()
    {
        SetupMoveBoundaries();
        Move();
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
