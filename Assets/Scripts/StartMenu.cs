using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMenu : MonoBehaviour
{
    public UnityEvent BeginGame;
    public bool playing = false;

    public static StartMenu Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    
    private void Start()
    {
        if (BeginGame == null) BeginGame = new UnityEvent();
        BeginGame.AddListener(StartGameMessage);
    }

    void StartGameMessage()
    {
        Debug.Log("Starting Game");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
