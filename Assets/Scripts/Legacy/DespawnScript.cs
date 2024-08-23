using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{

    public int maxTicksLive;

    private int ticksLived = 0;

    private void FixedUpdate()
    {
        if (ticksLived++ > maxTicksLive)
        {
            Destroy(gameObject);
        }
    }
}
