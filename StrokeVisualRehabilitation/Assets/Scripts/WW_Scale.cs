using System;
using UnityEngine;

public class WW_Scale : MonoBehaviour {

    // Use this for initialization
    void Start () {
        float quadHeight = Convert.ToSingle(Camera.main.orthographicSize * 2.0);
        float quadWidth = Convert.ToSingle(quadHeight * Screen.width / Screen.height);
        transform.localScale = new Vector3(quadWidth, quadHeight, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
