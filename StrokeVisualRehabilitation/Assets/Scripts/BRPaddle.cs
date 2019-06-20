using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BRPaddle : MonoBehaviour {

    public float speed = 1000;
    public float alpha = 0.3f;
    private Vector2 _historicPoint;
    private bool _hasHistoricPoint;
    private bool paddlePaused;

    public bool PaddlePaused { get { return paddlePaused;} set {paddlePaused = value; } }

    void Update()
    {
        if (!paddlePaused)
        {
            Vector3 vector3 = new Vector3(((Smoothify(TobiiAPI.GetGazePoint().Screen).x / Screen.currentResolution.width) * 80) - 40, 2f, -20f);
            float offset = transform.localScale.x / 2;
            if (vector3.x - offset > -38.5 && vector3.x + offset < 40.5)
            {
                transform.position = vector3;
            }
        }    
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