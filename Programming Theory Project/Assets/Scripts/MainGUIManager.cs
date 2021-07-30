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
        nextTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag.gameOver)
        {
            gameOverText.gameObject.SetActive(true);
        }
        else if (Flag.won)
        {
            gameWonText.gameObject.SetActive( true );
        }
    }

    public void PlayerUnitWarriorSpawner()
    {
        if ( Time.time > nextTurn  && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerWarriorSpawn();
        }
            
    }

    public void PlayerUnitRangerSpawner()
    {
        if ( Time.time > nextTurn && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerRangerSpawn();
        }
        
    }

    public void PlayerUnitWizardSpawner()
    {
        if ( Time.time > nextTurn && !Flag.won && !Flag.gameOver )
        {
            nextTurn = Time.time + 4;
            gameManagerRef.PlayerWizardSpawn();
        }
        
    }
}
