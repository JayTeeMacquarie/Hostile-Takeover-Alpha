using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical, fireTimer;
    public float speed, attackSpeed, fireRate, jump, hostJump;
    public int attack;
    public int health;
    public int maxHealth;
    public int playerLives = 3;
    public Transform respawnPoint;
    private bool jumping, faceLeft;
    private float attacked;
    private Enemy host;
    private SpriteRenderer playerAppearence;
    private Rigidbody2D player;
    public Bullet prefab;
    public bool playerdied = false;
    public HealthBar healthBar;

    public HostHealthBar hostHealthBar;

    void Start()
    {
        playerAppearence = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
        attacked = Time.time;
        speed = 3;
        jumping = false;
        fireTimer = Time.time;
        faceLeft = false;
        healthBar.SetMaxHealth(maxHealth);
        health = 50;
        Time.timeScale = 1f;
    }

    void Update()
    {
        healthBar.SetHealth(health, false);

        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0, 0));

        if(faceLeft && horizontal > 0){
            faceLeft = false;
        }
        if(!faceLeft && horizontal < 0){
            faceLeft = true;
        }

        if(host != null && faceLeft){
            host.turn(-1);
        }
        else if(host != null && !faceLeft){
            host.turn(1);
        }

        if(host != null && Input.GetAxis("Eject") > 0){
            killHost();
        }

        if(Input.GetKey(KeyCode.Space) && !jumping){
            if(host != null){
                player.AddForce(new Vector2(0, hostJump), ForceMode2D.Impulse);
            }
            else{
                player.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }
            jumping = true;
        }

        if(host != null){
            host.transform.position = transform.position + new Vector3(0, 0.6f, 0);
            if(fireTimer < Time.time && Input.GetAxis("Attack") > 0 && host.canShoot){
                fireTimer = Time.time + fireRate;
                Bullet bullet = Instantiate(prefab);
                bullet.name = "Bullet";
                bullet.friendly = true;
                bullet.transform.position = host.transform.position;
                bullet.shooter = gameObject;
                if(faceLeft){
                    bullet.speed = bullet.speed*-1;
                }
            }
            hostHealthBar.SetHealth(host.health);
        }

        if(health <= 0)
        {
            playerAppearence.enabled = false;
            playerdied = true;
            Debug.Log("u died");
            youDied();
        }
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    public void youDied()
    {
            if(playerLives != 0){
                transform.position = respawnPoint.transform.position;
                playerdied = false;
                health = maxHealth;
                playerAppearence.enabled = true;
                playerLives -= 1;
                LiveCounter.livesCount -= 1;
            }
        }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Enemy")){
            Enemy enemy = other.GetComponent<Enemy>();
            if(Input.GetAxis("Infect") > 0 && host == null && enemy.health <= 60){
                transform.position = enemy.transform.position;
                other.transform.parent = gameObject.transform;
                Debug.Log("infected >:)");
                host = enemy;
                playerAppearence.enabled = false;
                enemy.infect();
            }
            if(Input.GetAxis("Attack") > 0 && attacked < Time.time && host == null){
                //can only melee when not in a host (could change to scientist can melee too)
                enemy.damage(attack);
                Debug.Log("attacked, health is:" + enemy.health);
                attacked = Time.time + attackSpeed;
            }
        }

        if(other.CompareTag("Ground")){
            jumping = false;
        }
    }

    public void killHost()
    {
        if(host != null){
            Debug.Log("evicted :(");
            playerAppearence.enabled = true;
            host.health = 0;
            hostHealthBar.SetHealth(host.health);
        }
    }

    //In future set up so that infectable is an interface/abstract class
    //so this method returns the something of that type and can access
    //stuff like health
    public Enemy getHost()
    {
        return host;
    }
}
