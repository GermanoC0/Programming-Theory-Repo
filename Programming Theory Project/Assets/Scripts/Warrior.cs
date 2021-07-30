using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : Unit // INHERITANCE from Unit
{
    //public Slider healthBar;

    Warrior()
    {   
        // Set Warrior attributes
        health = 100;
        speed = 2;
        damage = 10;
        checkRange = 3;
        attackRange = 2;
        attackSpeed = 2f;
    }

    protected override void Hit() // POLYMORPHISM
    {
        StartCoroutine(HitDamage());  // Specific Attack for Warrior Class
    }

    
    /// <summary>
    /// Attack mode spedific for units to the nearest enemy unit. When the enemy unit health < 1the unit will be destroyed
    /// </summary>
    /// <returns></returns>
    IEnumerator HitDamage()
    {
        if ( nearestEnemy != null )
        {
            //Debug.Log( "Nearest " + nearestEnemy.GetComponent<Unit>().tag + " " + nearestEnemy.GetComponent<Unit>().name + " with health " + nearestEnemy.GetComponent<Unit>().health );

            while ( nearestEnemy.GetComponent<Unit>().health > 1 )
            {
                if ( nearestEnemy != null )
                {
                    // Update nearestEnemy Health bar and health
                    UpdateEnemyHealthStatus();
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
