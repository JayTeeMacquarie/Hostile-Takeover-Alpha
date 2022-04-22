using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical, fireTimer;
    public float speed, attackSpeed, fireRate, jump, hostJump;
    public int attack, health;
    public int maxHealth;
    private bool jumping, faceLeft;
    private float attacked;
    private Enemy host;
    private SpriteRenderer playerAppearence;
    private Rigidbody2D player;
    public Bullet prefab;

    // Start is called before the first frame update
    void Start()
    {
        playerAppearence = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
        attacked = Time.time;
        speed = 3;
        jumping = false;
        fireTimer = Time.time;
        faceLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0, 0));

        if(faceLeft && horizontal > 0){
            faceLeft = false;
        }
        if(!faceLeft && horizontal < 0){
            faceLeft = true;
        }

        if(host != null && Input.GetAxis("Fire2") > 0){
            killHost();
        }

        if(Input.GetKey(KeyCode.Space) && !jumping){
            Debug.Log("jump");
            if(host != null){
                player.AddForce(new Vector2(0, hostJump), ForceMode2D.Impulse);
            }
            else{
                player.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }
            jumping = true;
        }

        if(host != null){
            if(fireTimer < Time.time && Input.GetAxis("Fire3") > 0){
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
        }

        if(health <= 0){
            playerAppearence.enabled = false;
            Debug.Log("u died");
        }

        if(health > maxHealth){
            health = maxHealth;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Enemy")){
            Enemy enemy = other.GetComponent<Enemy>();
            if(Input.GetAxis("Fire1") > 0 && host == null && enemy.health <= 60){
                transform.position = enemy.transform.position;
                other.transform.parent = gameObject.transform;
                Debug.Log("infected >:)");
                host = enemy;
                playerAppearence.enabled = false;
                enemy.infect();
            }
            if(Input.GetAxis("Fire3") > 0 && attacked < Time.time){
                enemy.health = enemy.health - attack;
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
