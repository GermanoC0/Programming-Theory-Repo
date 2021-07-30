using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranger : Unit
{
    public Slider healthBar;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject projectileSpawnPoint;

    Ranger()
    {
        health = 35;
        speed = 4;
        damage = 18;
        checkRange = 6;
        attackRange = 5;

        /*
        if ( flagA != null )
            Debug.Log( "FlagA not NULL" );
        else
            Debug.Log( "FlagA is NULL" );*/
    }

    protected override void Yell()
    {
        Debug.Log( "I'm a Ranger!" );
        attackRange = Random.Range( attackRange - 1.5f, attackRange );
        healthBar = ( gameObject.GetComponentInChildren<Canvas>() ).GetComponentInChildren<Slider>();
        healthBar.maxValue = health;
    }

    protected override void MoveTowardFlag()
    {
        //Debug.Log( "Ranger Speed: " + speed );
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
        if ( nearestEnemy != null)
        {
            Debug.Log( "Nearest " + nearestEnemy.GetComponent<Unit>().tag + " " + nearestEnemy.GetComponent<Unit>().name + " with health " + nearestEnemy.GetComponent<Unit>().health );

            while ( nearestEnemy.GetComponent<Unit>().health > 1 )
            {
                if ( nearestEnemy != null )
                {
                    ThrowArrow();
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

                yield return new WaitForSeconds( 3f );

            }
            if ( nearestEnemy != null )
            {
                Destroy( nearestEnemy.gameObject );
                base.Hit();
            }
                
        }
        
    }

    private void ThrowArrow()
    {
        GameObject arrowRef = Instantiate(arrow, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
        //arrowRef.transform.rotation = Quaternion.Euler(90, projectileSpawnPoint.transform.rotation.y, 0);
        arrowRef.GetComponent<Rigidbody>().AddForce( arrowRef.transform.forward * 4f, ForceMode.Impulse );
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Projectile" ) )
        {
            Destroy( other.gameObject );
        }
    }

}
