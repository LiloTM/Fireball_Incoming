using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    private void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1f;
        StartMenu.Instance.BeginGame.AddListener(SetUp);
    }
    public void ChangeHealth()
    {
        slider.value -= 0.1f;
        if (slider.value == 0 && StartMenu.Instance.BeginGame != null && StartMenu.Instance.playing)
        {
            StartMenu.Instance.playing = false;
            StartMenu.Instance.EndGame.Invoke();
        }

    }
    private void SetUp()
    {
        slider.value = 100;
    }
}
