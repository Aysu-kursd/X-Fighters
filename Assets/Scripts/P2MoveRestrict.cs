using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("P1Left"))
        {
            Player2Movement.walkRight = false;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Movement.walkLeft = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Movement.walkRight = true;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Movement.walkLeft = true;
        }
    }
}
