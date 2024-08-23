using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpawner : MonoBehaviour
{
    public ParticleSystem particles;

    private void FixedUpdate()
    {
        if (!particles.isPlaying && FireSpawnState.Enabled)
        {
            particles.Play();
        }
        else if (particles.isPlaying && !FireSpawnState.Enabled)
        {
            particles.Stop();
        }
    }
}
