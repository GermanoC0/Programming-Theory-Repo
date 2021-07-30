using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIManager : MonoBehaviour
{
    [SerializeField]
    Button spawnWarriorBtn;

    [SerializeField]
    Button spawnRangerBtn;

    [SerializeField]
    Button spawnWizardBtn;

    GameManager gameManagerRef;

    private float nextTurn;

    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text gameWonText;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerRef = GameObject.Find( "GameManager" ).GetComponent<GameManager>();

        // Player spawning units delay
        nextTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // GameOver or Win Messages manager
        if (Flag.gameOver)
        {
            gameOverText.gameObject.SetActive(true);
        }
        else if (Flag.won)
        {
            gameWonText.gameObject.SetActive( true );
        }
    }

    /// <summary>
    /// Warrior button controller (you can spawn a warrior every 4 seconds)
    /// </summary>
    public void PlayerUnitWarriorSpawner()
    {
        if ( Time.time > nextTurn  && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerWarriorSpawn();
        }
            
    }

    /// <summary>
    /// Ranger button controller (you can spawn a ranger every 4 seconds)
    /// </summary>
    public void PlayerUnitRangerSpawner()
    {
        if ( Time.time > nextTurn && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerRangerSpawn();
        }
        
    }

    /// <summary>
    /// Wizard button controller (you can spawn a wizard every 4 seconds)
    /// </summary>
    public void PlayerUnitWizardSpawner()
    {
        if ( Time.time > nextTurn && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerWizardSpawn();
        }
        
    }
}
