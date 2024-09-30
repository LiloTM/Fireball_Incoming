//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (StartMenu.Instance.BeginGame != null && !StartMenu.Instance.playing)
        {
            StartMenu.Instance.BeginGame.Invoke();
            StartMenu.Instance.playing = true;
        }
    }
}