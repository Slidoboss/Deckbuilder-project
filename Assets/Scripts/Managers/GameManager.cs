using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private int _playerHealth;
    private int _playerXP;
    private int _difficulty = 5;
    private void Awake()
    {
        //This is the singleton implementation ensuring there's ever only one instance of this class throughout the entire game.
        //Note: good for managing global game states, configs, shared resources and data that needs to accessed from different areas in the game.
        //Cons of this design pattern are, data that the instance holds only persists during the games session.
        //if you need to save data between sessions you'll need another form of persistent storage.
        //Tight coupling is also another con.
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
