using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class WW_Movement : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;
        float x = ((gazePoint.x / 1920) * 22) - 11;
        float y = ((gazePoint.y / 1080) * 11) - 5;
        transform.position = new Vector3(x,y,0);

	}
}
