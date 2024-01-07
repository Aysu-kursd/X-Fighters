using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public Image P1Green;
    public Image P2Green;
    public Image P1Red;
    public Image P2Red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1Green.fillAmount = SaveScript.Player1Health;
        P2Green.fillAmount = SaveScript.Player2Health;

        //Red Bar will remove after 2 seconds
        if(SaveScript.Player2Timer > 0)
        {
            SaveScript.Player2Timer -= 2.0f * Time.deltaTime;
        }

        //Remove bar
        if(SaveScript.Player2Timer <= 0)
        {
           if(P2Red.fillAmount > SaveScript.Player2Health)
            {
                P2Red.fillAmount -= 0.003f;
            }
        }

        if( SaveScript.Player1Timer > 0)
        {
            SaveScript.Player1Timer -= 2.0f * Time.deltaTime;
        }

        if(SaveScript.Player1Timer <= 0)
        {
            if(P1Red.fillAmount > SaveScript.Player1Health)
            {
                P1Red.fillAmount -= 0.003f;
            }
        }
            }
}
