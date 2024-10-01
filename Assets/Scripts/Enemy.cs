//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures the animations of the enemy and destruction via player projectile
// Lets the enemy shoot enemy projectiles in the direction of the players head
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileSpeed = 100;
    private GameObject playerHead;
    private EmenyManager eM;
    private Animator anim;

    private void Start()
    {
        playerHead = GameObject.FindGameObjectWithTag("ActorHead");
        anim = transform.GetChild(0).GetComponent<Animator>();
        InvokeRepeating("Shoot", 3f, 6f);    
    }
    public void getEnemyManager(EmenyManager M)
    {
        eM = M;
    }
    private void Shoot()
    {
        if (playerHead == null) return;
        anim.Play("Attack01");
        GameObject p = Instantiate(projectilePrefab, transform.GetChild(1).transform.position, Quaternion.identity);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        rb.velocity += (playerHead.transform.position - p.transform.position).normalized * Time.deltaTime * projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        eM.enemies.Remove(this.gameObject);
        GameManager.Instance.IncreaseScore();
        if(other.gameObject.tag == "Projectile") Destroy(this.gameObject);
    }
}
