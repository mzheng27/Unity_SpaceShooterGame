using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;

    private Vector3 _defaultSpawn;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    private void Start()
    {
        _defaultSpawn = transform.position;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        Destruction();
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        PlayerController.Instance.PlayerKilled();
    }

    public void Respawn()
    {
        transform.position = _defaultSpawn;
    }
}
















