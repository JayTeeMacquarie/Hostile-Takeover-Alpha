using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed, attackSpeed;
    public int attack;
    private bool isHost, jumping;
    private float attacked;
    private GameObject host;
    private SpriteRenderer playerAppearence;
    private Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        playerAppearence = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
        attacked = Time.time;
        speed = 3;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0, 0));

        if(host != null && Input.GetAxis("Fire2") > 0){
            killHost();
        }

        if(Input.GetKey(KeyCode.Space) && !jumping && isHost){
            Debug.Log("jump");
            Vector2 jump = new Vector2(0, 5);
            player.AddForce(jump, ForceMode2D.Impulse);
            jumping = true;
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
                isHost = true;
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
}
