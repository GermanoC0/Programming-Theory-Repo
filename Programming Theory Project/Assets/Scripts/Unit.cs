using UnityEngine;


public class Unit : MonoBehaviour
{
    public int health { get; set; }
    protected float speed;
    protected int damage;
    protected float checkRange;
    protected float attackRange;

    protected GameObject flagA;
    protected GameObject flagB;

    protected GameObject nearestEnemy;
    private bool isAttacking = false;
    private bool readyToAttack = false;

    private bool flag = true;

    

    private void Awake()
    {
        flagA = GameObject.Find( "FlagA" );
        flagB = GameObject.Find( "FlagB" );

        /*
        if ( flagA != null )
            Debug.Log( "FlagA not NULL" );
        else
            Debug.Log( "FlagA is NULL" );*/

        
    }

    // Start is called before the first frame update
    void Start()
    {
        Yell();
    }

    protected virtual void Yell()
    {
        Debug.Log("NoOne");
    }


    // Update is called once per frame
    void Update()
    {
        if ( !isAttacking )
            MoveTowardFlag();


        if ( CheckIfNearbyEnemy() )
        {
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

        if (readyToAttack)
        {
            if (flag)
            {
                flag = false;
                Hit();
            }
            
        }            
    }


    protected virtual void Hit()
    {
        Debug.Log( "Passo Base" );
        readyToAttack = false;
        flag = true;
        isAttacking = false;
    }

    protected virtual void MoveTowardEnemy()
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

    private void AdjustAttackingPosition()
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

    protected virtual void MoveTowardFlag()
    {
        //Debug.Log( "Unit Speed set to: " + speed );

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

    private bool CheckIfNearbyEnemy()
    {
        GameObject o = NearbyEnemy();
        if ( o != null )
        {
            Debug.Log( gameObject.tag + " " + gameObject.name + " is near to " + o.name );
            nearestEnemy = o;
            return true;
        }

        return false;
    }

    private int CheckFaction()
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

    private GameObject NearbyEnemy()
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
