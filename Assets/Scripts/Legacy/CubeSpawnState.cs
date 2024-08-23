using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnState : MonoBehaviour
{
    public static bool Enabled = false;

    private void OnEnable()
    {
        Enabled = true;
    }

    private void OnDisable()
    {
        Enabled = false;
    }
}
