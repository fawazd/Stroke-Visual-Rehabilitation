using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WW_FoundWally : MonoBehaviour {

    public GameObject wally;
    public GameObject puzzle;
    public GameObject hint;
    public Text found;
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
        if ((wally.transform.position.x - 0.5 < transform.position.x) &&
                (wally.transform.position.x + 0.5 > transform.position.x) &&
                (wally.transform.position.y - 0.6 < transform.position.y) &&
                (wally.transform.position.y + 0.6 > transform.position.y))
        {
            count++;
        }
        else
        {
            count = 0;
        }
        if (count >= 60)
        {
            count = 0;
            puzzleNum++;
            switch (puzzleNum)
            {
                case 2:
                    wally.transform.position = new Vector3(5.22f, 1.72f, 0.001f);
                    hint.transform.position = new Vector3(5.22f, 1.72f, 0);
                    break;
                case 3:
                    wally.transform.position = new Vector3(-1.7f, 3.51f, 0.001f);
                    hint.transform.position = new Vector3(-1.7f, 3.51f, 0);
                    break;
                case 4:
                    wally.transform.position = new Vector3(-6.84f, 0.47f, 0.001f);
                    hint.transform.position = new Vector3(-6.84f, 0.47f, 0);
                    break;
                case 5:
                    wally.transform.position = new Vector3(2.87f, -2.34f, 0.001f);
                    hint.transform.position = new Vector3(2.87f, -2.34f, 0);
                    break;
                default:
                    break;
            }
            StartCoroutine(ShowMessage("You found Wally", 2, puzzleNum.ToString()));
        }
    }
}
