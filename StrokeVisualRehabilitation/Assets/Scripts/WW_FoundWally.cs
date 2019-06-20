using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;

public class WW_FoundWally : MonoBehaviour {

    // Public variables
    public GameObject frog1;
    public GameObject frog2;
    public GameObject frog3;
    public GameObject puzzle;
    public GameObject hint1;
    public GameObject hint2;
    public GameObject hint3;
    public Text found;
    public GazeAware gazeAware;

    // Provate variables
    int count = 0;
    int puzzleNum = 1;

    IEnumerator ShowMessage(string message, float delay, string newPuzzle)
    {
        found.text = message;
        found.enabled = true;
        yield return new WaitForSeconds(delay);
        found.enabled = false;
        puzzle.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/wally" + newPuzzle);
    }

    // Update is called once per frame
    void Update () {
        //if (frog1.GetComponent<GazeAware>().HasGazeFocus)
        //{
        //    Debug.Log("Hello");
        //}
        //StartCoroutine(ShowMessage("You found Wally", 2, puzzleNum.ToString()));
    }
}
