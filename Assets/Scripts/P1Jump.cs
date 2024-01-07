using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Jump : MonoBehaviour
{
    public GameObject Player1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2SpaceDetector"))
        {
            if (Player1Movement.facingRight == true)
            {
                Player1.transform.Translate(-0.8f, 0, 0);
            }
            if(Player1Movement.facingLeft == true)
            {
                Player1.transform.Translate(0.8f, 0, 0);
            }
        }
    }
}
