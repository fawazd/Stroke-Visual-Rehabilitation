using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class GazePointScript : MonoBehaviour {

    public float speed = 1000;
    public float alpha = 0.3f;
    private Vector2 _historicPoint;
    private bool _hasHistoricPoint;

    private float opacity = 0.5f;

    public float Opacity { get { return opacity; } set { opacity = value; } }
	
	// Update is called once per frame
	void Update () {
        Vector3 vector3 = new Vector3(((Smoothify(TobiiAPI.GetGazePoint().Screen).x / Screen.currentResolution.width) * 36) - 18, .6f, (Smoothify(TobiiAPI.GetGazePoint().Screen).y / Screen.currentResolution.height) * 17 - 7f);
        transform.position = vector3;
    }

    private Vector2 Smoothify(Vector2 point)
    {
        if (!_hasHistoricPoint)
        {
            _historicPoint = point;
            _hasHistoricPoint = true;
        }

        var smoothedPoint = new Vector2(point.x * alpha + _historicPoint.x * (1.0f - alpha),
            point.y * alpha + _historicPoint.y * (1.0f - alpha));

        _historicPoint = smoothedPoint;

        return smoothedPoint;
    }
}
