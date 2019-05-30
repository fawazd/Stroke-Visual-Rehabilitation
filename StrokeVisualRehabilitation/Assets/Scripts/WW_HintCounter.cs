using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WW_HintCounter : MonoBehaviour {

    public MeshRenderer mr;
    public float hintLength;
    public float hintTimer;
    int counter;
    float currSize;
    float startSize = 1;
	
	// Update is called once per frame
	void Update () {
        counter++;
        if (counter%hintTimer == 0)
        {
            mr.enabled = true;
            currSize = startSize;
        }
        else if (counter % hintTimer == hintLength)
        {
            mr.enabled = false;
            startSize += 0.5f;
            transform.localScale = new Vector3(startSize, startSize, 0.01f);
        }
        else if ((counter % hintTimer > 0) && (counter % hintTimer < hintLength))
        {
            currSize -= (currSize / 4);
            transform.localScale = new Vector3(currSize, currSize, 0.01f);
        }
	}
}
