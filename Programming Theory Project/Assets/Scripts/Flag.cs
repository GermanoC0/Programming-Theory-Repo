using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // ENCAPSULATION
    public static bool gameOver { get; private set; }
    public static bool won { get; private set; }

    /// <summary>
    /// Manage the collision between unit and flag
    /// </summary>
    /// <param name="other"></param>
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
