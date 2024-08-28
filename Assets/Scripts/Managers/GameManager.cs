using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public DeckManager DeckManager{ get; private set; }
    public AudioManager AudioManager { get; private set; }
    public OptionsManager OptionsManager { get; private set; }
    private int _playerHealth;
    private int _playerXP;
    private int _difficulty = 5;
    public bool isPlayingCard = false;
    private void Awake()
    {
        //This is the singleton implementation ensuring there's ever only one instance of this class throughout the entire game.
        //Note: good for managing global game states, configs, shared resources and data that needs to accessed from different areas in the game.
        //Cons of this design pattern are, data that the instance holds only persists during the games session.
        //if you need to save data between sessions you'll need another form of persistent storage.
        //Tight coupling is also another con.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        DeckManager = GetComponentInChildren<DeckManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        OptionsManager = GetComponentInChildren<OptionsManager>();

        if(DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager"); 
            if(prefab == null)
            {
                Debug.Log("DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab,transform.position,Quaternion.identity,transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }
        if(AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager"); 
            if(prefab == null)
            {
                Debug.Log("AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab,transform.position,Quaternion.identity,transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if(OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager"); 
            if(prefab == null)
            {
                Debug.Log("OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab,transform.position,Quaternion.identity,transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
    }

    public int PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }
    public int PlayerXP
    {
        get { return _playerXP; }
        set { _playerXP = value; }
    }
    public int Difficulty
    {
        get { return _difficulty; }
        set { _difficulty = value; }
    }

}
