using Tobii.Gaming;
using UnityEngine;

public class CardScript : MonoBehaviour
{

    private Animator anim;
    public GameObject card;

    private float targetTime = 1f;
    private bool timer;

    public MemGameMan memGameMan;

    public AudioClip hover;
    public AudioClip flip;

    public AudioSource audioSource;

    public GazeAware gazeAware;

    // Use this for initialization
    void Start()
    {
        gazeAware = GetComponent<GazeAware>();
        anim = GetComponent<Animator>();
        memGameMan = GameObject.Find("GameManager").GetComponent<MemGameMan>();
        audioSource = GameObject.Find("GameAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gazeAware.HasGazeFocus)
        {
            timer = true;
        }
        else
        {
            timer = false;
            targetTime = 1f;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hover"))
            {
                Animate("Idle", null);
            }
        }
        if (timer)
        {
            targetTime -= Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && memGameMan.Cards.Count < 2)
            {
                Animate("Hover", null);
            }
        }
        if (targetTime <= 0f)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hover"))
            {
                Animate("FlipUp", null);
                memGameMan.Cards.Add(this);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    timer = true;        
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    timer = false;
    //    targetTime = 3f;
    //    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hover"))
    //    {
    //        Animate("Idle", null);
    //    }
    //}

    public void Animate(string name, AudioClip audio)
    {
        anim.Play(name);
        audioSource.PlayOneShot(audio);
    }

}