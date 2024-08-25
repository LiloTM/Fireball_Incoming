using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileSpeed = 100;
    private GameObject playerHead;
    private EmenyManager eM;

    private void Start()
    {
        playerHead = GameObject.FindGameObjectWithTag("ActorHead");
        InvokeRepeating("Shoot", 3f, 3f);    
    }
    public void getEnemyManager(EmenyManager M)
    {
        eM = M;
    }
    private void Shoot()
    {
        if (playerHead == null) return;

        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        rb.velocity += (playerHead.transform.position - p.transform.position) * Time.deltaTime * projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        eM.enemies.Remove(this.gameObject);
        if(other.gameObject.tag == "Projectile") Destroy(this.gameObject);
    }
}
