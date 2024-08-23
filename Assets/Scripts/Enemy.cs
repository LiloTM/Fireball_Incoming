using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileSpeed = 100;
    private GameObject playerHead;

    private void Start()
    {
        playerHead = GameObject.FindGameObjectWithTag("ActorHead");
        InvokeRepeating("Shoot", 3f, 3f);    
    }

    private void Shoot()
    {
        if (playerHead == null) return;

        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        rb.velocity += (playerHead.transform.position - p.transform.position) * Time.deltaTime * projectileSpeed;
    }
}
