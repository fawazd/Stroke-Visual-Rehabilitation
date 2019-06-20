using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BS_PlayerMovement : MonoBehaviour {

    //Public variables
    public Rigidbody rb;
    public float sidewaysInitalForce;


    // Update is called once per frame
    void FixedUpdate () {
        rb.drag = 0;
        // Get point where user is looking
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;

        // Convert x screen pixel co ordinate to relative x co ordinate
        float x = ((gazePoint.x / Screen.currentResolution.width) * 15) - 6;

        // If user is looking to left of rabbit push rabbit left
        if (x < rb.position.x - 0.3)
        {
            rb.AddForce(-sidewaysInitalForce * Time.deltaTime, 0, 0);
        }
        // If user is looking to right of rabbit push rabbit right
        else if (x > rb.position.x + 0.3)
        {
            rb.AddForce(sidewaysInitalForce * Time.deltaTime, 0, 0);
        }
        // If user is looking at rabbit increase drag to stop rabbit from moving
        else
        {
            rb.drag = 10;
        }
    }
}
