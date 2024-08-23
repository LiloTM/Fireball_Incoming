using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int lifeTime = 5;
    [SerializeField] private float timeAlive = 0;
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeTime) Destroy(this.gameObject);
    }

}
