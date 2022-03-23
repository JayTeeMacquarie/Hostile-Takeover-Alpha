using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private bool infected;
    private float deathTimer, deathRate;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        deathTimer = Time.time;
        deathRate = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            if(infected){
                player.killHost();
            }
            Destroy(gameObject);
        }
        if(infected && deathTimer < Time.time){
            health--;
            deathTimer = Time.time + deathRate;
        }
    }

    public void infect()
    {
        infected = true;
    }
}
