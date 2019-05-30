using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongGameManager : MonoBehaviour
{
    public PongBall ball;
    public PongPaddle paddle;

    public PongPaddle playerPaddle;
    public PongPaddle computerPaddle;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        ball = Instantiate(ball) as PongBall;
        playerPaddle = Instantiate(paddle) as PongPaddle;
        computerPaddle = Instantiate(paddle) as PongPaddle;

        playerPaddle.Init(true, ball);
        computerPaddle.Init(false, ball);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
