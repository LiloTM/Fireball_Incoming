//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to destroy the Projectile over time and reduce the players health in case of a hit
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int lifeTime = 5;
    [SerializeField] private float timeAlive = 0;
    private Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeTime) Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ActorHead")
        {
            player.ChangeHealth();
            Destroy(this.gameObject);
        }
    }

}
