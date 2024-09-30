using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMenu : MonoBehaviour
{
    public static StartMenu Instance { get; private set; }
    private bool playing = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public UnityEvent BeginGame;
    private void Start()
    {
        if (BeginGame == null) BeginGame = new UnityEvent();
        BeginGame.AddListener(StartGameMessage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BeginGame != null && !playing)
        {
            BeginGame.Invoke();
            playing = true;
        }
    }    

    void StartGameMessage()
    {
        Debug.Log("Starting Game");
    }
}
