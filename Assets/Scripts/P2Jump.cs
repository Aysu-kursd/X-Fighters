using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Jump : MonoBehaviour
{
    public GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1SpaceDetector"))
        {
            if (Player1Movement.facingRight == true)
            {
                Player2.transform.Translate(-0.8f, 0, 0);
            }
            if (Player1Movement.facingLeft == true)
            {
                Player2.transform.Translate(0.8f, 0, 0);
            }
        }
    }
}
