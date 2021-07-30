using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public static bool gameOver { get; private set; }
    public static bool won { get; private set; }

    private void OnTriggerEnter( Collider other )
    {
        if (other.CompareTag("Enemy") && gameObject.name.Equals("FlagA"))
        {
            gameOver = true;
        }
        else if ( other.CompareTag( "Player" ) && gameObject.name.Equals( "FlagB" ) )
        {
            won = true;
        }


    }
}
