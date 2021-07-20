using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject warrior;

    // Start is called before the first frame update
    void Start()
    {
        warrior.gameObject.tag = "Enemy";
        warrior.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
