using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongBall : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float radius;
    private bool reset;
    private Vector2 direction;
    private int playerScore;
    private int computerScore;
    private Text compText;
    private Text playerText;

    // Start is called before the first frame update
    void Start()
    {
        reset = false;
        direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2;
        playerScore = 0;
        computerScore = 0;
        resetText();
    }

    // Update is called once per frame
    void Update()
    {
        reset = false;
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.x < -9 + radius)
        {
            direction.x = -direction.x;
        }
        if (transform.position.x > 9 - radius)
        {
            direction.x = -direction.x;
        }
        if (transform.position.y < -5 + radius)
        {
            reset = true;
            computerScore += 1;
            resetBall();
            resetText();
        }
        if (transform.position.y > 5 - radius)
        {
            reset = true;
            playerScore += 1;
            resetBall();
            resetText();
        }
    }

    void resetText()
    {
        compText.text = computerScore.ToString();
        playerText.text = playerScore.ToString();
    }

    void resetBall()
    {
        var pos = new Vector2(0, 0);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            bool isBottom = collision.GetComponent<PongPaddle>().isBottom;

            if (isBottom && direction.y < 0)
            {
                direction.y = -direction.y;
            }
            if (!isBottom && direction.y > 0)
            {
                direction.y = -direction.y;
            }
        }
    }

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    public int ComputerScore
    {
        get
        {
            return computerScore;
        }

        set
        {
            computerScore = value;
        }
    }

    public Text CompText
    {
        get
        {
            return compText;
        }

        set
        {
            compText = value;
        }
    }

    public Text PlayerText
    {
        get
        {
            return playerText;
        }

        set
        {
            playerText = value;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public bool Reset
    {
        get
        {
            return reset;
        }

        set
        {
            reset = value;
        }
    }
}
