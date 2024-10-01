//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Regulates the health and UI for the player. 
// Invokes the EndGame Event should the players health drop to 0
public class Player : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1f;
        GameManager.Instance.BeginGame.AddListener(SetUp);
    }
    public void ChangeHealth()
    {
        slider.value -= 0.1f;
        if (slider.value == 0 && GameManager.Instance.BeginGame != null && GameManager.Instance.playing)
        {
            GameManager.Instance.playing = false;
            GameManager.Instance.EndGame.Invoke();
        }

    }
    private void SetUp()
    {
        slider.value = 100;
    }
}
