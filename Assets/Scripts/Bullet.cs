using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool friendly;
    public bool eFriendly;
    public float lifespan;
    public int damage;
    private float age;
    private Vector3 movement;
    public GameObject shooter;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        movement = new Vector3(speed, 0, 0);
        age = 0;
        eFriendly = false;
        friendly = false;
    }

    void Update()
    {
        age = age + Time.deltaTime;
        transform.Translate(movement);
        if(age > lifespan){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.CompareTag("Enemy") && other != shooter && !eFriendly){
            Debug.Log("lol");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.health = enemy.health - damage;
            Debug.Log("attacked, health is:" + enemy.health);
            Destroy(gameObject);
        }

        if(other.CompareTag("Player") && !friendly){
            if(player.getHost() != null){
                Enemy enemy = player.getHost().GetComponent<Enemy>();
                enemy.health -= damage;
                Debug.Log("attacked, health is:" + enemy.health);
            }
            else{
                player.health -= damage;
                Debug.Log("Ouch, health: " + player.health);
            }
        }
    }
}
