using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health, speed;
    private int chaseSpeed, isRight;
    private bool infected;
    public bool canShoot;
    private float deathTimer, deathRate, fireTimer, damageTimer, damageLength;
    public float fireRate;
    private Player player;
    private SpriteRenderer sprite;
    public Bullet prefab;
    public Spawner spawn;
    public HealthBar healthPrefab;
    private HealthBar healthBar;
    private RectTransform healthTran;
    private Camera cam;

    void Start()
    {
        isRight = 1;
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        healthBar = Instantiate(healthPrefab);
        healthBar.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        healthTran = healthBar.GetComponent<RectTransform>();
        deathTimer = Time.time;
        deathRate = 0.2f;
        fireTimer = Time.time;
        damageTimer = Time.time;
        damageLength = 0.2f;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null){
            healthBar.SetHealth(health, true);
            Vector2 newPos = RectTransformUtility.WorldToScreenPoint(cam, transform.position);
            newPos.y += 250;
            healthTran.position = newPos;
        }

        if(health <= 0){
            if(infected){
                player.killHost();
                player.speed = 3;
            }
            spawn.Respawn();
            Destroy(healthBar);
            Destroy(gameObject);
        }
        if(infected && deathTimer < Time.time){
            health--;
            player.health++;
            deathTimer = Time.time + deathRate;
        }
        float yDistance = transform.position.y - player.transform.position.y;
        yDistance = (yDistance*yDistance)/yDistance;
        if(Vector3.Distance(transform.position, player.transform.position) < 7 && !infected){
            //if enemy is close to the player
            if(yDistance < 0.6 || yDistance > -0.6){
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
                if(canShoot && fireTimer < Time.time){
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
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > 5 || yDistance < 0.6 || yDistance > -0.6){
            //This else if allows for it to shoot while running
            //idle
            if(!infected){
                transform.Translate(isRight*speed*Time.deltaTime, 0, 0);
            }
        }

        if(isRight == 1){
            sprite.flipX = true;
        }
        else{
            sprite.flipX = false;
        }

        if(damageTimer < Time.time){
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    public void infect()
    {
        infected = true;
        Destroy(healthBar.gameObject);
        player.speed = 5;
    }

    public bool isInfected()
    {
        return infected;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Track") && !infected){
            isRight *= -1;
        }
    }

    public void damage(int d)
    {
        //also change sprite colour
        health -= d;
        sprite.color = new Color(1, 0, 0, 1);
        damageTimer = Time.time + damageLength;
    }

    public void turn(int i){
        isRight = i;
    }
}
