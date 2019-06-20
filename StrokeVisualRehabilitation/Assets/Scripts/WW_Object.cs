using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class WW_Object : MonoBehaviour {

    // Public variables
    public GameObject hint;

    // Private variables
    private GazeAware gazeAware;
    private int count;
    private float targetTime = 0.1f;
    private bool timer;

    private float ThetaScale = 0.01f;
    private float radius = 1f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;

    // Use this for initialization
    void Start () {
        // Move hint to position of frog
        hint.transform.position = new Vector3(transform.position.x, transform.position.y, 0.001f);
        
        // Get components
        gazeAware = GetComponent<GazeAware>();
        LineDrawer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        // If frog is being looked at
        if (gazeAware.HasGazeFocus)
        {
            // Start timer
            timer = true; 
        }
        // If frog isn't being looked at
        else
        {
            // Stop timer and reset target time
            timer = false;
            targetTime = 0.1f;
        }
        // If timer is on
        if (timer)
        {
            // Reduce target time
            targetTime -= Time.deltaTime;
        }
        // If target time is reached
        if (targetTime <= 0f)
        {
            // Draw circle around frog
            Theta = 0f;
            Size = (int)((1f / ThetaScale) + 1f);
            LineDrawer.positionCount = Size;
            for (int i = 0; i < Size; i++)
            {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = transform.position.x + (radius * Mathf.Cos(Theta));
                float y = transform.position.y + (radius * Mathf.Sin(Theta));
                LineDrawer.SetPosition(i, new Vector3(x, y, -0.01f));
            }
        }
    }
}
