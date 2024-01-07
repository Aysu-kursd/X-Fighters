using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class P2Select : MonoBehaviour
{
    public int MaxIcons = 6;
    public int IconsPerRow = 3;
    public int MaxRows = 2;

    public GameObject NinjaP2;
    public GameObject StrangeGuyP2;
    public GameObject ZombieP2;
    

    public GameObject NinjaP2Text;
    public GameObject StrangeGuyP2Text;
    public GameObject ZombieP2Text;

    public TextMeshProUGUI P2Name;

    public string CharacterSelectionP2;

    private int IconNumber = 1;
    private int RowNumber = 1;
    private float PauseTime = 1.0f;
    private bool TimeCountDown = false;
    private bool ChangeCharacter = false;
    private AudioSource MyPlayer;
    public int Scene = 1;



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
                NinjaP2.gameObject.SetActive(true);
                NinjaP2Text.gameObject.SetActive(true);
                P2Name.text = "Ninja";
                CharacterSelectionP2 = "NinjaP2";
                ChangeCharacter = false;
            }
            //second char
            if (IconNumber == 2)
            {
                SwitchOff();
                StrangeGuyP2.gameObject.SetActive(true);
                StrangeGuyP2Text.gameObject.SetActive(true);
                P2Name.text = "Strange Guy";
                CharacterSelectionP2 = "StrangeGuyP2";
                ChangeCharacter = false;
            }
            //third char
            if (IconNumber == 3)
            {
                SwitchOff();
                ZombieP2.gameObject.SetActive(true);
                ZombieP2Text.gameObject.SetActive(true);
                P2Name.text = "Zombie";
                CharacterSelectionP2 = "ZombieP2";
                ChangeCharacter = false;
            }
        }


        if (Input.GetButtonDown("Fire1P2"))
        {
            SaveScript.P2Select = CharacterSelectionP2;
            MyPlayer.Play();
            SceneManager.LoadScene(Scene);
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
        if (Input.GetAxis("HorizontalP2") > 0)
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
        if (Input.GetAxis("HorizontalP2") < 0)
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
        if (Input.GetAxis("VerticalP2") < 0)
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
        if (Input.GetAxis("VerticalP2") > 0)
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
        NinjaP2.gameObject.SetActive(false);
        StrangeGuyP2.gameObject.SetActive(false);
        ZombieP2.gameObject.SetActive(false);

        NinjaP2Text.gameObject.SetActive(false);
        StrangeGuyP2Text.gameObject.SetActive(false);
        ZombieP2Text.gameObject.SetActive(false);
    }
}
