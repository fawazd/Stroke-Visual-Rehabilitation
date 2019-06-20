using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class WW_Movement : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        // Get user gaze co ordinates
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;

        // Convert from raw co ordinates to in game co ordinates
        float x = ((gazePoint.x / 1920) * 22) - 11;
        float y = ((gazePoint.y / 1080) * 11) - 5;

        // Move object
        transform.position = new Vector3(x,y,0);
	}
}
