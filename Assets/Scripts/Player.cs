using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed;
    private bool host;
    public SpriteRenderer player;

    // Start is called before the first frame update
    void Start()
    {
        host = false;
        player = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        vertical = Input.GetAxis(InputAxis.Vertical) * speed * Time.deltaTime;

        transform.Translate(new Vector3(horizontal, vertical, 0));
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(Input.GetAxis(InputAxis.GunHorizontal) > 0){
            if(!host){
                GameObject enemy = collider.gameObject;
                player.Sprite = enemy.Find("Sprite").GetComponent<SpriteRenderer>().Sprite;

            }
        }
    }
}
