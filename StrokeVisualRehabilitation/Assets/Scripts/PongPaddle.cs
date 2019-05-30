using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.SceneManagement;
public class PongPaddle: MonoBehaviour
{
    public bool isBottom;
    private float width;
    private PongBall ball;

    public float alpha = 0.3f;
    private Vector2 _historicPoint;
    private bool _hasHistoricPoint;

    // Start is called before the first frame update
    public void Init(bool isBottomPaddle, PongBall ball)
    {
        width = transform.localScale.x;
        this.ball = ball;
        isBottom = isBottomPaddle;
        ResetPaddle();
    }
    void Start()
    {

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

    // Update is called once per frame
    void Update()
    {
        if (double.IsNaN(TobiiAPI.GetGazePoint().Screen.x))
        {
            SceneManager.LoadScene("PongScene");
        }
        if (ball.Reset)
        {
            ResetPaddle();
        }
        var pos = transform.position;
        if (isBottom)
        {
            Vector2 input = Smoothify(TobiiAPI.GetGazePoint().Screen);
            pos.x = (input.x - Screen.width / 2) / 100;
        }
        else
        {
            if (ball.Direction.x > 0 && ball.transform.position.x > pos.x)
            {
                pos.x = pos.x + (Random.Range((float)2.5, (float)10) / 100);
            }
            else if(ball.Direction.x < 0 && ball.transform.position.x < pos.x)
            {
                pos.x = pos.x - (Random.Range((float)2.5, (float)10) / 100);
            }
        }
        if (pos.x - 1.25 > (Screen.width / 2 * -1) / 100 && pos.x + 1.25 < (Screen.width / 2) / 100)
        {
            transform.position = pos;
        }
    }

    void ResetPaddle()
    {
        var pos = Vector2.zero;
        if (isBottom)
        {
            pos = new Vector2(0, (float)-4.5);
        }
        else
        {
            pos = new Vector2(0, (float)4.5);
        }

        transform.position = pos;
    }
}
