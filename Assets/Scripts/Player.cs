using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed, attackSpeed;
    public int attack;
    private bool isHost;
    private float attacked;
    private GameObject host;
    private SpriteRenderer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<SpriteRenderer>();
        attacked = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        vertical = Input.GetAxis(InputAxis.Vertical) * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, vertical, 0));

        if(host != null && Input.GetAxis("Fire2") > 0){
            killHost();
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
                player.enabled = false;
                enemy.infect();
            }
            if(Input.GetAxis("Fire3") > 0 && attacked < Time.time){
                enemy.health = enemy.health - attack;
                Debug.Log("attacked, health is:" + enemy.health);
                attacked = Time.time + attackSpeed;
            }
        }
    }

    public void killHost()
    {
        if(host != null){
            Debug.Log("evicted :(");
            player.enabled = true;
            Destroy(host);
        }
    }
}
