using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int speed;
    private int chaseSpeed;
    private bool infected;
    public bool canShoot;
    private float deathTimer, deathRate, fireTimer;
    public float fireRate;
    private Player player;
    public Bullet prefab;
    public Spawner spawn;
    private int isRight;

    void Start()
    {
        isRight = 1;
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
            spawn.Respawn();
            Destroy(gameObject);
        }
        if(infected && deathTimer < Time.time){
            health--;
            player.health++;
            deathTimer = Time.time + deathRate;
        }
        float yDistance = transform.position.y - player.transform.position.y;
        yDistance = (yDistance*yDistance)/yDistance;
        if(Vector3.Distance(transform.position, player.transform.position) < 7 && yDistance < 0.25){
            //if enemy is clos to the player

            if(transform.position.x - player.transform.position.x > 0){
                //if on players left/right
                    if(canShoot){
                        isRight = -1;
                    }
                    else{
                        isRight = 1;
                    }
            }
            else{
                if(canShoot){
                    isRight = 1;
                }
                else{
                    isRight = -1;
                }
            }
            if(!infected && canShoot && fireTimer < Time.time){
                fireTimer = Time.time + fireRate;
                Bullet bullet = Instantiate(prefab);
                bullet.name = "Bullet";
                bullet.transform.position = transform.position;
                bullet.shooter = gameObject;
                if(isRight == -1){
                    bullet.speed = bullet.speed*-1;
                }
            }
        }
        else if(!infected && Vector3.Distance(transform.position, player.transform.position) > 5){
            //idle
            transform.Translate(isRight*speed*Time.deltaTime, 0, 0);
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Track")){
            isRight *= -1;
        }
    }
}
