using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : Unit
{
    public Slider healthBar;
    [SerializeField]
    private GameObject fireball;
    [SerializeField]
    private GameObject projectileSpawnPoint;

    Wizard()
    {
        health = 20;
        speed = 3;
        damage = 12;
        checkRange = 5;
        attackRange = 4;

        /*
        if ( flagA != null )
            Debug.Log( "FlagA not NULL" );
        else
            Debug.Log( "FlagA is NULL" );*/
    }

    protected override void Yell()
    {
        Debug.Log( "I'm a Wizard!" );
        attackRange = Random.Range( attackRange - 1.5f, attackRange );
        healthBar = ( gameObject.GetComponentInChildren<Canvas>() ).GetComponentInChildren<Slider>();
        healthBar.maxValue = health;
    }

    protected override void MoveTowardFlag()
    {
        //Debug.Log( "Wizard Speed: " + speed );
        base.MoveTowardFlag();
    }

    protected override void MoveTowardEnemy()
    {
        //Debug.Log( "Warrior Speed: " + speed );
        base.MoveTowardEnemy();
    }

    protected override void Hit()
    {
        //
        StartCoroutine( HitDamage() );
    }


    IEnumerator HitDamage()
    {
        if ( nearestEnemy != null )
        {
            Debug.Log( "Nearest " + nearestEnemy.GetComponent<Unit>().tag + " " + nearestEnemy.GetComponent<Unit>().name + " with health " + nearestEnemy.GetComponent<Unit>().health );

            while ( nearestEnemy.GetComponent<Unit>().health > 1 )
            {
                if ( nearestEnemy != null )
                {
                    CastFireball();
                    if ( nearestEnemy.GetComponent<Unit>() is Warrior )
                        nearestEnemy.GetComponent<Warrior>().healthBar.value -= damage;
                    else if ( nearestEnemy.GetComponent<Unit>() is Wizard )
                        nearestEnemy.GetComponent<Wizard>().healthBar.value -= damage;
                    else if ( nearestEnemy.GetComponent<Unit>() is Ranger )
                        nearestEnemy.GetComponent<Ranger>().healthBar.value -= damage;
                    //healthBar.value -= damage;
                    nearestEnemy.GetComponent<Unit>().health -= damage;
                }  
                else
                    break;

                yield return new WaitForSeconds( 5f );

            }
            if ( nearestEnemy != null )
            {
                Destroy( nearestEnemy.gameObject );
                base.Hit();
            }
        }
        
    }

    private void CastFireball()
    {
        GameObject fireballRef = Instantiate(fireball, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
        fireballRef.GetComponent<Rigidbody>().AddForce( projectileSpawnPoint.transform.forward * 3f, ForceMode.Impulse);
    }

    private void OnTriggerEnter( Collider other )
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy( other.gameObject );
        }
    }

}
