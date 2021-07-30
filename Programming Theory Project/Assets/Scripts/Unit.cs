using UnityEngine;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    public int health { get; set; }
    protected float speed;
    protected int damage;
    protected float checkRange;
    protected float attackRange;
    protected float attackSpeed;

    protected GameObject flagA;
    protected GameObject flagB;

    protected GameObject nearestEnemy;
    private bool isAttacking = false;
    private bool readyToAttack = false;

    private bool flag = true;

    public Slider healthBar;


    private void Awake()
    {
        flagA = GameObject.Find( "FlagA" );
        flagB = GameObject.Find( "FlagB" );        
    }

    private void Start()
    {
        healthBar = ( gameObject.GetComponentInChildren<Canvas>() ).GetComponentInChildren<Slider>();

        // Random offset of attackRange
        attackRange = Random.Range( attackRange - 1.5f, attackRange );

        // Set the maxHealth on the health bar component
        healthBar.maxValue = health;
    }


    // Update is called once per frame
    void Update()
    {
        // If the unit is not attacking then run toward the enemy flag
        if ( !isAttacking )
            MoveTowardFlag();


        // If the unit detects a enemy unit nearby its range
        if ( CheckIfNearbyEnemy() )
        {
            // Prepare to attack, moving toward the enemy and adjusting the position attack range
            isAttacking = true;
            if (!readyToAttack)
            {
                MoveTowardEnemy();
                AdjustAttackingPosition();
            }
            
        }
        else
        {
            isAttacking = false;
        }

        // when the units has the right position and is ready to attack then Hit
        if (readyToAttack)
        {
            if (flag)
            {
                flag = false;
                Hit();
            }
            
        }            
    }


    protected virtual void Hit() // POLYMORPHISM
    {
        readyToAttack = false;
        flag = true;
        isAttacking = false;
    }

    /// <summary>
    /// Move the unit toward the nearest enemy since it reach the attacking range
    /// </summary>
    protected void MoveTowardEnemy() //ABSTRACTION
    {
        if ( nearestEnemy != null)
        {
            gameObject.transform.LookAt( nearestEnemy.transform.position );

            if ( ( nearestEnemy.transform.position - gameObject.transform.position ).magnitude > attackRange )
            {
                gameObject.transform.LookAt( nearestEnemy.transform.position);
                gameObject.transform.position = Vector3.MoveTowards( gameObject.transform.position, nearestEnemy.transform.position, speed * Time.deltaTime );
            }
        }
        
    }

    private void AdjustAttackingPosition() //ABSTRACTION
    {
        if ( nearestEnemy != null )
        {
            Ray ray = new Ray(transform.position, (nearestEnemy.transform.position - gameObject.transform.position ).normalized);
            gameObject.transform.LookAt( nearestEnemy.transform.position );
            //Debug.DrawRay( transform.position, ( nearestEnemy.transform.position - gameObject.transform.position ).normalized * 5, Color.green );
           
            RaycastHit hit;
            if ( Physics.Raycast( ray, out hit, ( attackRange + 0.5f ) ) )
            {
                if ( !hit.collider.CompareTag( "Enemy" ) && gameObject.CompareTag( "Player" ) )
                {
                    if ( Random.Range( -1f, 1f ) > 0 )
                        gameObject.transform.Translate( ( Vector3.right + new Vector3( 30, 0, 0 ) ) * Time.deltaTime * speed, Space.Self );
                    else
                        gameObject.transform.Translate( ( Vector3.left + new Vector3( -30, 0, 0 ) ) * Time.deltaTime * speed, Space.Self );
                }
                else if ( !hit.collider.CompareTag( "Player" ) && gameObject.CompareTag( "Enemy" ) )
                {
                    if ( Random.Range( -1f, 1f ) > 0 )
                        gameObject.transform.Translate( ( Vector3.right + new Vector3( 30, 0, 0 ) ) * Time.deltaTime * speed, Space.Self );
                    else
                        gameObject.transform.Translate( ( Vector3.left + new Vector3( -30, 0, 0 ) ) * Time.deltaTime * speed, Space.Self );
                }
                else
                    readyToAttack = true;
            }
        }

    }

    /// <summary>
    /// Depending on Faction, unit move toward the flag to capture
    /// </summary>
    protected void MoveTowardFlag() //ABSTRACTION
    {

        if ( CheckFaction() == 1 )
        {
            gameObject.transform.LookAt( flagA.transform.position );
            gameObject.transform.position = Vector3.MoveTowards( gameObject.transform.position, flagA.transform.position, speed * Time.deltaTime );
        }
        else if ( CheckFaction() == 0 )
        {
            gameObject.transform.LookAt( flagB.transform.position );
            gameObject.transform.position = Vector3.MoveTowards( gameObject.transform.position, flagB.transform.position, speed * Time.deltaTime );
        }
    }


    /// <summary>
    /// Check the nearest enemy unit and set it to the global variable nearest Enemy
    /// </summary>
    /// <returns></returns>
    private bool CheckIfNearbyEnemy() //ABSTRACTION
    {
        GameObject o = NearbyEnemy(); //ABSTRACTION
        if ( o != null )
        {
            //Debug.Log( gameObject.tag + " " + gameObject.name + " is near to " + o.name );
            nearestEnemy = o;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check and return the unit faction
    /// </summary>
    /// <returns></returns>
    private int CheckFaction() //ABSTRACTION
    {
        if (gameObject.CompareTag("Enemy"))
        {
            return 1; // Enemy Faction
        }
        else
        {
            return 0; // Player Faction
        }
    }

    /// <summary>
    /// Depending on the checkRange field of the unit, search and return the nearest enemy unit
    /// </summary>
    /// <returns></returns>
    private GameObject NearbyEnemy() //ABSTRACTION
    {
        GameObject[] enemies;
        float minDist;
        int minDistIndex;

        if ( CheckFaction() == 1 )
            enemies = GameObject.FindGameObjectsWithTag("Player");
        else
            enemies = GameObject.FindGameObjectsWithTag( "Enemy" );

        if (enemies.Length > 0)
        {
            minDist = ( enemies[0].transform.position - gameObject.transform.position ).magnitude;
            minDistIndex = 0;

            for ( int i = 1; i < enemies.Length; i++ )
            {
                float tempDist = (enemies[i].transform.position - gameObject.transform.position).magnitude;
                if ( minDist > tempDist )
                {
                    minDist = tempDist;
                    minDistIndex = i;
                }
            }

            if ( minDist < checkRange )
            {
                return enemies[minDistIndex];
            }
            else
                return null;
        }

        return null;
        
    }
}
