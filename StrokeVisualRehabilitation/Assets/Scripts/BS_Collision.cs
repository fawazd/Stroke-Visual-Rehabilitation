using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Collision : MonoBehaviour {

    public BS_GameManager gm;
    
    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<BS_GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "backWall")
        {
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Player")
        {
            gm.PlayChomp();
            gm.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
