using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit[] units;
    private Dictionary<string, Unit> playerUnits;

    [SerializeField] Unit warrior;
    [SerializeField] Unit ranger;
    [SerializeField] Unit wizard;

    private bool isGameOver;
    private bool isWon;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isWon = false;

        // Dictionary with the possible units
        playerUnits = new Dictionary<string, Unit>();
        playerUnits.Add("Warrior", warrior);
        playerUnits.Add( "Ranger", ranger );
        playerUnits.Add( "Wizard", wizard );

        InvokeRepeating( "EnemySpawner", 1, 4 );
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = Flag.gameOver;
        isWon = Flag.won;
    }

    /// <summary>
    /// If the game is not finished and no winner, random enemies spawn
    /// </summary>
    private void EnemySpawner()
    {
        if ( !isGameOver && !isWon )
        {
            int randomIndex = Random.Range(0, units.Length);

            Unit enemy = Instantiate(units[randomIndex], new Vector3(0,1,8), units[randomIndex].transform.rotation);

            // Adjust unit and it's healthbar rotation
            enemy.transform.rotation = Quaternion.Euler( 0, -180, 0 );
            enemy.GetComponentInChildren<Canvas>().transform.rotation = Quaternion.Euler( 0, 0, 0 );

            enemy.gameObject.tag = "Enemy";
            enemy.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            // Stop spawning enemies if the game is ended
            CancelInvoke( "EnemySpawner" );
        }
    }

    /// <summary>
    /// Spawn Player Warrior unit when the Warrior button has pressed
    /// </summary>
    public void PlayerWarriorSpawn()
    {
        Unit warrior = Instantiate(playerUnits["Warrior"], new Vector3(0,1,-9), playerUnits["Warrior"].transform.rotation);
        
        warrior.gameObject.tag = "Player";
        warrior.GetComponent<Renderer>().material.color = Color.blue;
    }

    /// <summary>
    /// Spawn Player Ranger unit when the Ranger button has pressed
    /// </summary>
    public void PlayerRangerSpawn()
    {
        Unit ranger = Instantiate(playerUnits["Ranger"], new Vector3(0,1,-9), playerUnits["Ranger"].transform.rotation);
        
        ranger.gameObject.tag = "Player";
        ranger.GetComponent<Renderer>().material.color = Color.blue;
    }

    /// <summary>
    /// Spawn Player Wizard unit when the Wizard button has pressed
    /// </summary>
    public void PlayerWizardSpawn()
    {
        Unit wizard = Instantiate(playerUnits["Wizard"], new Vector3(0,1,-9), playerUnits["Wizard"].transform.rotation);
        
        wizard.gameObject.tag = "Player";
        wizard.GetComponent<Renderer>().material.color = Color.blue;
    }
}
