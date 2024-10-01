//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Regulates the Game by containing the BeginGame and EndGame Unity Events. 
// Additionally contains the score of the player, that is shown at the end of the game.
// Lastly, it regulates the "Menu" for the player upon completion and beginning of the game.
public class GameManager : MonoBehaviour
{
    public UnityEvent BeginGame;
    public UnityEvent EndGame;
    public bool playing = false;
    private int score;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    private void Start()
    {
        if (BeginGame == null) BeginGame = new UnityEvent();
        if (EndGame == null) EndGame = new UnityEvent();
        BeginGame.AddListener(StartGameMessage);
        EndGame.AddListener(EndGameMessage);
    }
    private void StartGameMessage()
    {
        Debug.Log("Starting Game");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        score = 0;
    }
    private void EndGameMessage()
    {
        Debug.Log("Ending Game");
        transform.GetChild(2).gameObject.GetComponent<TMPro.TMP_Text>().text = "Congrats! Your Score is: " + score;
        transform.GetChild(2).gameObject.SetActive(true);
        Invoke("ActivateButton", 5);
    }
    private void ActivateButton()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void IncreaseScore()
    {
        score++;
    }
}
