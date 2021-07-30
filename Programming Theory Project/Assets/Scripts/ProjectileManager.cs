using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    /// <summary>
    /// If the projectile collide with something not Enemy or Player units, destroy the projectile
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter( Collision collision )
    {
        if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag( "Player" ) )
        {
            Destroy(gameObject);
        }
    }
}
