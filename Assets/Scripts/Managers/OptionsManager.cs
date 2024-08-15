using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private bool _muteSound = false;
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameManager.Instance.AudioManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
