using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class P1Select : MonoBehaviour
{
    public int MaxIcons = 6;
    public int IconsPerRow = 3;
    public int MaxRows = 2;

    public GameObject NinjaP1;
    public GameObject StrangeGuyP1;
    public GameObject ZombieP1;
    

    public GameObject NinjaP1Text;
    public GameObject StrangeGuyP1Text;
    public GameObject ZombieP1Text;

    public TextMeshProUGUI P1Name;

    public string CharacterSelectionP1;

    private int IconNumber = 1;
    private int RowNumber = 1;
    private float PauseTime = 1.0f;
    private bool TimeCountDown = false;
    private bool ChangeCharacter = false;
    private AudioSource MyPlayer;



    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter = true;
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SaveScript.P1Select);
        //Switching character
        if (ChangeCharacter == true)
        {
            //first char
            if (IconNumber == 1)
            {
                SwitchOff();
                NinjaP1.gameObject.SetActive(true);
                NinjaP1Text.gameObject.SetActive(true);
                P1Name.text = "Ninja";
                CharacterSelectionP1 = "NinjaP1";
                ChangeCharacter = false;
            }
            //second char
            if (IconNumber == 2)
            {
                SwitchOff();
                StrangeGuyP1.gameObject.SetActive(true);
                StrangeGuyP1Text.gameObject.SetActive(true);
                P1Name.text = "Strange Guy";
                CharacterSelectionP1 = "StrangeGuyP1";
                ChangeCharacter = false;
            }
            //third char
            if (IconNumber == 3)
            {
                SwitchOff();
                ZombieP1.gameObject.SetActive(true);
                ZombieP1Text.gameObject.SetActive(true);
                P1Name.text = "Zombie";
                CharacterSelectionP1 = "ZombieP1";
                ChangeCharacter = false;
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            SaveScript.P1Select = CharacterSelectionP1;
            MyPlayer.Play();
            NextPlayer();
        }

        //Debug to show icon num on console
        Debug.Log("Icon number = " + IconNumber);



        // moving between icons
        if (TimeCountDown == true)
        {
            if (PauseTime > 0.1f)
            {
                PauseTime -= 1.0f * Time.deltaTime;
            }
            if (PauseTime <= 0.1f)
            {
                PauseTime = 1.0f;
                TimeCountDown = false;
            }
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (PauseTime == 1.0f)
            {
                if (IconNumber < IconsPerRow * RowNumber)
                {
                    IconNumber++;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (PauseTime == 1.0f)
            {
                if (IconNumber > IconsPerRow * (RowNumber - 1) +1)
                {
                    IconNumber--;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (PauseTime == 1.0f)
            {
                if (RowNumber < MaxRows)
                {
                    IconNumber += IconsPerRow;
                    RowNumber++;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            if (PauseTime == 1.0f)
            {
                if (RowNumber > 1)
                {
                    IconNumber -= IconsPerRow;
                    RowNumber--;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
    }
    
    void SwitchOff()
    {
        NinjaP1.gameObject.SetActive(false);
        StrangeGuyP1.gameObject.SetActive(false);
        ZombieP1.gameObject.SetActive(false);

        NinjaP1Text.gameObject.SetActive(false);
        StrangeGuyP1Text.gameObject.SetActive(false);
        ZombieP1Text.gameObject.SetActive(false);
    }

    void NextPlayer()
    {
        NinjaP1Text.gameObject.SetActive(false);
        StrangeGuyP1Text.gameObject.SetActive(false);
        ZombieP1Text.gameObject.SetActive(false);

        if(SaveScript.Player1Mode == true)
        {
            this.gameObject.GetComponent<CPUSelect>().enabled = true;
            this.gameObject.GetComponent<P1Select>().enabled = false;
        }
        if (SaveScript.Player1Mode == false)
        {
            this.gameObject.GetComponent<P2Select>().enabled = true;
            this.gameObject.GetComponent<P1Select>().enabled = false;
        }
    }
}
