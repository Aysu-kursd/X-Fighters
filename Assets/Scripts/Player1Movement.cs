using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    private Animator Anim;
    public float walkSpeed = 0.001f;
    public bool isJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool canWalkLeft = true;
    private bool canWalkRight = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 oppPosition;
    public static bool facingLeft = false;
    public static bool facingRight = true;
    public static bool walkLeftP1 = true;
    public static bool walkRightP1 = true;
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;
    public GameObject Restrict;
    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        Opponent = GameObject.Find("Player2");
            //Get animator components from child
            Anim = GetComponentInChildren<Animator>();
            //Get Audio components from child
            MyPlayer = GetComponentInChildren<AudioSource>();
           
            StartCoroutine(FaceLeft());
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we knocked out
        if (SaveScript.Player1Health <= 0)
        {
            Anim.SetTrigger("KnockOut");
            //Enable scripts when knocked out
            Player1.GetComponent<Player1Actions>().enabled = false;
            StartCoroutine(KnockedOut());
            
        }

        //Check victory
        if(SaveScript.Player2Health <= 0)
        {
            Anim.SetTrigger("Victory");
            Player1.GetComponent<Player1Actions>().enabled = false;
            this.GetComponent<Player1Movement>().enabled = false;
        }

        //Listen the animator about state info
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Prevent to character exit from screen
        //Create bounds for screen
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);
        
        //Character can not exit screen
        if(ScreenBounds.x > Screen.width - 100)
        {
            canWalkRight = false;
        }
        if (ScreenBounds.x < 100)
        {
            canWalkLeft = false;
        }
        //Character can walk when inside the screen bounds
        else if (ScreenBounds.x > 100 && ScreenBounds.x < Screen.width - 100)
        {
            canWalkRight = true;
            canWalkLeft = true;
        }

        //Get the opponent's position
        oppPosition = Opponent.transform.position;

        //Facing left or right of the opponent

        //Flip around to face opponent
        if(oppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if (oppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }


        //Walking left and right
        if (Player1Layer0.IsTag("Motion"))
        {
            Time.timeScale = 1.0f;
            //Move Forward when value is greater than 0
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (canWalkRight == true)
                {
                    if (walkRightP1 == true)
                    {
                        Anim.SetBool("Forward", true);
                        transform.Translate(-walkSpeed, 0, 0);
                    }
                }
            }
            //Move backward when value is lower than 0
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (canWalkLeft == true)
                {
                    if (walkLeftP1 == true)
                    {
                        Anim.SetBool("Backward", true);
                        transform.Translate(walkSpeed, 0, 0);
                    }
                }
            }
        }
        //else don't change position
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }

        //Jump and crouch
        //Jump trigger when value is greater than 0
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isJumping == false)
            {
                isJumping = true;
                Anim.SetTrigger("Jump");
                //create pause for prevent double jump with using coroutine
                StartCoroutine(jumpPause());
            }
        }
        //Crouch when value is lower than 0
        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }

        //Resets the restrict
        if(Restrict.gameObject.activeInHierarchy == false)
        {
            walkLeftP1 = true;
            walkRightP1 = true;
        }

        //Disable Colliders when blocking
        if (Player1Layer0.IsTag("Block"))
        {
            RB.isKinematic = true;
            BoxCollider.enabled = false;
            CapsuleCollider.enabled = false;
           
        }
        else
        {
            RB.isKinematic = false;
            BoxCollider.enabled = true;
            CapsuleCollider.enabled = true;
        }
    }

    //Reactions
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is currently blocking and ignore hits if so
        if (Player1Layer0.IsTag("Block"))
        {
            return; // Ignore the hit because the player is blocking
        }

        if (other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("HeavyReact");
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightKick;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("HeavyReact");
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
        }

    }


    //create pause
    IEnumerator jumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        isJumping = false;
    }

    IEnumerator FaceLeft()
    {
        if (facingLeft == true)
        {
            facingLeft = false;
            facingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1,0);
        }
    }
    IEnumerator FaceRight()
    {
        if (facingRight == true)
        {
            facingLeft = true;
            facingRight = false;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);
        }
    }
    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player1Movement>().enabled = false;
    }
}
