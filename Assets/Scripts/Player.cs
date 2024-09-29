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
    }
    public void ChangeHealth()
    {
        slider.value -= 0.1f;
    }
}
