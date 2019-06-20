using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class GazePointScript : MonoBehaviour
{

    public float speed = 1000;
    public float alpha = 0.3f;
    private Vector2 _historicPoint;
    private bool _hasHistoricPoint;
    public MemGameMan gameManager;
    private float opacity = 0.5f;

    public float Opacity { get { return opacity; } set { opacity = value; } }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameIsPaused)
        {
            Vector3 vector3 = new Vector3((Smoothify(TobiiAPI.GetGazePoint().Viewport).x * 21f) - 10.5f, 5f,
            (Smoothify(TobiiAPI.GetGazePoint().Viewport).y * 12f) - 6f);
            transform.position = vector3;
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