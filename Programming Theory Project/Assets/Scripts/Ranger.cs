using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Unit // INHERITANCE from Unit
{
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject projectileSpawnPoint;

    private float projectileSpeed;

    Ranger()
    {
        // Set Ranger attributes
        health = 35;
        speed = 4;
        damage = 18;
        checkRange = 6;
        attackRange = 5;
        attackSpeed = 3f;
        projectileSpeed = 4f;

    }


    protected override void Hit() // POLYMORPHISM
    {
        StartCoroutine( HitDamage() ); // Specific Attack for Ranger Class
    }


    /// <summary>
    /// Attack mode spedific for units to the nearest enemy unit. When the enemy unit health < 1the unit will be destroyed
    /// </summary>
    /// <returns></returns>
    IEnumerator HitDamage()
    {
        if ( nearestEnemy != null)
        {
            //Debug.Log( "Nearest " + nearestEnemy.GetComponent<Unit>().tag + " " + nearestEnemy.GetComponent<Unit>().name + " with health " + nearestEnemy.GetComponent<Unit>().health );

            while ( nearestEnemy.GetComponent<Unit>().health > 1 )
            {
                if ( nearestEnemy != null )
                {
                    // Ranger ability
                    ThrowArrow(); //ABSTRACTION

                    // Update nearestEnemy Health bar and health
                    UpdateEnemyHealthStatus(); //ABSTRACTION
                }
                else
                    break;

                yield return new WaitForSeconds( attackSpeed );

            }
            if ( nearestEnemy != null )
            {
                Destroy( nearestEnemy.gameObject );
                base.Hit();
            }
                
        }
        
    }

    /// <summary>
    /// Instantiate and throw the arrow
    /// </summary>
    private void ThrowArrow() //ABSTRACTION
    {
        GameObject arrowRef = Instantiate(arrow, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
        arrowRef.GetComponent<Rigidbody>().AddForce( arrowRef.transform.forward * projectileSpeed, ForceMode.Impulse );
    }

    /// <summary>
    /// Update nearestEnemy Health bar and health
    /// </summary>
    private void UpdateEnemyHealthStatus() //ABSTRACTION
    {
        if ( nearestEnemy.GetComponent<Unit>() is Warrior )
            nearestEnemy.GetComponent<Warrior>().healthBar.value -= damage;
        else if ( nearestEnemy.GetComponent<Unit>() is Wizard )
            nearestEnemy.GetComponent<Wizard>().healthBar.value -= damage;
        else if ( nearestEnemy.GetComponent<Unit>() is Ranger )
            nearestEnemy.GetComponent<Ranger>().healthBar.value -= damage;

        nearestEnemy.GetComponent<Unit>().health -= damage;
    }

    /// <summary>
    /// Destroy the projectile that collide with the unit
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Projectile" ) )
        {
            Destroy( other.gameObject );
        }
    }

}
