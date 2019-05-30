using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BS_PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float forwardForce;
    public float sidewaysInitalForce;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate () {
        rb.drag = 0;
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;
        float y = ((gazePoint.y / 1080) * 150);
        float x = ((gazePoint.x / 1920) * 14) - 7;

        if (x < rb.position.x - 0.25)
        {
            rb.AddForce(-sidewaysInitalForce * Time.deltaTime, 0, 0);
        }
        else if (x > rb.position.x + 0.25)
        {
            rb.AddForce(sidewaysInitalForce * Time.deltaTime, 0, 0);
        }
        else
        {
            rb.drag = 10;
        }
    }
}
