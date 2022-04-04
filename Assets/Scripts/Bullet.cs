using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifespan;
    public int damage;
    private float age;
    private Vector3 movement;
    private Player player;

    void Start()
    {
        movement = new Vector3(speed, 0, 0);
        age = 0;
        player = FindObjectOfType<Player>();
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
        if(other.CompareTag("Enemy") && other != player.getHost()){
            Debug.Log("lol");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.health = enemy.health - damage;
            Debug.Log("attacked, health is:" + enemy.health);
            Destroy(gameObject);
        }
    }
}
