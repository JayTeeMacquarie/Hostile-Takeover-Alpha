using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy prefab;
    public float cooldown;
    private float coolTimer;
    private bool waiting;

    public int health;
    public bool canShoot;
    public float fireRate;
    public Bullet bullet;

    void Start()
    {
        coolTimer = Time.time;
        waiting = false;

        Enemy enemy = Instantiate(prefab);
        enemy.health = health;
        enemy.canShoot = canShoot;
        enemy.fireRate = fireRate;
        enemy.prefab = bullet;
        enemy.transform.parent = gameObject.transform;
        enemy.transform.position = gameObject.transform.position;
        enemy.spawn = this;
    }

    void Update()
    {
        if(waiting){
            coolTimer -= Time.deltaTime;
            if(coolTimer <= 0){
                Enemy enemy = Instantiate(prefab);
                enemy.health = health;
                enemy.canShoot = canShoot;
                enemy.fireRate = fireRate;
                enemy.prefab = bullet;
                enemy.transform.parent = gameObject.transform;
                enemy.transform.position = gameObject.transform.position;
                enemy.spawn = this;

                Debug.Log("Respawn");
                waiting = false;
                coolTimer = cooldown;
            }
        }
    }

    public void Respawn()
    {
        waiting = true;
        coolTimer = cooldown;
    }
}
