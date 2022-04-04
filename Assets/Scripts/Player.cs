using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical, fireTimer;
    public float speed, attackSpeed, fireRate;
    public int attack;
    private bool jumping, faceLeft;
    private float attacked;
    private GameObject host;
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

        if(Input.GetKey(KeyCode.Space) && !jumping && host != null){
            Debug.Log("jump");
            Vector2 jump = new Vector2(0, 5);
            player.AddForce(jump, ForceMode2D.Impulse);
            jumping = true;
        }

        if(host != null){
            if(fireTimer < Time.time && Input.GetAxis("Fire3") > 0){
                fireTimer = Time.time + fireRate;
                Bullet bullet = Instantiate(prefab);
                bullet.name = "Bullet";
                bullet.transform.position = host.transform.position;
                if(faceLeft){
                    bullet.speed = bullet.speed*-1;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Enemy")){
            Enemy enemy = other.GetComponent<Enemy>();
            if(Input.GetAxis("Fire1") > 0 && host == null && enemy.health <= 60){
                other.transform.parent = gameObject.transform;
                Debug.Log("infected >:)");
                host = other;
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
            Destroy(host);
        }
    }

    public GameObject getHost()
    {
        return host;
    }
}
