//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple button trigger that begins the game (utilises the GameManager Script)
public class ButtonPress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.BeginGame != null && !GameManager.Instance.playing)
        {
            GameManager.Instance.BeginGame.Invoke();
            GameManager.Instance.playing = true;
        }
    }
}