using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private void OnCollisionEnter( Collision collision )
    {
        if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag( "Player" ) )
        {
            Destroy(gameObject);
        }
    }
}
