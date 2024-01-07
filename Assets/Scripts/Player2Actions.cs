using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    private Animator Anim;
    public float jumpSpeed = 1.0f;
    public GameObject Player1;
    private AnimatorStateInfo Player1Layer0;
    public float PunchMovespeed = 0.3f;
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    public static bool HitsP2 = false;
    // Start is called before the first frame update
    void Start()
    {
        //Get components from animator
        Anim = GetComponent<Animator>();
        //Get components from aaudio source
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Listen the animator about state info
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Standing Attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            //If  press Fire1 button lightpunch animation will trigger
            if (Input.GetButtonDown("Fire1P2"))
            {
                Anim.SetTrigger("LightPunch");
                HitsP2 = false;
            }
            //If  press Fire2 button HeavyPunch animation will trigger
            if (Input.GetButtonDown("Fire2P2"))
            {
                Anim.SetTrigger("HeavyPunch");
                HitsP2 = false;
            }
            //If  press Fire3 button LightKick animation will trigger
            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
            //If  press Fire4 button HeavyKick animation will trigger
            if (Input.GetButtonDown("JumpP2"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }

            //if press tab button center block animation will trigger
            if (Input.GetButtonDown("BlockP2"))
            {
                Anim.SetTrigger("BlockOn");
            } 
        }
        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }
        //Crouching Attack
        if (Player1Layer0.IsTag("Crouching"))
            {
                //If press Fire3 button when crouching, LegSweep animation will trigger
                if (Input.GetButtonDown("Fire3P2"))
                {
                    Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
            }

            //Jumping Attack
            if (Player1Layer0.IsTag("Jumping"))
            {
                //If  press Fire4 button when jumping HurricaneKick animation will trigger
                if (Input.GetButtonDown("JumpP2"))
                {
                    Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }
            }
        
    }
    //Function for jump up
     public void JumpUp()
    {
        Player1.transform.Translate(0, jumpSpeed, 0);
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0, jumpSpeed, 0);
        Player1.transform.Translate(0.1f, 0, 0);

    }
    public void Flipback()
    {
        Player1.transform.Translate(0, jumpSpeed, 0);
        Player1.transform.Translate(-0.1f, 0, 0);

    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }
    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    public void HeavyPunchMove()
    {
        Player1.transform.Translate(-PunchMovespeed, 0, 0);
    }
    public void HeavyPunchMoveFlip()
    {
        Player1.transform.Translate(PunchMovespeed, 0, 0);
    }
}