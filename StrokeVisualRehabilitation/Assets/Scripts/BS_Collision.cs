using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Collision : MonoBehaviour {

    public BS_GameManager gm;

    /// <summary>
    /// Initializes variables
    /// </summary
    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<BS_GameManager>();
    }

    /// <summary>
    /// Collision logic
    /// </summary
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy object on collision with backwall
        if (collision.collider.name == "backWall")
        {
            Destroy(gameObject);
        }
        // Destroy object on collision with player
        // Also play sound and increase score
        if (collision.collider.tag == "Player")
        {
            gm.PlayChomp();
            gm.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
