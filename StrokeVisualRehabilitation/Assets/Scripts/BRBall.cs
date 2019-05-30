using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BRBall : MonoBehaviour {

    public BRBall ball;
    public BRGameManager gameManager;
    public Vector3 intialImpulse;
    private bool ballInPlay;

    // Use this for initialization
    void Start () {
      
	}
	 
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !ballInPlay && !gameManager.GameIsPaused)
        {
            GetComponent<Rigidbody>().AddForce(intialImpulse, ForceMode.Impulse);
            ballInPlay = true;
        }
        if (!ballInPlay)
        {
            ball.transform.position = new Vector3(GameObject.Find("BRPaddle").transform.position.x, 2f, (GameObject.Find("BRPaddle").transform.position.z + (2 / ball.transform.localScale.z)));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BRBrick(Clone)")
        {
            gameManager.Score = gameManager.Score += 100;

            if (collision.gameObject.GetComponent<Renderer>().material.color == Color.green)
            {
                collision.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            }
            else if(collision.gameObject.GetComponent<Renderer>().material.color == Color.cyan)
            {
                collision.gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                Destroy(collision.gameObject);
                gameManager.Score = gameManager.Score += 100;
            }
        }

        if(collision.gameObject.name == "arenaEdge (3)")
        {
            ResetBall();
            gameManager.Lives = gameManager.Lives -= 1;
        }
    }

    public void ResetBall()
    {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.position = new Vector3(GameObject.Find("BRPaddle").transform.position.x, 2f, (GameObject.Find("BRPaddle").transform.position.z + ball.transform.localScale.z));
        ball.ballInPlay = false;
    }
}
