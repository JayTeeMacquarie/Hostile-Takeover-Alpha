using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private bool infected;
    public bool canShoot;
    private float deathTimer, deathRate, fireTimer;
    public float fireRate;
    private Player player;
    public Bullet prefab;

    void Start()
    {
        player = FindObjectOfType<Player>();
        deathTimer = Time.time;
        deathRate = 0.2f;
        fireTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            if(infected){
                player.killHost();
                player.speed = 3;
            }
            Destroy(gameObject);
        }
        if(infected && deathTimer < Time.time){
            health--;
            player.health++;
            deathTimer = Time.time + deathRate;
        }
        if(Vector3.Distance(transform.position, player.transform.position) < 5 && !infected && canShoot){
            float yDistance = transform.position.y - player.transform.position.y;
            yDistance = (yDistance*yDistance)/yDistance;
            if(yDistance < 0.25 && fireTimer < Time.time){
                fireTimer = Time.time + fireRate;
                Bullet bullet = Instantiate(prefab);
                bullet.name = "Bullet";
                bullet.transform.position = transform.position;
                bullet.shooter = gameObject;
                if(transform.position.x - player.transform.position.x > 0){
                    bullet.speed = bullet.speed*-1;
                }
            }
        }
    }

    public void infect()
    {
        infected = true;
        player.speed = 5;
    }

    public bool isInfected()
    {
        return infected;
    }
}
